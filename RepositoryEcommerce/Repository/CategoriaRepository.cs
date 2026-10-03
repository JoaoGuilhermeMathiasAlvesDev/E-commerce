using DominioEcommerce.Entitidades;
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
    }
}
