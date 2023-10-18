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
    public class DetalleMovimientoRepository : GenericRepository<DetalleMovimiento>, IDetalleMovimiento
    {
        private readonly ApiJwtContext _context;

        public DetalleMovimientoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<DetalleMovimiento>> GetAllAsync()
        {
            return await _context.DetalleMovimientos
                .Include(p => p.Medicamento)
                .Include(p => p.MovimientoMedicamento)
                .ToListAsync();
        }

        public override async Task<DetalleMovimiento> GetByIdAsync(int id)
        {
            return await _context.DetalleMovimientos
            .Include(p => p.Medicamento)
            .Include(p => p.MovimientoMedicamento)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}