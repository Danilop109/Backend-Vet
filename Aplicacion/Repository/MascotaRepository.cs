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

        //CONSULTA A-3: Mostrar las mascotas que se encuentren registradas cuya especie sea felina.
        
        public async Task<IEnumerable<Mascota>> GetPetEspecie()
        {
            return await (
                from m in _context.Mascotas 
                join e in _context.Especies on m.IdEspecieFk equals e.Id
                where e.Nombre == "Felina"
                select m
            ).ToListAsync();
        }
        public async Task<IEnumerable<Mascota>> GetPetEspecie2()
        {
            return await _context.Mascotas
            .Where(m => m.IdEspecieFk == 1)
            .ToListAsync();
        }
    }
}