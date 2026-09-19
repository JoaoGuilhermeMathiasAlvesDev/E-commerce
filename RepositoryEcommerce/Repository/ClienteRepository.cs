using DominioEcommerce.Entitidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private ContextEcommerce contexto;
        public ClienteRepository(ContextEcommerce contexto)
        {
            this.contexto = contexto;
        }
        public ClienteRepository(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<string>> ObterTodosEmails()
        {
            return await _userManager.Users.OfType<Cliente>().Select(c => c.Email).AsTracking().ToListAsync();
        }
    }
}
