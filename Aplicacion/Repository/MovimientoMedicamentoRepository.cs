using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MovimientoMedicamentoRepository : GenericRepository<MovimientoMedicamento>, IMovimientoMedicamento
    {
        private readonly ApiJwtContext _context;

        public MovimientoMedicamentoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}