using DominioEcommerce.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public record RegistrarClienteModel
    {
        public string Nome { get; init; } = string.Empty;
        public string SobreNome { get; init; } = string.Empty;
        public DateTime DataNascimento { get; init; }
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string Senha { get; init; } = string.Empty;
        public Endereco Endereco { get; init; }

    }
}
