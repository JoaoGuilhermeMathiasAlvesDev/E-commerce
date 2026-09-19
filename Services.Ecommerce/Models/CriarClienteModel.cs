using Services.Ecommerce.Models;

public record CriarClienteModel
{
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Senha { get; set; }
    public EnderecoModel Endereco { get; set; }
}