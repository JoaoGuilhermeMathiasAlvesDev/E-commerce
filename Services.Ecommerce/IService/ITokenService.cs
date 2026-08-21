using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.IService
{
    public interface ITokenService
    {
        Task<string> GerarToken(Usuario usuario);
    }
}
