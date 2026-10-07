using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class RegistrarProdutoModel
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O estoque inicial não pode ser negativo.")]
        public int Estoque { get; set; }

        public string UrlImagem { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O peso deve ser maior que zero.")]
        public decimal PesoGramas { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "A altura deve ser maior que zero.")]
        public decimal AlturaCm { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "A largura deve ser maior que zero.")]
        public decimal LarguraCm { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O comprimento deve ser maior que zero.")]
        public decimal ComprimentoCm { get; set; }

        public List<Guid> CategoriaIds { get; set; } = new();
    }
}
