using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;

namespace Dominio.Interfaces
{
    public interface IMascota : IGenericRepository<Mascota>
    {
        Task<IEnumerable<Mascota>> GetPetEspecie();
        Task<IEnumerable<object>> GetPetGropuByEspe();
        Task<IEnumerable<object>> GetPetForVet();
        abstract Task<IEnumerable<object>> GetPetProRazaGoldenRetriever();
        abstract Task<(int totalRegistros, IEnumerable<object> registros)> GetPetProRazaGoldenRetriever(int pageIndex, int pageSize, string search);
    }
}