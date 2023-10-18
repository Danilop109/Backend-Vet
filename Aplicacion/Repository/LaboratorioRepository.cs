using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public override async Task<IEnumerable<Laboratorio>> GetAllAsync()
        {
            return await _context.Laboratorios
                .ToListAsync();
        }

        public override async Task<Laboratorio> GetByIdAsync(int id)
        {
            return await _context.Laboratorios
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        
    }
}