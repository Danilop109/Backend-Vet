using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MedicamentoProveedorRepository : GenericRepository<MedicamentoProveedor>, IMedicamentoProveedor
    {
        private readonly ApiJwtContext _context;

        public MedicamentoProveedorRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}