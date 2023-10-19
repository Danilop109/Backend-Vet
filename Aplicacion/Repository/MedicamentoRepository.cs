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

        public override async Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Medicamentos as IQueryable<Medicamento>;

        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRegistros = await query.CountAsync();
        var registros = await query 
            .Include(p => p.Laboratorio)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

        //CONSULTA A-2: Listar los medicamentos que pertenezcan a el laboratorio Genfar
        public async Task<IEnumerable<object>> GetMediFromLab()
        {
            return await (
                from m in _context.Medicamentos
                join l in _context.Laboratorios on m.IdLaboratorioFk equals l.Id
                where l.Nombre == "Genfar"
                select new {
                    NombreMedicamento= m.Nombre,
                    Precio=m.Precio,
                    NombreLaboratorio= l.Nombre 
                }
                
            ).ToListAsync();
        }

        public async Task<(int totalRegistros, IEnumerable<object> registros)> GetMediFromLab(int pageIndex, int pageSize, string search)
        {
            var query = (
                from m in _context.Medicamentos
                join l in _context.Laboratorios on m.IdLaboratorioFk equals l.Id
                where l.Nombre == "Genfar"
                select new {
                    NombreMedicamento= m.Nombre,
                    Precio=m.Precio,
                    NombreLaboratorio= l.Nombre 
                }
                
            );

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombreMedicamento.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.NombreMedicamento);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }

        //CONSULTA A-5: Listar los medicamentos que tenga un precio de venta mayor a 50000

        public async Task<IEnumerable<Medicamento>> GetMedi50000()
        {
            return await _context.Medicamentos
            .Where(m => m.Precio >= 50000)
            .ToListAsync();
        }

        public async Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetMedi50000(int pageIndex, int pageSize, string search)
        {
            var query = _context.Medicamentos
            .Where(m => m.Precio >= 50000);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Nombre.ToLower().Contains(search));
            }

            query = query.OrderBy(p => p.Nombre);
            var totalRegistros = await query.CountAsync();
            var registros = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (totalRegistros, registros);
        }
    }
}