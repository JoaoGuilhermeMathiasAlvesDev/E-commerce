using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ContextEcommerce contexto) :base(contexto)
        {
            
        }

        public Task<bool> ExisteProduto(string nome)
        {
            return _dbSet.AnyAsync(p => p.Nome.ToLower() == nome);
        }

        public Task<List<Produto>> ListaAtivos()
        {
            return _dbSet.AsNoTracking().Where(p => p.Ativo).ToListAsync();
        }
    }
}
