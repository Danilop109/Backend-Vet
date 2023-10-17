using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class MascotaConfiguration : IEntityTypeConfiguration<Mascota>
    {
        public void Configure(EntityTypeBuilder<Mascota> builder)
        {
             builder.ToTable("mascota");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

            builder.Property(p => p.FechaNacimiento)
            .HasColumnName("fechaNacimiento")
            .HasColumnType("Date")
            .IsRequired();

            builder.HasOne(p => p.Propietario)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdPropietarioFk);

            builder.HasOne(p => p.Especie)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdEspecieFk);

            builder.HasOne(p => p.Raza)
            .WithMany(p => p.Mascotas)
            .HasForeignKey(p => p.IdRazaFk);
        }
    }
}