using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IRaza : IGenericRepository<Raza>
    {
        abstract Task<IEnumerable<object>> GetPetsByRaza();
        abstract Task<(int totalRegistros,object registros)> GetPetsByRaza(int pageIndez, int pageSize, string search);
    }
}