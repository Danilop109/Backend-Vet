using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IMedicamentoProveedor : IGenericRepository<MedicamentoProveedor>
    {
        abstract Task<IEnumerable<object>> GetProveeSaleMedi();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetProveeSaleMedi(int pageIndex, int pageSize, string search);
    }
}