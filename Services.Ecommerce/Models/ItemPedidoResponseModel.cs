using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class ItemPedidoResponseModel
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal TotalItem { get; set; }
    }
}
