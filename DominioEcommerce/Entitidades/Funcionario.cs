using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Funcionario : Usuario
    {
        public string Matricula { get; private set; }


        public Funcionario(string nome, string sobreNome, DateTime dataNascimento, string email, string phoneNumber, string senha, string matricula)
            : base(nome, sobreNome, dataNascimento, email, phoneNumber, senha)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new DominioException.DominioException("Matrícula é obrigatória.",
                    new List<string> { "Matrícula é obrigatória." });
            Matricula = matricula.Trim();
        }

    }
}
