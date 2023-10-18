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
    public class RazaRepository : GenericRepository<Raza>, IRaza 
    {
        private readonly ApiJwtContext _context;

        public RazaRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<Raza>> GetAllAsync()
        {
            return await _context.Razas
                .Include(r => r.Especie)
                .ToListAsync();
        }

        public override async Task<Raza> GetByIdAsync(int id)
        {
            return await _context.Razas
            .Include(r => r.Especie)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}