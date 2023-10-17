using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class VeterinarioConfiguration : IEntityTypeConfiguration<Veterinario>
    {
        public void Configure(EntityTypeBuilder<Veterinario> builder)
        {
            builder.ToTable("veterinario");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(p => p.Correo)
            .HasColumnName("correo")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(p => p.Telefono)
            .HasColumnName("telefono")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(p => p.Especialidad)
            .HasColumnName("especialidad")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();
        }
    }
}