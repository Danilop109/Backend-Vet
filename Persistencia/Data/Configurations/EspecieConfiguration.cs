using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class EspecieConfiguration : IEntityTypeConfiguration<Especie>
    {
        public void Configure(EntityTypeBuilder<Especie> builder)
        {
            builder.ToTable("especie");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        }
    }
}