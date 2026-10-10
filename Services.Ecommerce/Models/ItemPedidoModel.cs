using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class ItemPedidoModel
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal TotalItem { get; set; }
    }
}
