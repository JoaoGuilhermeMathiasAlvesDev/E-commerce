using DominioEcommerce.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class CriarPedidoModel
    {
        public Guid ClienteId { get; set; }
        public decimal ValorFrete { get; set; }
        public MetodoPagamento MetodoPagamento { get; set; }
        public string? ObservacoesEntrega { get; set; }
        public List<ItemPedidoModel> Itens { get; set; } = new();
    }
}
