using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Produto : EntityBase.EntityBase
    {
        public string Nome { get; private set; } = string.Empty;
        public string Descricao { get; private set; } = string.Empty;
        public decimal Preco { get; private set; }
        public int Estoque { get; private set; }
        public string UrlImagem { get; private set; } = string.Empty;
        public bool Ativo { get; private set; } = true;

        public decimal PesoGramas { get; private set; }
        public decimal AlturaCm { get; private set; }
        public decimal LarguraCm { get; private set; }
        public decimal ComprimentoCm { get; private set; }

        private readonly List<CategoriaProduto> _categoriaProdutos = new();

        public IReadOnlyCollection<CategoriaProduto> CategoriaProdutos => _categoriaProdutos.AsReadOnly();

        protected Produto()
        {
        }

        public Produto(string nome, string descricao, decimal preco, int estoque, string urlImagem,
                       decimal pesoGramas, decimal alturaCm, decimal larguraCm, decimal comprimentoCm)
        {
            ValidarEInicializar(nome, descricao, preco, estoque, urlImagem, pesoGramas, alturaCm, larguraCm, comprimentoCm);
        }


        private void ValidarEInicializar(string nome, string descricao, decimal preco, int estoque, string urlImagem,
                                         decimal pesoGramas, decimal alturaCm, decimal larguraCm, decimal comprimentoCm)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DominioException.DominioException("O nome do produto não pode ser vazio.", new List<string> { "O nome do produto não pode ser vazio." });

            if (preco <= 0) throw new ArgumentOutOfRangeException(nameof(preco), "O preço deve ser maior que zero.");
            if (estoque < 0) throw new ArgumentOutOfRangeException(nameof(estoque), "O estoque inicial não pode ser negativo.");
            if (pesoGramas <= 0) throw new ArgumentOutOfRangeException(nameof(pesoGramas), "O peso deve ser maior que zero.");
            if (alturaCm <= 0) throw new ArgumentOutOfRangeException(nameof(alturaCm), "A altura deve ser maior que zero.");
            if (larguraCm <= 0) throw new ArgumentOutOfRangeException(nameof(larguraCm), "A largura deve ser maior que zero.");
            if (comprimentoCm <= 0) throw new ArgumentOutOfRangeException(nameof(comprimentoCm), "O comprimento deve ser maior que zero.");

            Nome = nome.Trim();
            Descricao = descricao?.Trim() ?? string.Empty;
            Preco = preco;
            Estoque = estoque;
            UrlImagem = urlImagem?.Trim() ?? string.Empty;
            PesoGramas = pesoGramas;
            AlturaCm = alturaCm;
            LarguraCm = larguraCm;
            ComprimentoCm = comprimentoCm;
        }


        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AtualizarPreco(decimal novoPreco)
        {
            if (novoPreco <= 0)
                throw new ArgumentOutOfRangeException(nameof(novoPreco), "O preço do produto deve ser maior que zero.");

            Preco = novoPreco;
        }

        public void ReporEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new DominioException.DominioException("Estoque insuficiente.",
                                         new List<string> { $"O produto {Nome} possui apenas {Estoque} unidades disponíveis." });

            Estoque += quantidade;
        }

        public void DebitarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade para débito deve ser maior que zero.");

            if (Estoque < quantidade)
            {
                throw new DominioException.DominioException("Estoque insuficiente.",
                    new List<string> { $"O produto {Nome} possui apenas {Estoque} unidades disponíveis." });
            }

            Estoque -= quantidade;
        }

        public void AdicionarCategoria(Guid categoriaId)
        {
            if (categoriaId == Guid.Empty)
                throw new ArgumentException("O ID da categoria não pode ser vazio.", nameof(categoriaId));
            if (_categoriaProdutos.Exists(cp => cp.CategoriaId == categoriaId))
                return;
            _categoriaProdutos.Add(new CategoriaProduto(categoriaId, this.Id));
        }

        public void AtualizarDadosPrincipais(string nome, string descricao, string urlImagem)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DominioException.DominioException("O nome do produto não pode ser vazio.", 
                                new List<string> { "O nome do produto não pode ser vazio." });
            Nome = nome.Trim();
            Descricao = descricao?.Trim() ?? string.Empty;
            UrlImagem = urlImagem?.Trim() ?? string.Empty;
        }
    }

}
