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

        public override async Task<(int totalRegistros, IEnumerable<Cita> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
        {
            var query = _context.Citas as IQueryable<Cita>;

            if (!string.IsNullOrEmpty(search.ToString()))
            {
                query = query.Where(p => p.IdVeterinarioFk == search);
            }

            query = query.OrderBy(p => p.Id);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndez - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        //CONSULTA A-6 : Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
        public async Task<IEnumerable<object>> GetPetMotiveDate()
        {
            DateOnly uno = new DateOnly(2023, 01, 01);
            DateOnly tres = new DateOnly(2023, 03, 31);
            var objeto = from c in _context.Citas
                         join m in _context.Mascotas on c.IdMascotaFk equals m.Id
                         where c.Motivo == "Vacunacion" && c.Fecha >= uno && c.Fecha <= tres
                         select new
                         {
                            Id= c.Id,
                             Nombre = m.Nombre,
                             Fecha = c.Fecha,
                             Motivo = c.Motivo
                         };
            return await objeto.ToListAsync();
        }

        public async Task<(int totalRegistros, IEnumerable<object> registros)> GetPetMotiveDate(int pageIndex, int pageSize, string search)
        {
            DateOnly uno = new DateOnly(2023, 01, 01);
            DateOnly tres = new DateOnly(2023, 03, 31);
            var query = from c in _context.Citas
                         join m in _context.Mascotas on c.IdMascotaFk equals m.Id
                         where c.Motivo == "Vacunacion" && c.Fecha >= uno && c.Fecha <= tres
                         select new
                         {
                            Id= c.Id,
                             Nombre = m.Nombre,
                             Fecha = c.Fecha,
                             Motivo = c.Motivo
                         };

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Nombre);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }
    }
}