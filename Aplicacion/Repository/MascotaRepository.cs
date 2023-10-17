using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MascotaRepository : GenericRepository<Mascota>, IMascota
    {
        private readonly ApiJwtContext _context;

        public MascotaRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }
    }
}