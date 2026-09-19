using DominioEcommerce.Entitidades;
using DominioEcommerce.Enum;
using Services.Ecommerce.Models;

public class FuncionarioResponseModel
{
    public Guid Id { get; set; }
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public string SobreNome { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Role { get; set; }
    public EnderecoModel Endereco { get; set; }

    public static FuncionarioResponseModel De(Funcionario f) => new FuncionarioResponseModel
    {
        Id = f.Id,
        Matricula = f.Matricula,
        Nome = f.Nome,
        SobreNome = f.SobreNome,
        Email = f.Email,
        PhoneNumber = f.PhoneNumber,
        DataNascimento = f.DataNascimento,
        Role = ((RoleUsuario)f.Role).ToString(),
        Endereco = new EnderecoModel
        {
            Logradouro = f.Endereco.Logradouro,
            Numero = f.Endereco.Numero,
            Complemento = f.Endereco.Complemento,
            Bairro = f.Endereco.Bairro,
            Cidade = f.Endereco.Cidade,
            Estado = f.Endereco.Estado,
            Cep = f.Endereco.Cep
        }
    };
}