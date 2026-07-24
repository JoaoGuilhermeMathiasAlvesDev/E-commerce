using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

           
            builder.Property(p => p.NomeCliente)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.TelefoneCliente)
                   .HasMaxLength(20);

            builder.Property(p => p.EmailCliente)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Subtotal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ValorFrete)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Ignore(p => p.Total);

            // Enums mapeados
            builder.Property(p => p.MetodoPagamento)
                   .IsRequired();

            builder.Property(p => p.Status)
                   .IsRequired();

            builder.Property(p => p.ObservacoesEntrega)
                   .HasMaxLength(500);

            // -------------------------------------------------------------
            // MAPEMENTO DO VALUE OBJECT: EnderecoEntrega
            // -------------------------------------------------------------
            builder.OwnsOne(p => p.EnderecoEntrega, endereco =>
            {
                endereco.Property(e => e.Logradouro)
                        .HasColumnName("Endereco_Logradouro")
                        .IsRequired()
                        .HasMaxLength(150);

                endereco.Property(e => e.Numero)
                        .HasColumnName("Endereco_Numero")
                        .IsRequired()
                        .HasMaxLength(20);

                endereco.Property(e => e.Complemento)
                        .HasColumnName("Endereco_Complemento")
                        .HasMaxLength(100);

                endereco.Property(e => e.Bairro)
                        .HasColumnName("Endereco_Bairro")
                        .IsRequired()
                        .HasMaxLength(100);

                endereco.Property(e => e.Cidade)
                        .HasColumnName("Endereco_Cidade")
                        .IsRequired()
                        .HasMaxLength(100);

                endereco.Property(e => e.Estado)
                        .HasColumnName("Endereco_Estado")
                        .IsRequired()
                        .HasMaxLength(2);

                endereco.Property(e => e.Cep)
                        .HasColumnName("Endereco_Cep")
                        .IsRequired()
                        .HasMaxLength(10);
            });

            // -------------------------------------------------------------
            // RELACIONAMENTO E ENCAPSULAMENTO DE _itens (ItemPedido)
            // -------------------------------------------------------------
            builder.HasMany(p => p.Itens)
                   .WithOne()
                   .HasForeignKey("PedidoId") 
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            // Informa ao EF Core para acessar a coleção privada '_itens' diretamente
            builder.Metadata.FindNavigation(nameof(Pedido.Itens))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(p => p.Cliente)
                   .WithMany()
                   .HasForeignKey(p => p.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasIndex(p => p.ClienteId);
            builder.HasIndex(p => p.Status);
        }
    }
}
