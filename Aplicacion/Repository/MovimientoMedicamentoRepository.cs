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
    public class MovimientoMedicamentoRepository : GenericRepository<MovimientoMedicamento>, IMovimientoMedicamento
    {
        private readonly ApiJwtContext _context;

        public MovimientoMedicamentoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<MovimientoMedicamento>> GetAllAsync()
        {
            return await _context.MovimientoMedicamentos
                .Include(m => m.TipoMovimiento)
                .ToListAsync();
        }

        public override async Task<MovimientoMedicamento> GetByIdAsync(int id)
        {
            return await _context.MovimientoMedicamentos
            .Include(m => m.TipoMovimiento)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}