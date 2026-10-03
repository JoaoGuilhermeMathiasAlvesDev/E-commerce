using DominioEcommerce.Entitidades;
using RepositoryEcommerce.Context;
using RepositoryEcommerce.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Repository
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ContextEcommerce context) : base(context)
        {
            
        }
    }
}
