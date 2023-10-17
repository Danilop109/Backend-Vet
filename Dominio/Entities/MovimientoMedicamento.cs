using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class MovimientoMedicamento : BaseEntity
    {
        public DateOnly Fecha { get; set; }
        public int IdTipoMovimientoFk { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public ICollection<DetalleMovimiento> DetalleMovimientos { get; set; }
    }
}