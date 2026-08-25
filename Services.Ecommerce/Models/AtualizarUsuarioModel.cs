using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class AtualizarUsuarioModel
    {
        [Required(ErrorMessage = "O Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Sobrenome é obrigatório.")]
        public string Sobrenome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório.")]
        public string PhoneNumber { get; set; } = string.Empty;

    }
}

