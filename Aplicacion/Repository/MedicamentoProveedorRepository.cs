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
    public class MedicamentoProveedorRepository : GenericRepository<MedicamentoProveedor>, IMedicamentoProveedor
    {
        private readonly ApiJwtContext _context;

        public MedicamentoProveedorRepository(ApiJwtContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<MedicamentoProveedor>> GetAllAsync()
        {
            return await _context.MedicamentoProveedores
                .Include(m => m.Proveedor)
                .Include(m => m.Medicamento)
                .ToListAsync();
        }

        public override async Task<MedicamentoProveedor> GetByIdAsync(int id)
        {
            return await _context.MedicamentoProveedores
            .Include(m => m.Proveedor)
            .Include(m => m.Medicamento)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        //CONSULTA B-4: Listar los proveedores que me venden un determinado medicamento.
        public async Task<IEnumerable<object>> GetProveeSaleMedi()
        {
            var query = from p in _context.Proveedores
                        select new
                        {
                            NombreProveedor = p.Nombre,
                            Medicamentos = (from mp in _context.MedicamentoProveedores
                                            join m in _context.Medicamentos on mp.IdMedicamentoFk equals m.Id
                                            where mp.IdProveedorFk == p.Id
                                            select new
                                            {
                                                NombreMedicamento = m.Nombre
                                            }).ToList()
                        };

            return await query.ToListAsync();
        }

    }
}