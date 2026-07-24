using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            
            builder.HasKey(p => p.Id);

            
            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            
            builder.Property(p => p.Descricao)
                   .HasMaxLength(1000);

            
            builder.Property(p => p.Preco)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            
            builder.Property(p => p.Estoque)
                   .IsRequired();

            
            builder.Property(p => p.UrlImagem)
                   .HasMaxLength(500);

            
            builder.Property(p => p.Ativo)
                   .IsRequired()
                   .HasDefaultValue(true);

            
            builder.Property(p => p.PesoGramas)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.AlturaCm)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.LarguraCm)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ComprimentoCm)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            
            builder.HasOne(p => p.Categoria)
                   .WithMany()
                   .HasForeignKey(p => p.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(p => p.Nome);
            builder.HasIndex(p => p.CategoriaId);
        }
    }
}
