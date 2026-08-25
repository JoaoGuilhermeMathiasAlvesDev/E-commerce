using DominioEcommerce.EntityBase;
using Microsoft.EntityFrameworkCore;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly ContextEcommerce _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(ContextEcommerce context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task Adicionar(TEntity obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public void Atualizar(TEntity obj)
        {
             _dbSet.Update(obj);
        }

        public void Dispose()
        {
           _context?.Dispose();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
           return await _dbSet.AsNoTracking().ToListAsync();
        }

        public void Remover(Guid id)
        {
            var entity = _dbSet.Find(id);

            if(entity != null) 
              _dbSet.Remove(entity);
        }

    }
}
