using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface IPedidoService
    {
        Task<PedidoResponseModel> CriarAsync(CriarPedidoModel model);
        Task<PedidoResponseModel> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PedidoResponseModel>> ObterPorClienteAsync(Guid clienteId);
        Task<PedidoResponseModel> AdicionarItemAsync(Guid pedidoId, ItemPedidoModel model);
        Task<PedidoResponseModel> RemoverItemAsync(Guid pedidoId, Guid produtoId);
        Task<PedidoResponseModel> PagarAsync(Guid id);
        Task<PedidoResponseModel> CancelarAsync(Guid id);
    }
}
