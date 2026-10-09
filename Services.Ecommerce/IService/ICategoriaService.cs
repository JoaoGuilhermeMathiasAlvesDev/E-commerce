using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface ICategoriaService
    {
        Task<CategoriaResponseModel> Adicionar(RegistrarCategoriaModel model);
        Task<CategoriaResponseModel> Atualizar(Guid id, AtualizarCategoriaModel model);
        Task AtivarAsync(Guid id);
        Task DesativarAsync(Guid id);
        Task<CategoriaResponseModel> ObterPorId(Guid id);
        Task<IEnumerable<CategoriaResponseModel>> ObterTodos();
    }
}
