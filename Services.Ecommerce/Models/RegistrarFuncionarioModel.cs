using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public record RegistrarFuncionarioModel
    {
        string Nome;
        string SobreNome;
        DateTime DataNascimento;
        string Email;
        string PhoneNumber;
        string Senha;
        int Role;
        string Matricula;
    }
}
