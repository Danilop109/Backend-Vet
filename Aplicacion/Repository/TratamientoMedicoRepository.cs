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
    public class TratamientoMedicoRepository : GenericRepository<TratamientoMedico>, ITratamientoMedico
    {
        private readonly ApiJwtContext _context;

        public TratamientoMedicoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<TratamientoMedico>> GetAllAsync()
        {
            return await _context.TratamientoMedicos
                .Include(t => t.Cita)
                .Include(t => t.Medicamento)
                .ToListAsync();
        }

        public override async Task<TratamientoMedico> GetByIdAsync(int id)
        {
            return await _context.TratamientoMedicos
            .Include(t => t.Cita)
            .Include(t => t.Medicamento)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRegistros, IEnumerable<TratamientoMedico> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
    {
        var query = _context.TratamientoMedicos as IQueryable<TratamientoMedico>;

        if (search != 0)
        {
            query = query.Where(p => p.IdCitaFk == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Cita)
            .Include(p => p.Medicamento)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}