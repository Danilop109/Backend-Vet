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
    public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
    {
        private readonly ApiJwtContext _context;

        public MedicamentoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Medicamento>> GetAllAsync()
        {
            return await _context.Medicamentos
                .Include(m => m.Laboratorio)
                .ToListAsync();
        }

        public override async Task<Medicamento> GetByIdAsync(int id)
        {
            return await _context.Medicamentos
            .Include(m => m.Laboratorio)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        //CONSULTA A-2: Listar los medicamentos que pertenezcan a el laboratorio Genfar
        public async Task<IEnumerable<Medicamento>> GetMediFromLab()
        {
            return await (
                from m in _context.Medicamentos
                join l in _context.Laboratorios on m.IdLaboratorioFk equals l.Id
                where l.Nombre == "Genfar"
                select m
                
            ).ToListAsync();
        }
    }
}