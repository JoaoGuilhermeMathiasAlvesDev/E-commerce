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
            builder.HasMany(c => c.Produtos)
                   .WithOne() // Ou .WithOne(p => p.Categoria) se a entidade Produto tiver a propriedade Categoria
                   .HasForeignKey("CategoriaId")
                   .OnDelete(DeleteBehavior.Restrict);

            // Mapeamento para o backing-field privado _produtos
            builder.Metadata.FindNavigation(nameof(Categoria.Produtos))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Índices de busca
            builder.HasIndex(c => c.Nome);
            builder.HasIndex(c => c.Ativo);
        }
    }
}
