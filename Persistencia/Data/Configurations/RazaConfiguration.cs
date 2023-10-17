using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class RazaConfiguration : IEntityTypeConfiguration<Raza>
    {
        public void Configure(EntityTypeBuilder<Raza> builder)
        {
            builder.ToTable("raza");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(150)
            .IsRequired();

            builder.HasOne(p => p.Especie)
            .WithMany(p => p.Razas)
            .HasForeignKey(p=> p.IdEspecieFk);
        }
    }
}