using DominioEcommerce.Entitidades;
using DominioEcommerce.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class PedidoResponseModel
    {
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal Total { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public StatusPedidos Status { get; set; }
        public string? ObservacoesEntrega { get; set; }
        public List<ItemPedidoResponseModel> Itens { get; set; } = new();

        public static PedidoResponseModel De(Pedido p) => new()
        {
            Id = p.Id,
            ClienteId = p.ClienteId,
            NomeCliente = p.NomeCliente,
            Subtotal = p.Subtotal,
            ValorFrete = p.ValorFrete,
            Total = p.Total,
            MetodoPagamento = p.MetodoPagamento,
            Status = p.Status,
            ObservacoesEntrega = p.ObservacoesEntrega,
            Itens = p.Itens.Select(i => new ItemPedidoResponseModel
            {
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade,
                TotalItem = i.TotalItem
            }).ToList()
        };
    }
}
