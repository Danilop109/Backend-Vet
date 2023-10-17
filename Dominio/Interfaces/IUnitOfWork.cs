using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IUnitOfWork
    {
        IRol Roles { get; }
        IUser Users { get; }
        ICita Citas { get; }
        IDetalleMovimiento DetalleMovimientos { get; }
        IEspecie Especies { get; }
        ILaboratorio Laboratorios { get; }
        IMascota Mascotas { get; }
        IMedicamento Medicamentos { get; }
        IMedicamentoProveedor MedicamentoProveedores { get; }
        IMovimientoMedicamento MovimientoMedicamentos { get; }
        IPropietario Propietarios { get; }
        IProveedor Proveedores { get; }
        IRaza Razas { get; }
        ITipoMovimiento TipoMovimientos { get; }
        ITratamientoMedico TratamientoMedicos { get; }
        IVeterinario Veterinarios { get; }
        Task<int> SaveAsync();
    }
}