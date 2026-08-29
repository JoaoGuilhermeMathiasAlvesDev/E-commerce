using DominioEcommerce.Entitidades;

namespace Services.Ecommerce.Models
{
    public record ClienteModel
    {
        public string Id { get; init; } = string.Empty;
        public string Nome { get; init; } = string.Empty;
        public string Sobrenome { get; init; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Email { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public EnderecoModel? Endereco { get; init; }

        public static ClienteModel ToModel(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            return new ClienteModel
            {
                Id = cliente.Id.ToString(),
                Nome = cliente.Nome,
                Sobrenome = cliente.SobreNome,
                Email = cliente.Email,
                PhoneNumber = cliente.PhoneNumber,
                Endereco = cliente.Endereco == null ? null : new EnderecoModel
                {
                    Logradouro = cliente.Endereco.Logradouro,
                    Numero = cliente.Endereco.Numero,
                    Complemento = cliente.Endereco.Complemento,
                    Bairro = cliente.Endereco.Bairro,
                    Cidade = cliente.Endereco.Cidade,
                    Estado = cliente.Endereco.Estado,
                    Cep = cliente.Endereco.Cep
                }
            };
        }
    }
}