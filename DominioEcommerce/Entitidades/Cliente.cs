using DominioEcommerce.Enum;
using DominioEcommerce.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class Cliente : Usuario
    {
        private List<Pedido> _pedidos = new();
        public Endereco Endereco { get; private set; }

        public IReadOnlyCollection<Pedido> Pedidos => _pedidos.AsReadOnly();

        public Cliente() : base() 
        {
            
        }

        public Cliente(string nome, string sobreNome, DateTime dataNascimento, string email,
            string phoneNumber, string senha, Endereco endereco)
            : base(nome, sobreNome, dataNascimento, email, (int)RoleUsuario.Cliente, phoneNumber, senha)
        {
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

        public void AdicionarPedido(Pedido pedido)
        {
            if (pedido is null)
            {
                throw new DominioException.DominioException("O pedido não pode ser nulo.",
                    new List<string> { "O pedido não pode ser nulo." });
            }

            _pedidos.Add(pedido);
        }
    }
}
