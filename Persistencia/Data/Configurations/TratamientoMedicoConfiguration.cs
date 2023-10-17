using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TratamientoMedicoConfiguration : IEntityTypeConfiguration<TratamientoMedico>
    {
        public void Configure(EntityTypeBuilder<TratamientoMedico> builder)
        {
            builder.ToTable("tratamientoMedico");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Dosis)
            .HasColumnName("dosis")
            .HasColumnType("varchar")
            .HasMaxLength(300)
            .IsRequired();

            builder.Property(p => p.FechaAdministracion)
            .HasColumnName("fechaAdministracion")
            .HasColumnType("DateTime")
            .IsRequired();

            builder.Property(p => p.Observacion)
            .HasColumnName("observacion")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.HasOne(p => p.Cita)
            .WithMany(p => p.TratamientoMedicos)
            .HasForeignKey(p => p.IdCitaFk);

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.TratamientoMedicos)
            .HasForeignKey(p => p.IdMedicamentoFk);
        }
    }
}