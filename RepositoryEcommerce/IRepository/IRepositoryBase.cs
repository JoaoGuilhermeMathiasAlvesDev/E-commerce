using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task Adicionar(TEntity obj);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        void Atualizar(TEntity obj);
        void Remover(Guid id);
    }
}
