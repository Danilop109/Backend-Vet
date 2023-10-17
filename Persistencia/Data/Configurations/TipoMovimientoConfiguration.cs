using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TipoMovimientoConfiguration : IEntityTypeConfiguration<TipoMovimiento>
    {
        public void Configure(EntityTypeBuilder<TipoMovimiento> builder)
        {
            builder.ToTable("tipoMovimiento");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Descripcion)
            .HasColumnName("descripcion")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

        }
    }
}