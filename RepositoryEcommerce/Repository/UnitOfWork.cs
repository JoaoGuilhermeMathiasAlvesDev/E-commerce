using Microsoft.EntityFrameworkCore.Storage;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextEcommerce _contexto;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ContextEcommerce contexto)
        {
            _contexto = contexto;
           
        }

        public async Task BeginTransactionAsync()
        {
           if( _transaction == null ) 
                _transaction =  await _contexto.Database.BeginTransactionAsync();
        }

        public async Task<bool> CommitTransactionAsync()
        {
            try
            {
                var sucess = await _contexto.SaveChangesAsync() > 0; 
                if(_transaction  != null)
                {
                   await _transaction.CommitAsync();
                    await DisposeTransactionAsync();
                }

                return sucess;
            }
            catch (Exception)
            {

                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    await DisposeTransactionAsync(); 
                }
                return false;
            }
        }

        public async Task<int> CompleteAsync()
        {
           return await _contexto.SaveChangesAsync();
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                DisposeTransaction();
            }
        }

        private void DisposeTransaction()
        {
            _transaction?.Dispose();
            _transaction = null;
        }
        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null; 
            }
        }
    }
}
