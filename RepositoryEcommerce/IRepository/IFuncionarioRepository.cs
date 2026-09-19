using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.IRepository
{
    public interface IFuncionarioRepository 
    {
        Task<Funcionario> ObterPorId(Guid id);
        Task<Funcionario> ObterPorEmail(string email);
        Task<List<Funcionario>> Listar();
        Task<List<Funcionario>> ListarAtivos();

        Task<List<string>> ObterTodosEmails();
        Task<string> ObterUltimaMatricula();
    }
}
