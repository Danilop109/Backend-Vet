using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IMovimientoMedicamento : IGenericRepository<MovimientoMedicamento>
    {
        abstract Task<IEnumerable<object>> GetmoviMedi();
        abstract Task<(int totalRegistros,IEnumerable<object> registros)> GetmoviMedi(int pageIndez, int pageSize, string search);
    }
}