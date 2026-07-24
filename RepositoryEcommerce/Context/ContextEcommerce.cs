using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Context
{
    public class ContextEcommerce :  DbContext
    {
        public ContextEcommerce(DbContextOptions<ContextEcommerce> options) : base(options) { }
        public ContextEcommerce()
        {
            
        }

        public DbSet<Categoria>  Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<ItemPedido> itemPedidos { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContextEcommerce).Assembly);

        }

    }
}
