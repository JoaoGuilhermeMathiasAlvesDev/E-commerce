using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class CategoriaProdutoMapping : IEntityTypeConfiguration<CategoriaProduto>
    {
        public void Configure(EntityTypeBuilder<CategoriaProduto> builder)
        {
            // Opcional: Define explicitamente o nome da tabela no banco
            builder.ToTable("CategoriasProdutos");

            // Chave primária composta
            builder.HasKey(cp => new { cp.CategoriaId, cp.ProdutoId });

            // Relacionamento com Categoria
            builder.HasOne(cp => cp.Categoria)
                .WithMany(c => c.CategoriaProdutos)
                .HasForeignKey(cp => cp.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Produto
            builder.HasOne(cp => cp.Produto)
                .WithMany(p => p.CategoriaProdutos)
                .HasForeignKey(cp => cp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
