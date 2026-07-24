using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class ItenPedidoMapping : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.NomeProdutoNoMomentodaCompra)
                   .IsRequired()
                   .HasMaxLength(150);

      
            builder.Property(i => i.Quantidade)
                   .IsRequired();

            
            builder.Property(i => i.PrecoUnitario)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Ignore(i => i.TotalItem);

   
            builder.HasOne<Produto>()
                   .WithMany()
                   .HasForeignKey(i => i.ProdutoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(i => i.PedidoId)
                   .IsRequired();

            
            builder.HasIndex(i => i.PedidoId);
            builder.HasIndex(i => i.ProdutoId);
        }
    }
}
