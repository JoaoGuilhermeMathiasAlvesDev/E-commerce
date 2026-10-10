using DominioEcommerce.Entitidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryEcommerce.IRepository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Cliente> _userManager;
        public PedidoService(IUnitOfWork unitOfWork, UserManager<Cliente> userManager = null)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<PedidoResponseModel> AdicionarItemAsync(Guid pedidoId, ItemPedidoModel model)
        {
            var pedido = await _unitOfWork.Pedido.ObterPorId(pedidoId);
            var produto = await _unitOfWork.Produto.ObterPorId(model.ProdutoId);

            if (pedido == null)
            {
                throw new Exception("Pedido não encontrado.");
            }

            if (!produto.Ativo)
                throw new Exception($"O produto {produto.Nome} está inativo.");

            produto.DebitarEstoque(model.Quantidade);
            pedido.AdicionarItem(new ItemPedido(pedido.Id, produto.Id, produto.Nome, model.Quantidade, produto.Preco));

            _unitOfWork.Produto.Atualizar(produto);
            _unitOfWork.Pedido.Atualizar(pedido);
            await _unitOfWork.SalvarAsync();

            return PedidoResponseModel.De(pedido);
        }

        public Task<PedidoResponseModel> CancelarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoResponseModel> CriarAsync(CriarPedidoModel model)
        {
            if (model.Itens.Count == 0)
                throw new Exception("O pedido precisa ter ao menos um item.");

            var cliente = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.ClienteId)
                ?? throw new Exception("Cliente não encontrado.");

            await _unitOfWork.BeginTransactionAsync();

            var pedido = new Pedido(
                cliente.Id, cliente.Nome, cliente.PhoneNumber ?? string.Empty, cliente.Email!,
                cliente.Endereco, model.ValorFrete, model.MetodoPagamento, model.ObservacoesEntrega);

            var itens = model.Itens
                .GroupBy(i => i.ProdutoId)
                .Select(g => new ItemPedidoModel { ProdutoId = g.Key, Quantidade = g.Sum(x => x.Quantidade) });

            foreach (var item in itens)
            {
                var produto = await _unitOfWork.Produto.ObterPorId(item.ProdutoId);

                if (!produto.Ativo)
                    throw new Exception($"O produto {produto.Nome} está inativo.");

                produto.DebitarEstoque(item.Quantidade);
                _unitOfWork.Produto.Atualizar(produto);

                pedido.AdicionarItem(new ItemPedido(pedido.Id,produto.Id,produto.Nome,item.Quantidade, produto.Preco));

                await _unitOfWork.ItemPedido.Adicionar(pedido.Itens.Last());
            }

           
            await _unitOfWork.Pedido.Adicionar(pedido);
            await _unitOfWork.SalvarAsync();

            return PedidoResponseModel.De(pedido);
        }

        public async Task<IEnumerable<PedidoResponseModel>> ObterPorClienteAsync(Guid clienteId)
        {
            var pedidos = await _unitOfWork.Pedido.ObterPedidosClienteAsync(clienteId);
            return pedidos.Select(PedidoResponseModel.De);
        }

        public async Task<PedidoResponseModel> ObterPorIdAsync(Guid id)
        {
            var obterPedido = await _unitOfWork.Pedido.ObterPorId(id);

            if (obterPedido == null)
                throw new Exception("Pedido não encontrado.");

            return PedidoResponseModel.De(obterPedido);
        }

        public async Task<PedidoResponseModel> PagarAsync(Guid id)
        {
            var pedido = await _unitOfWork.Pedido.ObterPorId(id);
            pedido.Pagar();

            _unitOfWork.Pedido.Atualizar(pedido);
            await _unitOfWork.SalvarAsync();

            return PedidoResponseModel.De(pedido);
        }

        public async Task<PedidoResponseModel> RemoverItemAsync(Guid pedidoId, Guid produtoId)
        {
            await _unitOfWork.BeginTransactionAsync();

            var pedido = await _unitOfWork.Pedido.ObterPorId(pedidoId);
            var item = pedido.Itens.FirstOrDefault(i => i.ProdutoId == produtoId)
                ?? throw new Exception("Item não encontrado no pedido.");

            var produto = await _unitOfWork.Produto.ObterPorId(produtoId);
            var quantidade = item.Quantidade;

            pedido.RemoverItem(produtoId);      
            produto.ReporEstoque(quantidade);

            _unitOfWork.Produto.Atualizar(produto);
            _unitOfWork.Pedido.Atualizar(pedido);
            await _unitOfWork.SalvarAsync();

            return PedidoResponseModel.De(pedido);
        }
    }
}
