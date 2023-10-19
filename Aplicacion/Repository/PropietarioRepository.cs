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
    public class PropietarioRepository : GenericRepository<Propietario>, IPropietario
    {
        private readonly ApiJwtContext _context;

        public PropietarioRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<Propietario>> GetAllAsync()
        {
            return await _context.Propietarios
                .ToListAsync();
        }

        public override async Task<Propietario> GetByIdAsync(int id)
        {
            return await _context.Propietarios
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<(int totalRegistros, IEnumerable<Propietario> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Propietarios as IQueryable<Propietario>;

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

        //CONSULTA A-4: Listar los propietarios y sus mascotas.
        public async Task<IEnumerable<object>> GetPetPer()
        {
            var objeto = from p in _context.Propietarios
            select new{
                    Nombre= p.Nombre,
                    Correo = p.Correo,
                    Telefono= p.Telefono,
                
                mascotas = (
                    from m in _context.Mascotas
                    where p.Id == m.IdPropietarioFk
                    select new {
                        Nombre = m.Nombre,
                        Fecha= m.FechaNacimiento
                    }
                ).ToList()
            };
            return await objeto.ToListAsync();
        }

        public async Task<(int totalRegistros, IEnumerable<object> registros)> GetPetPer(int pageIndex, int pageSize, string search)
        {
            var query = from p in _context.Propietarios
            select new{
                    Nombre= p.Nombre,
                    Correo = p.Correo,
                    Telefono= p.Telefono,
                
                mascotas = (
                    from m in _context.Mascotas
                    where p.Id == m.IdPropietarioFk
                    select new {
                        Nombre = m.Nombre,
                        Fecha= m.FechaNacimiento
                    }
                ).ToList()
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