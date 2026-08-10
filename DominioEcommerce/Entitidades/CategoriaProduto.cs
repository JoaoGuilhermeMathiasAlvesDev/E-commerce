using System;
using System.Collections.Generic;
using System.Text;

namespace DominioEcommerce.Entitidades
{
    public class CategoriaProduto : EntityBase.EntityBase
    {
        public Guid CategoriaId { get;private set; }
        public Guid ProdutoId { get; private set; }

        // Propriedades de navegação para o EF Core
        public virtual Categoria Categoria { get; private set; }
        public virtual Produto Produto { get; private set; }

        public CategoriaProduto()
        {
            
        }

        public CategoriaProduto(Guid categoria, Guid produto)
        {
            CategoriaId = categoria;
            ProdutoId = produto;    
        }
    }
}
