using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class MedicamentoConfiguration : IEntityTypeConfiguration<Medicamento>
    {
        public void Configure(EntityTypeBuilder<Medicamento> builder)
        {
            builder.ToTable("medicamento");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.CantidadDisponible)
            .HasColumnName("cantidadDisponible")
            .HasColumnType("int")
            .IsRequired();

            builder.HasOne(p => p.Laboratorio)
            .WithMany(p => p.Medicamentos)
            .HasForeignKey(p => p.IdLaboratorioFk);
        }
    }
}