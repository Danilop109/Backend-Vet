using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class MedicamentoProveedorConfiguration : IEntityTypeConfiguration<MedicamentoProveedor>
    {
        public void Configure(EntityTypeBuilder<MedicamentoProveedor> builder)
        {
            builder.ToTable("medicamentoProveedor");

            builder.Property(p => p.Id)
            .IsRequired();

            builder.HasOne(p => p.Medicamento)
            .WithMany(p => p.MedicamentoProveedores)
            .HasForeignKey(p => p.IdMedicamentoFk);

            builder.HasOne(p => p.Proveedor)
            .WithMany(p => p.MedicamentoProveedores)
            .HasForeignKey(p => p.IdProveedorFk);
        }
    }
}