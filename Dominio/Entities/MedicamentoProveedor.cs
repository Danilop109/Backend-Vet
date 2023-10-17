using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class MedicamentoProveedor : BaseEntity
    {
        public int IdProveedorFk { get; set; }
        public Proveedor Proveedor { get; set; }
        public int IdMedicamentoFk { get; set; }
        public Medicamento Medicamento { get; set; }
    }
}