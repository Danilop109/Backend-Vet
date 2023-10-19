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
    }
}