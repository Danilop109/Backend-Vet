using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> builder)
        {
            builder.ToTable("proveedor");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(p => p.Direccion)
            .HasColumnName("direccion")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.Property(p => p.Telefono)
            .HasColumnName("telefono")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();
        }
    }
}