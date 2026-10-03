using DominioEcommerce.Entitidades;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class ItemPedidoRepository : RepositoryBase<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ContextEcommerce context) : base(context)
        {
        }   
    }
}
