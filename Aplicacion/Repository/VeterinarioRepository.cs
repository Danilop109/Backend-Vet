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
    public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinario
    {
        private readonly ApiJwtContext _context;

        public VeterinarioRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<Veterinario>> GetAllAsync()
        {
            return await _context.Veterinarios
                .ToListAsync();
        }

        public override async Task<Veterinario> GetByIdAsync(int id)
        {
            return await _context.Veterinarios
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        //CONSULTA A-1: Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular.
        public async Task<IEnumerable<Veterinario>> GetCirujanoVascular()
        {
            return await _context.Veterinarios
            .Where(v => v.Especialidad == "Cirujano Vascular")
            .ToListAsync();
        }
    }
}