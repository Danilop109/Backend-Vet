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
                .Include(p => p.Propietario)
                .Include(p => p.Especie)
                .Include(p => p.Raza)
                .ToListAsync();
        }

        public override async Task<Mascota> GetByIdAsync(int id)
        {
            return await _context.Mascotas
                .Include(p => p.Propietario)
                .Include(p => p.Especie)
                .Include(p => p.Raza)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRegistros, IEnumerable<Mascota> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Mascotas as IQueryable<Mascota>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Include(p => p.Propietario)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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

        //CONSULTA B-1: Listar todas las mascotas agrupadas por especie. SOSPECHOSA

        public async Task<IEnumerable<object>> GetPetGropuByEspe()
        {
            var objeto = from e in _context.Especies
                         join m in _context.Mascotas on e.Id equals m.IdEspecieFk into grupoEspecie
                         select new
                         {
                             Nombre = e.Nombre,
                             mascotica = grupoEspecie.Select(m => new
                             {
                                 Nombre = m.Nombre,
                                 Fecha = m.FechaNacimiento
                             }).ToList()

                         };
            return await objeto.ToListAsync();
        }

        //CONSULTA B-3 : Listar las mascotas que fueron atendidas por un determinado veterinario.
        public async Task<IEnumerable<object>> GetPetForVet()
        {
            var consulta =
            from e in _context.Citas
            join v in _context.Veterinarios on e.IdVeterinarioFk equals v.Id
            select new
            {
                Veterinario = v.Nombre,
                Mascotas = (from c in _context.Citas
                            join m in _context.Mascotas on c.IdMascotaFk equals m.Id
                            where c.IdVeterinarioFk == v.Id
                            select new
                            {
                                NombreMascota = m.Nombre,
                                FechaNacimiento = m.FechaNacimiento,
                            }).ToList()
            };

            return await consulta.ToListAsync();
        }

        //CONSULTA B5: Listar las mascotas y sus propietarios cuya raza sea Golden Retriver

        public async Task<IEnumerable<object>> GetPetProRazaGoldenRetriever()
        {
            var objeto = from m in _context.Mascotas
                         join p in _context.Propietarios on m.IdPropietarioFk equals p.Id
                         join r in _context.Razas on m.IdRazaFk equals r.Id
                         where r.Nombre == "Golden Retriever"
                         select new
                         {
                             NombreMascota = m.Nombre,
                             NombrePropietario = p.Nombre,
                             Raza = r.Nombre
                         };
            return await objeto.ToListAsync();
        }

        public async Task<(int totalRegistros, IEnumerable<object> registros)> GetPetProRazaGoldenRetriever(int pageIndex, int pageSize, string search)
        {
            var query = from m in _context.Mascotas
                        join p in _context.Propietarios on m.IdPropietarioFk equals p.Id
                        join r in _context.Razas on m.IdRazaFk equals r.Id
                        where r.Nombre == "Golden Retriever"
                        select new
                        {
                            NombreMascota = m.Nombre,
                            NombrePropietario = p.Nombre,
                            Raza = r.Nombre
                        };

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreMascota.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.NombreMascota);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        

    }
}