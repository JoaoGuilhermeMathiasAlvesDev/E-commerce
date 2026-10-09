using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterPorIdsAsync(IEnumerable<Guid> ids);
    }
}
