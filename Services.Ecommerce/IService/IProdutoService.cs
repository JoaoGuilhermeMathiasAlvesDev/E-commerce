using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface IProdutoService
    {
        Task<ProdutoResponseModel> AdicionarAsync(RegistrarProdutoModel model);
        Task<ProdutoResponseModel> AtualizarAsync(Guid id, AtualizarProdutoModel model);
        Task<ProdutoResponseModel> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ProdutoResponseModel>> ObterTodosAsync();
        Task AtivarAsync(Guid id);
        Task DesativarAsync(Guid id);
        Task<ProdutoResponseModel> ReporEstoqueAsync(Guid id, AjustarEstoqueModel model);
        Task<ProdutoResponseModel> DebitarEstoqueAsync(Guid id, AjustarEstoqueModel model);
    }
}
