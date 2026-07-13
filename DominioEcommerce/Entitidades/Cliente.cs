using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Cliente : Usuario
    {
        private List<Pedido> _pedidos = new();

        public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();    

        public Cliente(string nome, string sobreNome, DateTime dataNascimento, string email, string phoneNumber, string senha)
            : base(nome, sobreNome, dataNascimento, email, phoneNumber, senha)
        {
        }
    }
}
