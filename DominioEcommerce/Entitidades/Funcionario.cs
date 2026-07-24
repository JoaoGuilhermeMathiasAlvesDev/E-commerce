using DominioEcommerce.Enum;
using DominioEcommerce.ValueObjects;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Funcionario : Usuario
    {
        public string Matricula { get; private set; }

        public Endereco Endereco { get; private set; }

        public Funcionario() : base() 
        {
            
        }

        public Funcionario(string nome, string sobreNome, DateTime dataNascimento, string email,
            string phoneNumber, string senha, string matricula,Endereco endereco)
            : base(nome, sobreNome, dataNascimento, email, (int)RoleUsuario.Cliente, phoneNumber, senha)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new DominioException.DominioException("Matrícula é obrigatória.",
                    new List<string> { "Matrícula é obrigatória." });

            AdicionarOuAtualizarEndereco(endereco);
        }

        public void CriarMatricula(string matricula,string ultimaMatricula)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new DominioException.DominioException("Matrícula é obrigatória.",
                    new List<string> { "Matrícula é obrigatória." });
            Matricula = matricula.Trim();
        }


        public void AdicionarOuAtualizarEndereco(Endereco endereco)
        {
            if (endereco == null)
            {
                throw new DominioException.DominioException("Endereço não pode ser nulo.",
                    new List<string> { "Endereço não pode ser nulo." });
            }
            Endereco = endereco;
        }
    }
}
