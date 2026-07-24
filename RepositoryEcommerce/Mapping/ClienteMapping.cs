using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // -------------------------------------------------------------
            // MAPEAMENTO DO VALUE OBJECT: Endereco
            // -------------------------------------------------------------
            builder.OwnsOne(c => c.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro)
                        .HasColumnName("Cliente_Logradouro")
                        .HasMaxLength(150);

                endereco.Property(e => e.Numero)
                        .HasColumnName("Cliente_Numero")
                        .HasMaxLength(20);

                endereco.Property(e => e.Complemento)
                        .HasColumnName("Cliente_Complemento")
                        .HasMaxLength(100);

                endereco.Property(e => e.Bairro)
                        .HasColumnName("Cliente_Bairro")
                        .HasMaxLength(100);

                endereco.Property(e => e.Cidade)
                        .HasColumnName("Cliente_Cidade")
                        .HasMaxLength(100);

                endereco.Property(e => e.Estado)
                        .HasColumnName("Cliente_Estado")
                        .HasMaxLength(2);

                endereco.Property(e => e.Cep)
                        .HasColumnName("Cliente_Cep")
                        .HasMaxLength(10);
            });

            // -------------------------------------------------------------
            // RELACIONAMENTO E ENCAPSULAMENTO DE _pedidos
            // -------------------------------------------------------------
            builder.HasMany(c => c.Pedidos)
                   .WithOne(p => p.Cliente)
                   .HasForeignKey(p => p.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Permite que o EF Core acesse o campo privado '_pedidos' diretamente
            builder.Metadata.FindNavigation(nameof(Cliente.Pedidos))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
