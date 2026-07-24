using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            
            builder.Property(f => f.Matricula)
                   .HasMaxLength(50);

            builder.HasIndex(f => f.Matricula)
                   .IsUnique(false); 

            // -------------------------------------------------------------
            // MAPEAMENTO DO VALUE OBJECT: Endereco
            // -------------------------------------------------------------
            builder.OwnsOne(f => f.Endereco, endereco =>
            {
                endereco.Property(e => e.Logradouro)
                        .HasColumnName("Funcionario_Logradouro")
                        .HasMaxLength(150);

                endereco.Property(e => e.Numero)
                        .HasColumnName("Funcionario_Numero")
                        .HasMaxLength(20);

                endereco.Property(e => e.Complemento)
                        .HasColumnName("Funcionario_Complemento")
                        .HasMaxLength(100);

                endereco.Property(e => e.Bairro)
                        .HasColumnName("Funcionario_Bairro")
                        .HasMaxLength(100);

                endereco.Property(e => e.Cidade)
                        .HasColumnName("Funcionario_Cidade")
                        .HasMaxLength(100);

                endereco.Property(e => e.Estado)
                        .HasColumnName("Funcionario_Estado")
                        .HasMaxLength(2);

                endereco.Property(e => e.Cep)
                        .HasColumnName("Funcionario_Cep")
                        .HasMaxLength(10);
            });
        }
    }
}
