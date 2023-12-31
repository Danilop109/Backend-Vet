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
    public class TipoMovimientoRepository: GenericRepository<TipoMovimiento>, ITipoMovimiento
    {
        private readonly ApiJwtContext _context;

        public TipoMovimientoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<TipoMovimiento>> GetAllAsync()
        {
            return await _context.TipoMovimientos
                .ToListAsync();
        }

        public override async Task<TipoMovimiento> GetByIdAsync(int id)
        {
            return await _context.TipoMovimientos
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRegistros, IEnumerable<TipoMovimiento> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.TipoMovimientos as IQueryable<TipoMovimiento>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Descripcion.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}