using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class TratamientoMedico : BaseEntity
    {
        public string Dosis {get; set;}
        public DateTime FechaAdministracion {get; set;}
        public string Observacion {get; set;}
        public int IdCitaFk {get; set;}
        public Cita Cita {get; set;}
        public int IdMedicamentoFk {get; set;}
        public Medicamento Medicamento {get; set;}
    }
}