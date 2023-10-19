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

        public override async Task<(int totalRegistros, IEnumerable<DetalleMovimiento> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
        {
            var query = _context.DetalleMovimientos as IQueryable<DetalleMovimiento>;

            if (!string.IsNullOrEmpty(search.ToString()))
            {
                query = query.Where(p => p.IdMovimientoMedicamentoFk == search);
            }

            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Include(p => p.MovimientoMedicamento)
                .Include(p => p.Medicamento)
                .Skip((pageIndez - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }
    }
}