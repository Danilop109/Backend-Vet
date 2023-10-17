using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class DetalleMovimientoRepository : GenericRepository<DetalleMovimiento>, IDetalleMovimiento
    {
        private readonly ApiJwtContext _context;

        public DetalleMovimientoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}