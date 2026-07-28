using DominioEcommerce.Entitidades;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class UsuarioRepository :  IUsuarioRepository
    {
        private readonly ContextEcommerce _context;
        public UsuarioRepository(ContextEcommerce context) 
        {
            
        }
    }
}
