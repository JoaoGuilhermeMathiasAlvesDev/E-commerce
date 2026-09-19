using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IClienteRepository 
    {
        Task<List<string>> ObterTodosEmails();

    }
}
