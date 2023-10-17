using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApiJwtContext _context;
        private UserRepository _users;
        private RolRepository _roles;
        private CitaRepository _citas;
        private DetalleMovimientoRepository _detalleMovimientos;
        private EspecieRepository _especies;
        private LaboratorioRepository _laboratorios;
        private MascotaRepository _mascotas;
        private MedicamentoProveedorRepository _medicamentoProveedor;
        private MedicamentoRepository _medicamentos;
        private MovimientoMedicamentoRepository _movimientoMedicamentos;
        private PropietarioRepository _propietarios;
        private ProveedorRepository _proveedores;
        private RazaRepository _razas;
        private TipoMovimientoRepository _tipoMovimientos;
        private TratamientoMedicoRepository _tratamientoMedicos;
        private VeterinarioRepository _veterionarios;
        public UnitOfWork(ApiJwtContext context)
        {
            _context = context;
        }

        public IRol Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context);
                }
                return _roles;
            }
        }

        public IUser Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_context);
                }
                return _users;
            }
        }

        public ICita Citas
    {
        get{
            if(_citas== null)
            {
                _citas= new CitaRepository(_context);
            }
            return _citas;
        }
    }
    public IDetalleMovimiento DetalleMovimientos
    {
        get{
            if(_detalleMovimientos== null)
            {
                _detalleMovimientos= new DetalleMovimientoRepository(_context);
            }
            return _detalleMovimientos;
        }
    }
    public IEspecie Especies
    {
        get{
            if(_especies== null)
            {
                _especies= new EspecieRepository(_context);
            }
            return _especies;
        }
    }
    public ILaboratorio Laboratorios
    {
        get{
            if(_laboratorios== null)
            {
                _laboratorios= new LaboratorioRepository(_context);
            }
            return _laboratorios;
        }
    }
    public IMascota Mascotas
    {
        get{
            if(_mascotas== null)
            {
                _mascotas= new MascotaRepository(_context);
            }
            return _mascotas;
        }
    }

    public IMedicamentoProveedor MedicamentoProveedores
    {
        get{
            if(_medicamentoProveedor== null)
            {
                _medicamentoProveedor= new MedicamentoProveedorRepository(_context);
            }
            return _medicamentoProveedor;
        }
    }
    public IMedicamento Medicamentos
    {
        get{
            if(_medicamentos== null)
            {
                _medicamentos= new MedicamentoRepository(_context);
            }
            return _medicamentos;
        }
    }
    public IMovimientoMedicamento MovimientoMedicamentos
    {
        get{
            if(_movimientoMedicamentos== null)
            {
                _movimientoMedicamentos= new MovimientoMedicamentoRepository(_context);
            }
            return _movimientoMedicamentos;
        }
    }
    public IPropietario Propietarios
    {
        get{
            if(_propietarios== null)
            {
                _propietarios= new PropietarioRepository(_context);
            }
            return _propietarios;
        }
    }
    public IProveedor Proveedores
    {
        get{
            if(_proveedores== null)
            {
                _proveedores= new ProveedorRepository(_context);
            }
            return _proveedores;
        }
    }
    public IRaza Razas
    {
        get{
            if(_razas== null)
            {
                _razas= new RazaRepository(_context);
            }
            return _razas;
        }
    }

     public ITipoMovimiento TipoMovimientos
    {
        get{
            if(_tipoMovimientos== null)
            {
                _tipoMovimientos= new TipoMovimientoRepository(_context);
            }
            return _tipoMovimientos;
        }
    }
    public ITratamientoMedico TratamientoMedicos
    {
        get{
            if(_tratamientoMedicos== null)
            {
                _tratamientoMedicos= new TratamientoMedicoRepository(_context);
            }
            return _tratamientoMedicos;
        }
    }

    public IVeterinario Veterinarios
    {
        get{
            if(_veterionarios== null)
            {
                _veterionarios= new VeterinarioRepository(_context);
            }
            return _veterionarios;
        }
    }

        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}