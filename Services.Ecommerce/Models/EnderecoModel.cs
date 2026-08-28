using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Ecommerce.Models
{
    public record EnderecoModel
    {
        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Número é obrigatório.")]
        public string Numero { get; set; } = string.Empty;

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Cidade é obrigatória.")]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "CEP inválido.")]
        public string Cep { get; set; } = string.Empty;
    }
}
