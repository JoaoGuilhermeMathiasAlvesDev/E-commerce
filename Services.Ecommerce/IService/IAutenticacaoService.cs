using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface IAutenticacaoService
    {
        Task<string> RegistrarClienteAsync(RegistrarClienteModel model);
        Task<string> RegistrarFuncionarioAsync(RegistrarFuncionarioModel model);
        Task<string> LoginAsync(Login model);
    }
}
