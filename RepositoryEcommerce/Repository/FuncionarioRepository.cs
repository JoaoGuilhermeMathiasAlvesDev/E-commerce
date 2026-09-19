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
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly UserManager<Usuario> _userManager;
        private ContextEcommerce contexto;
        public FuncionarioRepository(ContextEcommerce contexto)
        {
            this.contexto = contexto;
        }

        public FuncionarioRepository(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

    
        public async Task<List<Funcionario>> Listar()
        {
           return await _userManager.Users.OfType<Funcionario>().AsTracking().ToListAsync();

        }

        public async Task<List<Funcionario>> ListarAtivos()
        {
            return await _userManager.Users.OfType<Funcionario>().Where(f => f.Ativo).AsTracking().ToListAsync();
        }

        public async Task<Funcionario> ObterPorEmail(string email)
        {
            return await _userManager.Users.OfType<Funcionario>().AsTracking()
                .FirstOrDefaultAsync(f => f.Email == email);
        }

        public async Task<Funcionario> ObterPorId(Guid id)
        {
           return await _userManager.Users.OfType<Funcionario>().AsTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<string>> ObterTodosEmails()
        {
            return await _userManager.Users.OfType<Funcionario>().Select(c => c.Email).AsTracking().ToListAsync();
        }

        public async Task<string> ObterUltimaMatricula()
        {
            return await _userManager.Users.OfType<Funcionario>().AsTracking()
                .OrderByDescending(f => f.Matricula)
                .Select(f => f.Matricula)
                .FirstOrDefaultAsync();
        }
    }
}
