using Services.Ecommerce.IService;
using Services.Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Service
{
    public class AutenticacaoService : IAutenticacaoService
    {
        public Task<string> LoginAsync(Login model)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegistrarClienteAsync(RegistrarClienteModel model)
        {
            throw new NotImplementedException();
        }

        public Task<string> RegistrarFuncionarioAsync(RegistrarFuncionarioModel model)
        {
            throw new NotImplementedException();
        }
    }
}
