using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class TipoMovimientoRepository: GenericRepository<TipoMovimiento>, ITipoMovimiento
    {
        private readonly ApiJwtContext _context;

        public TipoMovimientoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}