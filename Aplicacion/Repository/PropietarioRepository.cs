using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
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
    }
}