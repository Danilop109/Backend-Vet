using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IMascota : IGenericRepository<Mascota>
    {
        abstract Task<IEnumerable<object>> GetPetEspecie();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetPetEspecie(int pageIndex, int pageSize, string search);
        abstract Task<IEnumerable<object>> GetPetGropuByEspe();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetPetGropuByEspe(int pageIndex, int pageSize, string search);
        abstract Task<IEnumerable<object>> GetPetForVet();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetPetForVet(int pageIndex, int pageSize, string search);
        abstract Task<IEnumerable<object>> GetPetProRazaGoldenRetriever();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetPetProRazaGoldenRetriever(int pageIndex, int pageSize, string search);
    }
}