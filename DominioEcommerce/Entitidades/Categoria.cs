using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Categoria : EntityBase.EntityBase
    {
        public string Nome { get; private set; } = string.Empty;
        public bool Ativo { get; private set; } = true;

        private readonly List<CategoriaProduto> _categoriaProdutos = new();

        public IReadOnlyCollection<CategoriaProduto> CategoriaProdutos => _categoriaProdutos.AsReadOnly();

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

        public void AdicionarCategoria(Guid categoriaId)
        {
            if (categoriaId == Guid.Empty)
                throw new ArgumentException("O ID da categoria não pode ser vazio.", nameof(categoriaId));

            if (_categoriaProdutos.Exists(cp => cp.CategoriaId == categoriaId))
                return;

            _categoriaProdutos.Add(new CategoriaProduto(categoriaId, this.Id));
        }

        public void RemoverCategoria(Guid categoriaId)
        {
            _categoriaProdutos.RemoveAll(cp => cp.CategoriaId == categoriaId);
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;
    }
}
