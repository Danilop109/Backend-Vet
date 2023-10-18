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
    }
}