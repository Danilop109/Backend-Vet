using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
    {
        private readonly ApiJwtContext _context;

        public MedicamentoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}