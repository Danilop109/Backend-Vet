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
    public class MovimientoMedicamentoRepository : GenericRepository<MovimientoMedicamento>, IMovimientoMedicamento
    {
        private readonly ApiJwtContext _context;

        public MovimientoMedicamentoRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

         public override async Task<IEnumerable<MovimientoMedicamento>> GetAllAsync()
        {
            return await _context.MovimientoMedicamentos
                .Include(m => m.TipoMovimiento)
                .ToListAsync();
        }

        public override async Task<MovimientoMedicamento> GetByIdAsync(int id)
        {
            return await _context.MovimientoMedicamentos
            .Include(m => m.TipoMovimiento)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        //CONSULTA B-2: Listar todos los movimientos de medicamentos y el valor total de cada movimiento.
        public async Task<IEnumerable<object>> GetmoviMedi()
    {
        
        var Movimiento = await (
            from d in _context.DetalleMovimientos
            join m in _context.MovimientoMedicamentos on d.IdMovimientoMedicamentoFk equals m.Id
            select new{
                idMovimientoMedicamento = m.Id,
                TipoMovimiento = m.TipoMovimiento.Descripcion,
                total = d.Precio * d.Cantidad,
            }).Distinct()
            .ToListAsync();

        return Movimiento;
    }
    }
}