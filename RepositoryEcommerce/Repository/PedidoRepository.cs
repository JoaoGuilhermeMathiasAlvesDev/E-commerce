using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Pedido?>> ObterPedidosClienteAsync(Guid clienteId)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.ClienteId == clienteId)
                .ToListAsync();
        }
    }
}
