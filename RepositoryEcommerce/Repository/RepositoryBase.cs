using DominioEcommerce.EntityBase;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        public Task Adicionar(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> ObterTodos()
        {
            throw new NotImplementedException();
        }

        public void Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
