using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ContextEcommerce context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Categoria>> ObterPorIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Categorias.Where(c => ids.Contains(c.Id)).ToListAsync();  
        }
    }
}
