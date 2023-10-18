using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;

namespace Api.Dtos
{
    public class MedicamentoProveedorDto
    {
        public int Id { get; set; }
        public int IdProveedorFk { get; set; }
        public ProveedorDto Proveedor { get; set; }
        public int IdMedicamentoFk { get; set; }
        public MedicamentoDto Medicamento { get; set; }
    }
}