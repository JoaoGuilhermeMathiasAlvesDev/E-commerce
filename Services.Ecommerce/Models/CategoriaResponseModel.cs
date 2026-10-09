using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class CategoriaResponseModel
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public bool Ativo { get; private set; }

        public static CategoriaResponseModel De(Categoria categoria) => new()
        {
            Id = categoria.Id,
            Nome = categoria.Nome,
            Ativo = categoria.Ativo
        };
    }
}
