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
    public class MascotaRepository : GenericRepository<Mascota>, IMascota
    {
        private readonly ApiJwtContext _context;

        public MascotaRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Mascota>> GetAllAsync()
        {
            return await _context.Mascotas
                .Include(p=> p.Propietario)
                .Include(p=> p.Especie)
                .Include(p=> p.Raza)
                .ToListAsync();
        }

        public override async Task<Mascota> GetByIdAsync(int id)
        {
            return await _context.Mascotas
                .Include(p=> p.Propietario)
                .Include(p=> p.Especie)
                .Include(p=> p.Raza)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}