using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.EntityBase
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public DateTime DataCadastro { get; set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }
    }
}
