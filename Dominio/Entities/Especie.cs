using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Especie : BaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<Mascota> Mascotas { get; set; }
        public ICollection<Raza> Razas { get; set; }

        
    }
}