using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IVeterinario : IGenericRepository<Veterinario>
    {
        abstract Task<IEnumerable<Veterinario>> GetCirujanoVascular();
        abstract Task<(int totalRegistros, IEnumerable<Veterinario> registros)> GetCirujanoVascular(int pageIndex, int pageSize, string search);
    }
}