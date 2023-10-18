using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MedicamentoProveedorRepository : GenericRepository<MedicamentoProveedor>, IMedicamentoProveedor
    {
        private readonly ApiJwtContext _context;

        public MedicamentoProveedorRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<MedicamentoProveedor>> GetAllAsync()
        {
            return await _context.MedicamentoProveedores
                .Include(m => m.Proveedor)
                .Include(m => m.Medicamento)
                .ToListAsync();
        }

        public override async Task<MedicamentoProveedor> GetByIdAsync(int id)
        {
            return await _context.MedicamentoProveedores
            .Include(m => m.Proveedor)
            .Include(m => m.Medicamento)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}