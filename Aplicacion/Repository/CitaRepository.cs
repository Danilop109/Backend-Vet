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
                .Include(p => p.Veterinario)
                .ToListAsync();
        }

        public override async Task<Cita> GetByIdAsync(int id)
        {
            return await _context.Citas
            .Include(p => p.Mascota)
            .Include(p => p.Veterinario)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        //CONSULTA A-6 : Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
        public async Task<IEnumerable<object>> GetPetMotiveDate()
        {
            DateOnly uno = new DateOnly(2023,01,01);
            DateOnly tres = new DateOnly(2023,01,01);
            var objeto = from c in _context.Citas
            join m in _context.Mascotas on c.IdMascotaFk equals m.Id
            where c.Motivo == "Vacunacion" && c.Fecha >= uno && c.Fecha<= tres
            select new 
            {
                Nombre = m.Nombre,
                Fecha = c.Fecha,
                Motivo = c.Motivo
            };
            return await objeto.ToListAsync();
        }
    }
}