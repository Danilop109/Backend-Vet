using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
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
    }
}