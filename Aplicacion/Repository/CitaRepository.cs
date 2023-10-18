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
    public class CitaRepository : GenericRepository<Cita>, ICita
    {
        private readonly ApiJwtContext _context;

        public CitaRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Cita>> GetAllAsync()
        {
            return await _context.Citas
                .Include(p => p.Mascota)
                .ToListAsync();
        }

        public override async Task<Cita> GetByIdAsync(int id)
        {
            return await _context.Citas
            .Include(p => p.Mascota)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}