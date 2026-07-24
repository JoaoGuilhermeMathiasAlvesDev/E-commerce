using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task<int> CompleteAsync();
        void Rollback();
    }
}
