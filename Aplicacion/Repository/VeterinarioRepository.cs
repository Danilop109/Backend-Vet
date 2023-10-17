using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinario
    {
        private readonly ApiJwtContext _context;

        public VeterinarioRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}