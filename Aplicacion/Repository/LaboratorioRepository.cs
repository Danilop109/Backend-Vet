using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class LaboratorioRepository : GenericRepository<Laboratorio>, ILaboratorio
    {
        private readonly ApiJwtContext _context;

        public LaboratorioRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}