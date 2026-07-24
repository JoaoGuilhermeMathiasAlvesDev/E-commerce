using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Categoria : EntityBase.EntityBase
    {
        public string Nome { get; private set; } = string.Empty;
        public bool Ativo { get; private set; } = true;

        private List<Produto> _produtos = new();

        public IReadOnlyCollection<Produto> Produtos => _produtos.AsReadOnly();

        protected Categoria()
        {

        }

        public Categoria(string nome, bool ativo)
        {
            criar(nome, ativo);
        }

        private void criar(string nome, bool ativo)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DominioException.DominioException("Nome da categoria não pode ser vazio.",
                    new List<string> { "Nome da categoria não pode ser vazio." });

            }

            Nome = nome;
            Ativo = ativo;
        }

        public void AdicionarProduto(Produto produto)
        {
            if (produto == null)
            {
                throw new DominioException.DominioException("Produto não pode ser nulo.",
                    new List<string> { "Produto não pode ser nulo." });
            }

            if (!_produtos.Contains(produto))
            {
                _produtos.Add(produto);
            }
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;
    }
}
