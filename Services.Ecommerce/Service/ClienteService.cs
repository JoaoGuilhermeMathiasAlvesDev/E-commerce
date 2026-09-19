using DominioEcommerce.DominioException;
using DominioEcommerce.Entitidades;
using DominioEcommerce.ValueObjects;
using Microsoft.AspNetCore.Http;
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
    public class ClienteService : IClienteService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IClienteRepository _clienteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClienteService(UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor, IClienteRepository clienteRepository)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _clienteRepository = clienteRepository;
        }

        public async Task AdicionarOuAtualizarEnderecoAsync(string clienteId, EnderecoModel endereco)
        {
            var clienteExiste = await _userManager.FindByIdAsync(clienteId);
            if (clienteExiste == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            var alterarEndereco = new Endereco(
                    endereco.Logradouro,
                    endereco.Numero,
                    endereco.Complemento,
                    endereco.Bairro,
                    endereco.Cidade,
                    endereco.Estado,
                    endereco.Cep
             );

            var cliente = (Cliente)clienteExiste;
            cliente.AdicionarOuAtualizarEndereco(alterarEndereco);

            var sucesso = await _userManager.UpdateAsync(cliente);

            if (!sucesso.Succeeded)
            {
                var erros = string.Join(" | ", sucesso.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao atualizar endereço: {erros}");
            }

        }

        public Task AdicionarPedidoAsync(string clienteId, Pedido pedido)
        {
            throw new NotImplementedException();
        }


        public async Task AtualizarAsync(ClienteModel cliente)
        {
            var clienteExiste = await _userManager.FindByIdAsync(cliente.Id.ToString());
            if (clienteExiste == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            var clienteAtualizado = (Cliente)clienteExiste;

            clienteAtualizado.AtualizarDados(
                cliente.Nome,
                cliente.Sobrenome,
                cliente.DataNascimento,
                cliente.PhoneNumber
            );

            var sucesso = await _userManager.UpdateAsync(clienteAtualizado);

            if (!sucesso.Succeeded)
            {
                var erros = string.Join(" | ", sucesso.Errors.Select(e => e.Description));
                throw new Exception($"Erro ao atualizar endereço: {erros}");
            }

        }

        public async Task<ClienteModel> CriarAsync(CriarClienteModel model)
        {
            var emailsRegistrados = await _clienteRepository.ObterTodosEmails();

            var verificarEmail = Usuario.VerificarSerExisteEmail(model.Email, emailsRegistrados);

            if (verificarEmail)
                throw new DominioException("E-mail já registrado.",
                             new List<string> { "E-mail já registrado." });

            var endereco = new Endereco(
                    model.Endereco.Logradouro,
                    model.Endereco.Numero,
                    model.Endereco.Complemento,
                    model.Endereco.Bairro,
                    model.Endereco.Cidade,
                    model.Endereco.Estado,
                    model.Endereco.Cep
                );


             var cliente = new Cliente(
                   model.Nome,
                   model.Sobrenome,
                   model.DataNascimento,
                   model.Email,
                   model.PhoneNumber,
                   model.Senha,
                   endereco
                );

            var sucesso = await _userManager.CreateAsync(cliente, model.Senha);

            if (!sucesso.Succeeded)
            {
                var erros = sucesso.Errors.Select(e => e.Description).ToList();
                throw new DominioException("Erro ao cadastrar cliente.", erros);
            }

            return ClienteModel.ToModel(cliente);

        }

        public async Task<ClienteModel> ObterClienteLogadoAsync()
        {
            var usuarioClaims = _httpContextAccessor.HttpContext?.User;

            if (usuarioClaims == null || !usuarioClaims.Identity.IsAuthenticated)
            {
                throw new DominioException("Usuário não autenticado.",
                    new List<string> { "Usuário não autenticado." });
            }

            var clienteLogado = await _userManager.GetUserAsync(usuarioClaims);

            if (clienteLogado == null)
            {
                throw new DominioException("Cliente não encontrado.",
                    new List<string> { "Cliente não encontrado." });
            }

            var cliente = clienteLogado as Cliente;
            if (cliente == null)
            {
                throw new DominioException("Usuário não é um cliente.",
                    new List<string> { "Usuário não é um cliente." });
            }

            return ClienteModel.ToModel(cliente);
        }

        public async Task<ClienteModel> ObterPorIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new DominioException("Id do cliente é obrigatório.",
                    new List<string> { "Id do cliente é obrigatório." });
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                throw new DominioException("Cliente não encontrado.",
                    new List<string> { "Cliente não encontrado." });
            }

            var cliente = usuario as Cliente;
            if (cliente == null)
            {
                throw new DominioException("Usuário não é um cliente.",
                    new List<string> { "Usuário não é um cliente." });
            }

            return ClienteModel.ToModel(cliente);
        }

        public async Task<IEnumerable<ClienteModel>> ObterTodosAsync()
        {
            var clientes = await _userManager.Users
                .OfType<Cliente>().AsNoTracking()
                .ToListAsync();

            return clientes.Select(ClienteModel.ToModel);
        }
    }
}
