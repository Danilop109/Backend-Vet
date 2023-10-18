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
    }
}