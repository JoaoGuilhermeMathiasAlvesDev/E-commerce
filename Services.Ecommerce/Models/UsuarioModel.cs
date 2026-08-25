using DominioEcommerce.Entitidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public record UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string SobreNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public int Role { get; set; } 

        public UsuarioModel ToModel (Usuario usuario)
        {
            if (usuario == null)
                return null;

            return new UsuarioModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                SobreNome = usuario.SobreNome,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                Role = (int)usuario.Role,
            };
        }
    }
}
