using DominioEcommerce.Entitidades;
using DominioEcommerce.ValueObjects;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteModel>> ObterTodosAsync();
        Task<ClienteModel> ObterPorIdAsync(string id);
        Task<ClienteModel> ObterClienteLogadoAsync();
        Task AtualizarAsync(ClienteModel cliente);
        Task AdicionarOuAtualizarEnderecoAsync(string clienteId, EnderecoModel endereco);
        Task AdicionarPedidoAsync(string clienteId, Pedido pedido);
    }
}
