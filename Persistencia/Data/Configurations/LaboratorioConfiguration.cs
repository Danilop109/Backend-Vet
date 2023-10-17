using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class LaboratorioConfiguration : IEntityTypeConfiguration<Laboratorio>
    {
        public void Configure(EntityTypeBuilder<Laboratorio> builder)
        {
            builder.ToTable("laboratorio");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.Direccion)
            .HasColumnName("direccion")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.Telefono)
            .HasColumnName("telefono")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}