using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class DefinirCategoriasDoProdutoModel
    {
        public List<Guid> CategoriaIds { get; set; } = new();
    }
}
