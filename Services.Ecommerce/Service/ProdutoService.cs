using DominioEcommerce.Entitidades;
using RepositoryEcommerce.IRepository;
using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProdutoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProdutoResponseModel> AdicionarAsync(RegistrarProdutoModel model)
        {
            var produto = new Produto(
                model.Nome,
                model.Descricao,
                model.Preco,
                model.Estoque,
                model.UrlImagem,
                model.PesoGramas,
                model.AlturaCm,
                model.LarguraCm,
                model.ComprimentoCm);

            foreach (var categoriaId in model.CategoriaIds.Distinct())
                produto.AdicionarCategoria(categoriaId);

            await _unitOfWork.Produto.Adicionar(produto);

            await _unitOfWork.SalvarAsync();

            return ProdutoResponseModel.De(produto);
        }

        public async Task AtivarAsync(Guid id)
        {
           var produto = await _unitOfWork.Produto.ObterPorId(id);
            if (produto == null)
                throw new Exception("Produto não encontrado.");

            produto.Ativar();
            _unitOfWork.Produto.Atualizar(produto);

            await _unitOfWork.SalvarAsync();

            return ;
        }

        public async Task<ProdutoResponseModel> AtualizarAsync(Guid id, AtualizarProdutoModel model)
        {
            var produto = await _unitOfWork.Produto.ObterPorId(id);

            produto.AtualizarDadosPrincipais(model.Nome, model.Descricao, model.UrlImagem);
            produto.AtualizarPreco(model.Preco);

            _unitOfWork.Produto.Atualizar(produto);
            
            await _unitOfWork.SalvarAsync();

            return ProdutoResponseModel.De(produto);
        }

        public async Task<ProdutoResponseModel> DebitarEstoqueAsync(Guid id, AjustarEstoqueModel model)
        {
            var produto = await _unitOfWork.Produto.ObterPorId(id);

            produto.DebitarEstoque(model.Quantidade);

            _unitOfWork.Produto.Atualizar(produto);

            await _unitOfWork.SalvarAsync();

            return ProdutoResponseModel.De(produto);    
        }

        public Task DesativarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProdutoResponseModel> ObterPorIdAsync(Guid id)
        {
            var produto = await _unitOfWork.Produto.ObterPorId(id);

            if(produto == null)
                throw new Exception("Produto não encontrado.");

            return ProdutoResponseModel.De(produto);
        }

        public async Task<IEnumerable<ProdutoResponseModel>> ObterTodosAsync()
        {
            var produtos =await _unitOfWork.Produto.ObterTodos();

            var produtosResponse = produtos.Select(p => ProdutoResponseModel.De(p)).ToList();
            return produtosResponse;
        }

        public async Task<ProdutoResponseModel> ReporEstoqueAsync(Guid id, AjustarEstoqueModel model)
        {
            var produto = await _unitOfWork.Produto.ObterPorId(id);
            produto.ReporEstoque(model.Quantidade);
            _unitOfWork.Produto.Atualizar(produto);

            await _unitOfWork.SalvarAsync();
            return ProdutoResponseModel.De(produto);
        }
    }
}
