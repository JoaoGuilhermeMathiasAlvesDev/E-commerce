using DominioEcommerce.Enum;
using DominioEcommerce.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq; // 💡 Correção: Adicionado para liberar o FirstOrDefault, Any e Sum

namespace DominioEcommerce.Entitidades
{
    public class Pedido : EntityBase.EntityBase
    {
       
        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; } = null!; 

        public string NomeCliente { get; private set; } = string.Empty;
        public string TelefoneCliente { get; private set; } = string.Empty;
        public string EmailCliente { get; private set; } = string.Empty;

        public Endereco EnderecoEntrega { get; private set; } = null!;
        public decimal Subtotal { get; private set; }
        public decimal ValorFrete { get; private set; }
        public decimal Total => Subtotal + ValorFrete;

        public MetodoPagamento MetodoPagamento { get; private set; }
        public StatusPedidos Status { get; private set; } = StatusPedidos.Pendente;
        public string? ObservacoesEntrega { get; private set; }

        private readonly List<ItemPedido> _itens = new(); 
        public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();

        protected Pedido()
        {
         
        }

    
        public Pedido(Guid clienteId, string nomeCliente, string telefoneCliente, string emailCliente,
              Endereco enderecoEntrega, decimal valorFrete, MetodoPagamento metodoPagamento, string? observacoesEntrega = null)
        {
            ValidarEInicializar(clienteId, nomeCliente, telefoneCliente, emailCliente, enderecoEntrega, valorFrete, metodoPagamento, observacoesEntrega);
        }

        private void ValidarEInicializar(Guid clienteId, string nomeCliente, string telefoneCliente, string emailCliente,
                                         Endereco enderecoEntrega, decimal valorFrete, MetodoPagamento metodoPagamento, string? observacoesEntrega)
        {
            if (clienteId == Guid.Empty)
                throw new DominioException.DominioException("O identificador do cliente é obrigatório.",
                    new List<string> { "Cliente inválido." });

            if (string.IsNullOrWhiteSpace(nomeCliente))
                throw new DominioException.DominioException("Nome do cliente é obrigatório.",
                    new List<string> { "Nome do cliente é obrigatório." });

            if (string.IsNullOrWhiteSpace(emailCliente))
                throw new DominioException.DominioException("E-mail do cliente é obrigatório.",
                    new List<string> { "E-mail do cliente é obrigatório." });

            if (enderecoEntrega == null)
                throw new DominioException.DominioException("O endereço de entrega é obrigatório.",
                    new List<string> { "O endereço de entrega é obrigatório." });
            
            ClienteId = clienteId; 
            NomeCliente = nomeCliente.Trim();
            TelefoneCliente = telefoneCliente?.Trim() ?? string.Empty;
            EmailCliente = emailCliente.Trim();
            EnderecoEntrega = enderecoEntrega;
            AtualizarFrete(valorFrete);
            MetodoPagamento = metodoPagamento;
            ObservacoesEntrega = observacoesEntrega?.Trim();

            Status = StatusPedidos.Pendente;
            Subtotal = 0;
        }

        public void AtualizarFrete(decimal novoValorFrete)
        {
            if (Status != StatusPedidos.Pendente)
                throw new DominioException.DominioException("Não é possível alterar o frete de um pedido não pendente.",
                    new List<string> { "Pedido não está pendente." });

            if (novoValorFrete < 0)
                throw new DominioException.DominioException("O valor do frete não pode ser negativo.",
                    new List<string> { "Valor do frete inválido." });

            ValorFrete = novoValorFrete;
        }


        public void AdicionarItem(ItemPedido item)
        {
            if (item == null)
                throw new DominioException.DominioException("O item do pedido não pode ser nulo.", new List<string> { "O item do pedido não pode ser nulo." });

            if (Status != StatusPedidos.Pendente)
                throw new DominioException.DominioException("Não é possível adicionar itens a um pedido que não está pendente.", new List<string> { "Pedido não está pendente." });

            var itemExistente = _itens.FirstOrDefault(i => i.ProdutoId == item.ProdutoId);
            if (itemExistente != null)
            {
                itemExistente.AtualizarQuantidade(itemExistente.Quantidade + item.Quantidade);
            }
            else
            {
                _itens.Add(item);
            }

            CalcularSubtotal();
        }

        public void RemoverItem(Guid produtoId)
        {
            if (Status != StatusPedidos.Pendente)
                throw new DominioException.DominioException("Não é possível remover itens de um pedido que não está pendente.", new List<string> { "Pedido não está pendente." });

            var item = _itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                _itens.Remove(item);
                CalcularSubtotal();
            }
        }

        public void Pagar()
        {
            if (Status != StatusPedidos.Pendente)
                throw new DominioException.DominioException("Apenas pedidos pendentes podem ser pagos.", new List<string> { "Status inválido para pagamento." });

            if (!_itens.Any())
                throw new DominioException.DominioException("Não é possível pagar um pedido sem itens.", new List<string> { "Pedido sem itens." });

            Status = StatusPedidos.Pago;
        }

        public void Cancelar()
        {
            if (Status == StatusPedidos.Entregue)
                throw new DominioException.DominioException("Não é possível cancelar um pedido que já foi entregue.", new List<string> { "Pedido já entregue." });

            Status = StatusPedidos.Cancelado;
        }

        private void CalcularSubtotal()
        {
            Subtotal = _itens.Sum(item => item.TotalItem);
        }
    }
}