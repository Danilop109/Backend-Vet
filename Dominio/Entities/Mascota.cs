using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Mascota : BaseEntity
    {
        public string Nombre { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public int IdPropietarioFk { get; set; }
        public Propietario Propietario { get; set; }
        public int IdEspecieFk { get; set; }
        public Especie Especie { get; set; }
        public int IdRazaFk { get; set; }
        public Raza Raza { get; set; }
        public ICollection<Cita> Citas { get; set; }

    }
}