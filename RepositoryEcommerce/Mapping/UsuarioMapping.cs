using DominioEcommerce.Entitidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryEcommerce.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
           
            builder.Property(u => u.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.SobreNome)
                   .IsRequired()
                   .HasMaxLength(100);

            
            builder.Property(u => u.DataNascimento)
                   .IsRequired();

            
            builder.Property(u => u.Role)
                   .IsRequired();

            builder.Property(u => u.Ativo)
                   .IsRequired()
                   .HasDefaultValue(true);
            
            builder.Property(u => u.Email)
                   .HasMaxLength(256);

            builder.Property(u => u.UserName)
                   .HasMaxLength(256);

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20);
        }
    }
}
