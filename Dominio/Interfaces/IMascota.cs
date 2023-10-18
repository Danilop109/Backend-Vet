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
        Task<IEnumerable<Mascota>> GetPetEspecie2();
    }
}