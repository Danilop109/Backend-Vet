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
    public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
    {
        private readonly ApiJwtContext _context;

        public ProveedorRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<Proveedor>> GetAllAsync()
        {
            return await _context.Proveedores
                .ToListAsync();
        }

        public override async Task<Proveedor> GetByIdAsync(int id)
        {
            return await _context.Proveedores
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}