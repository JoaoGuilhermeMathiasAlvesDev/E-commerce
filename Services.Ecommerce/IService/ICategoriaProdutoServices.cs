using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface ICategoriaProdutoServices
    {
        public interface ICategoriaProdutoService
        {
            Task<ProdutoResponseModel> AssociarAsync(Guid produtoId, Guid categoriaId);
            Task<ProdutoResponseModel> DesassociarAsync(Guid produtoId, Guid categoriaId);
            Task<ProdutoResponseModel> DefinirCategoriasAsync(Guid produtoId, DefinirCategoriasDoProdutoModel model);
            Task<IEnumerable<CategoriaResponseModel>> ObterCategoriasDoProdutoAsync(Guid produtoId);
            Task<IEnumerable<ProdutoResponseModel>> ObterProdutosDaCategoriaAsync(Guid categoriaId);
        }
    }
}
