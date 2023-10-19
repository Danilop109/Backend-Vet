using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IMedicamento : IGenericRepository<Medicamento>
    {
        abstract Task<IEnumerable<object>> GetMediFromLab();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetMediFromLab(int pageIndex, int pageSize, string search);
        Task<IEnumerable<Medicamento>> GetMedi50000();
        Task<(int totalRegistros, IEnumerable<Medicamento> registros)> GetMedi50000(int pageIndex, int pageSize, string search);
    }
}