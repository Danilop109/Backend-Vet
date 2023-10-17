using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class DetalleMovimientoConfiguration : IEntityTypeConfiguration<DetalleMovimiento>
    {
        public void Configure(EntityTypeBuilder<DetalleMovimiento> builder)
        {
            builder.ToTable("detalleMovimiento");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Cantidad)
            .HasColumnName("cantidad")
            .HasColumnType("int")
            .IsRequired();

            builder.Property(p => p.Precio)
            .HasColumnName("precio")
            .HasColumnType("double")
            .IsRequired();

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.DetalleMovimientos)
            .HasForeignKey(p => p.IdMedicamentoFk);

            builder.HasOne(p => p.MovimientoMedicamento)
            .WithMany(p => p.DetalleMovimientos)
            .HasForeignKey(p => p.IdMovimientoMedicamentoFk);

        }
    }
}