using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface ICita : IGenericRepository<Cita>
    {
        abstract Task<IEnumerable<object>> GetPetMotiveDate();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetPetMotiveDate(int pageIndex, int pageSize, string search);
    }
}