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

        public override async Task<(int totalRegistros, IEnumerable<MedicamentoProveedor> registros)> GetAllAsync(int pageIndex, int pageSize, int search)
    {
        var query = _context.MedicamentoProveedores as IQueryable<MedicamentoProveedor>;

        if (search != 0)
        {
            query = query.Where(p => p.IdProveedorFk == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Include(p => p.Proveedor)
            .Include(p => p.Medicamento)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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