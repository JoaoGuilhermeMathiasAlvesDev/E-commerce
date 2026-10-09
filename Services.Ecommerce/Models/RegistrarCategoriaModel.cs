using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Ecommerce.Models
{
    public class RegistrarCategoriaModel
    {
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
