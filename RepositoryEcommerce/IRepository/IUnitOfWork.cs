using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IUnitOfWork
    {
        IFuncionarioRepository Funcionario { get; }
        IClienteRepository Cliente { get; }
        Task BeginTransactionAsync();
        Task<bool> CommitTransactionAsync();
        Task<int> CompleteAsync();
        void Rollback();
    }
}
