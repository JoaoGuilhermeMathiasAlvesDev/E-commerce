using DominioEcommerce.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public abstract class Usuario : IdentityUser<Guid>
    {
        public string Nome { get; private set; } = string.Empty;
        public string SobreNome { get; private set; } = string.Empty;
        public DateTime DataNascimento { get; private set; }
        public RoleUsuario Role { get; private set; }
        public bool Ativo { get; private set; } = true;

        protected Usuario() : base()
        {

        }

        protected Usuario(string nome, string sobreNome, DateTime dataNascimento, string email, int role, string phoneNumber, string senha)
        {
            ValidarEInicializar(nome, sobreNome, dataNascimento, role, email, phoneNumber, senha);
        }

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        private void ValidarEInicializar(string nome, string sobreNome, DateTime dataNascimento, int role, string email, string phoneNumber, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DominioException.DominioException("Nome é obrigatório.",
                    new List<string> { "Nome é obrigatório." });
            if (string.IsNullOrWhiteSpace(sobreNome))
                throw new DominioException.DominioException("Sobrenome é obrigatório.",
                    new List<string> { "Sobrenome é obrigatório." });
            if (dataNascimento > DateTime.Now)
                throw new DominioException.DominioException("Data de nascimento inválida.",
                    new List<string> { "Data de nascimento inválida." });
            if (string.IsNullOrWhiteSpace(email))
                throw new DominioException.DominioException("E-mail é obrigatório.",
                    new List<string> { "E-mail é obrigatório." });
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DominioException.DominioException("Número de telefone é obrigatório.",
                    new List<string> { "Número de telefone é obrigatório." });
            if (string.IsNullOrWhiteSpace(senha))
                throw new DominioException.DominioException("Senha é obrigatória.",
                    new List<string> { "Senha é obrigatória." });

            ValidarRole(role);

            Nome = nome;
            SobreNome = sobreNome;
            DataNascimento = dataNascimento;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = (RoleUsuario)role;
            var emailFormatado = email.Trim().ToLower();
            Email = emailFormatado;
            UserName = emailFormatado;
        }

        public void AtualizarRole(int role)
        {
            ValidarRole(role);
            Role = (RoleUsuario)role;
        }

        private static void ValidarRole(int role)
        {
            if (!System.Enum.IsDefined(typeof(RoleUsuario), role))
            {
                throw new DominioException.DominioException("Role inválida.",
                    new List<string> { "Role inválida." });
            }
        }
    }
}
