using DominioEcommerce.Entitidades;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class CategoriaProdutoRepository : RepositoryBase<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(ContextEcommerce context) : base(context)
        {
            
        }
    }
}
