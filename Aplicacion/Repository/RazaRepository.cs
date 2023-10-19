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

        public override async Task<(int totalRegistros, IEnumerable<Raza> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Razas as IQueryable<Raza>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Mascotas)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    //CONSULTA B-6:Listar la cantidad de mascotas que pertenecen a una raza a una raza. Nota: Se debe mostrar una lista de las razas y la cantidad de mascotas que pertenecen a la raza. 
        public virtual async Task<IEnumerable<object>> GetPetsByRaza()
        {
            var objeto = from r in _context.Razas
                         join m in _context.Mascotas on r.Id equals m.IdRazaFk into razaMascotas
                         select new
                         {
                             NombreRaza = r.Nombre,
                             CantidadMascotas = razaMascotas.Count()
                         };

            return await objeto.ToListAsync();
        }

         public async Task<(int totalRegistros,object registros)> GetPetsByRaza(int pageIndez, int pageSize, string search)
    {
        var query = from r in _context.Razas
                         join m in _context.Mascotas on r.Id equals m.IdRazaFk into razaMascotas
                         select new
                         {
                             NombreRaza = r.Nombre,
                             CantidadMascotas = razaMascotas.Count()
                         };
        
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreRaza.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.NombreRaza);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
    }
}