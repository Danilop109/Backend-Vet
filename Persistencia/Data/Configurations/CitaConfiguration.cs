using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class CitaConfiguration : IEntityTypeConfiguration<Cita>
    {
        public void Configure(EntityTypeBuilder<Cita> builder)
        {
            builder.ToTable("cita");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Fecha)
            .HasColumnName("fecha")
            .HasColumnType("Date")
            .IsRequired();

            builder.Property(p => p.Hora)
            .HasColumnName("hora")
            .HasColumnType("DateTime")
            .IsRequired();

            builder.Property(p => p.Motivo)
            .HasColumnName("motivo")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

            builder.HasOne(p => p.Mascota)
            .WithMany(p => p.Citas)
            .HasForeignKey(p => p.IdMascotaFk);

            builder.HasOne(p => p.Veterinario)
            .WithMany(p => p.Citas)
            .HasForeignKey(p => p.IdVeterinarioFk);
        }
    }
}