using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class ProdutoResponseModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string UrlImagem { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public decimal PesoGramas { get; set; }
        public decimal AlturaCm { get; set; }
        public decimal LarguraCm { get; set; }
        public decimal ComprimentoCm { get; set; }
        public List<Guid> CategoriaIds { get; set; } = new();

        public static ProdutoResponseModel De(Produto produto) => new()
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            Estoque = produto.Estoque,
            UrlImagem = produto.UrlImagem,
            Ativo = produto.Ativo,
            PesoGramas = produto.PesoGramas,
            AlturaCm = produto.AlturaCm,
            LarguraCm = produto.LarguraCm,
            ComprimentoCm = produto.ComprimentoCm,
            CategoriaIds = produto.CategoriaProdutos.Select(cp => cp.CategoriaId).ToList()
        };
    }
}
