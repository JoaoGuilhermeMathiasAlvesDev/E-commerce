using DominioEcommerce.Entitidades;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface IFuncionarioSerivice
    {
        Task<Funcionario> ObterPorIdAsync(Guid id);
        Task<IEnumerable<FuncionarioResponseModel>> ListarAsync();
        Task<FuncionarioResponseModel> AdicionarAsync(RegistrarFuncionarioModel funcionario);
        void Atualizar(RegistrarFuncionarioModel funcionario);
        Task<int> SalvarAsync();
    }
}
