using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class MovimientoMedicamentoConfiguration : IEntityTypeConfiguration<MovimientoMedicamento>
    {
        public void Configure(EntityTypeBuilder<MovimientoMedicamento> builder)
        {
            builder.ToTable("movimientoMedicamento");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Fecha)
            .HasColumnName("fecha")
            .HasColumnType("Date")
            .IsRequired();

            builder.HasOne(p => p.TipoMovimiento)
            .WithMany(p => p.MovimientoMedicamentos)
            .HasForeignKey(p => p.IdTipoMovimientoFk);
        }
    }
}