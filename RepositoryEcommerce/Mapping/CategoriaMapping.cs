using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Ativo)
                   .IsRequired()
                   .HasDefaultValue(true);

            // Relacionamento 1 : N com Produtos
            builder.HasMany(c => c.CategoriaProdutos)
              .WithOne(cp => cp.Categoria)
                .HasForeignKey(cp => cp.CategoriaId);
            
            // Indica ao EF Core para acessar via backing field
            builder.Navigation(c => c.CategoriaProdutos)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            // Índices de busca
            builder.HasIndex(c => c.Nome);
            builder.HasIndex(c => c.Ativo);
        }
    }
}
