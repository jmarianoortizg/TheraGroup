using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessModel.Managers.General
{
    public class CatalogManager : ICatalogService
    {
        #region Fields

        private IDepartamento _departamentoDA;
        private ISucursal _sucursalDA;
        private IRol _rolDA;
        private IEmpresaSucursalMap _empresaSucursalMapDA;
        private IEmpresa _empresaDA;
        private IAreaServicio _areaServicioDA;
        private ICiudad _ciudadDA;
        private IClasificacionServicio _clasificacionServicioDA;
        private ICliente _clienteDA;
        private IConfiguracion _configuracionDA;
        private IEstado _estadoDA;
        private IFormaPago _formaPagoDA;
        private IFrecuenciaServicio _frecuenciaServicioDA;
        private IGiro _giroDA;
        private IMetodoCotizacion _metodoCotizacionDA;
        private IMoneda _monedaDA;
        private IProvedor _provedorDA;
        private IServicio _servicioDA;
        private ITiempoEntrega _tiempoEntregaDA;
        private IStatusCotizacion _statusCotizacionDA;

        #endregion Fields 

        #region Constructor 

        public CatalogManager( IDepartamento departamentoDA,
                               ISucursal sucursalDA,
                               IRol rolDA,
                               IEmpresaSucursalMap empresaSucursalMapDA,
                               IEmpresa empresaDA,
                               IAreaServicio areaServicioDA,
                               ICiudad ciudadDA,
                               IClasificacionServicio clasificacionServicioDA,
                               ICliente clienteDA,
                               IConfiguracion configuracionDA,
                               IEstado estadoDA,
                               IFormaPago formaPagoDA,
                               IFrecuenciaServicio frecuenciaServicioDA,
                               IGiro giroDA,
                               IMetodoCotizacion metodoCotizacionDA,
                               IMoneda monedaDA,
                               IProvedor provedorDA,
                               IServicio servicioDA,
                               ITiempoEntrega tiempoEntregaDA,
                               IStatusCotizacion statusCotizacionDA
                             )
        {
            //Dependency Injection
            _departamentoDA = departamentoDA;
            _sucursalDA = sucursalDA;
            _rolDA = rolDA;
            _empresaSucursalMapDA = empresaSucursalMapDA;
            _empresaDA = empresaDA;
            _areaServicioDA = areaServicioDA;
            _ciudadDA = ciudadDA;
            _clasificacionServicioDA = clasificacionServicioDA;
            _clienteDA = clienteDA;
            _configuracionDA = configuracionDA;
            _estadoDA = estadoDA;
            _formaPagoDA = formaPagoDA;
            _frecuenciaServicioDA = frecuenciaServicioDA;
            _giroDA = giroDA;
            _metodoCotizacionDA = metodoCotizacionDA;
            _monedaDA = monedaDA;
            _provedorDA = provedorDA;
            _servicioDA = servicioDA;
            _tiempoEntregaDA = tiempoEntregaDA;
            _statusCotizacionDA = statusCotizacionDA;
        }

        #endregion Constructor 

        #region Methods 

        #region Departamento
        public IList<Departamento> getDepartamentos()
        {
            return _departamentoDA.GetList().ToList();
        }

        #endregion Departamento 

        #region Sucursales
        public IList<Sucursal> getSucursales()
        {
            return _sucursalDA.GetList().ToList();
        }

        public IList<Sucursal> getSucursalesbyEmpresa(long idEmpresa)
        {
            return _empresaSucursalMapDA.GetList(t => t.empresaId == idEmpresa).Select(x => x.Sucursal).ToList();
        }

        public Sucursal getSucursalById(long idSucursal)
        {
            return _sucursalDA.Get(t => t.sucursalId == idSucursal);
        }
        #endregion Sucursales

        #region Roles
        public IList<Rol> getRoles()
        {
            return _rolDA.GetList().ToList();
        }
        #endregion Roles 

        #region AreaServicio
        public bool ExistAreaServicio(string descripcion, long clasificacionServicioId)
        {
            var areaServicio = _areaServicioDA.Get(t => t.descripcion.ToUpper().Equals(descripcion.ToUpper()) && t.clasificacionServicioId == clasificacionServicioId);
            if (areaServicio == null)
                return false;
            return true;
        }

        public AreaServicio getAreaServicioById(long idAreaServicio)
        {
            return _areaServicioDA.Get(t => t.areaServicioId == idAreaServicio);
        }

        public IList<AreaServicio> getAreaServicios()
        {
            return _areaServicioDA.GetList().ToList();
        }

        public void AddAreaServicio(AreaServicio AreaServicio)
        {
             _areaServicioDA.Add(AreaServicio);
        }

        public void EditAreaServicio(AreaServicio AreaServicio)
        {
            _areaServicioDA.Update(AreaServicio);
        }

        public void DeleteAreaServicio(long idAreaServicio)
        {
            var item = _areaServicioDA.Get( t => t.areaServicioId == idAreaServicio);
            _areaServicioDA.Delete(item);
        }

        #endregion AreaServicio

        #region ClasificacionServicio
        public IList<ClasificacionServicio> getClasificacionServicio()
        {
            return _clasificacionServicioDA.GetList().ToList();
        }
        #endregion ClasificacionServicio

        #region Empresa 

        public IList<Empresa> getEmpresas()
        {
            return _empresaDA.GetList().ToList();
        }

        public Empresa getEmpresaById(long idEmpresa)
        {
            return _empresaDA.Get(t => t.empresaId == idEmpresa);
        } 

        public void AddEmpresa(Empresa Empresa)
        {
            _empresaDA.Add(Empresa);
        }

        public void EditEmpresa(Empresa Empresa)
        {
            _empresaDA.Update(Empresa);
        }

        public void DeleteEmpresa(long idEmpresa)
        {
            var item = _empresaDA.Get(t => t.empresaId == idEmpresa);
            _empresaDA.Delete(item);
        }

        #endregion Empresa

        #region Cliente 

        public IList<Cliente> getClientes()
        {
            return _clienteDA.GetList().ToList();
        }

        public Cliente getClienteById(long idCliente)
        {
            return _clienteDA.Get(t => t.clienteId == idCliente);
        }

        public void AddCliente(Cliente Cliente)
        {
            _clienteDA.Add(Cliente);
        }

        public void EditCliente(Cliente Cliente)
        {
            _clienteDA.Update(Cliente);
        }

        public void DeleteCliente(long idCliente)
        {
            var item = _clienteDA.Get(t => t.clienteId == idCliente);
            _clienteDA.Delete(item);
        }

        #endregion Cliente

        #region Estado
        public IList<Estado> getEstado()
        {
            return _estadoDA.GetList().ToList();
        }
        #endregion Estado

        #region FormaPago
        public IList<FormaPago> getFormasPago()
        {
            return _formaPagoDA.GetList().ToList();
        }

        public FormaPago getFormaPagoById(long idFormaPago)
        {
            return _formaPagoDA.Get(t => t.formaPagoId == idFormaPago);
        }

        #endregion FormaPago

        #region MetodosCotizacion
        public IList<MetodoCotizacion> getMetodoCotizaciones()
        {
            return _metodoCotizacionDA.GetList().ToList();
        }
        #endregion MetodoCotizacion

        #region Ciudad
        public IList<Ciudad> getCiudad()
        {
            return _ciudadDA.GetList().ToList();
        }

        #endregion Ciudad

        #region Giro

        public bool ExistGiro(string descripcion)
        {
            var giro = _giroDA.Get(t => t.descripcion.ToUpper().Equals(descripcion.ToUpper()));
            if (giro == null)
                return false;
            return true;
        }

        public IList<Giro> getGiros()
        {
            return _giroDA.GetList().ToList();
        }

        public Giro getGiroById(long idGiro)
        {
            return _giroDA.Get(t => t.giroId == idGiro);
        }

        public void AddGiro(Giro Giro)
        {
            _giroDA.Add(Giro);
        }

        public void EditGiro(Giro Giro)
        {
            _giroDA.Update(Giro);
        }

        public void DeleteGiro(long idGiro)
        {
            var item = _giroDA.Get(t => t.giroId == idGiro);
            _giroDA.Delete(item);
        }

        #endregion Giro

        #region Provedor 

        public bool existProvedor(string nombre, string razonSocial,string rfc)
        {
            var provedor = _provedorDA.Get(t => t.nombre.ToUpper().Equals(nombre.ToUpper()) && t.razonSocial.ToUpper().Equals(razonSocial.ToUpper()) && t.rfc.ToUpper().Equals(rfc.ToUpper()));
            if (provedor == null)
                return false;
            return true;
        }

        public IList<Provedor> getProvedores()
        {
            return _provedorDA.GetList().ToList();
        }

        public Provedor getProvedorById(long idProvedor)
        {
            return _provedorDA.Get(t => t.provedorId == idProvedor);
        }

        public void AddProvedor(Provedor Provedor)
        {
            _provedorDA.Add(Provedor);
        }

        public void EditProvedor(Provedor Provedor)
        {
            _provedorDA.Update(Provedor);
        }

        public void DeleteProvedor(long idProvedor)
        {
            var item = _provedorDA.Get(t => t.provedorId == idProvedor);
            _provedorDA.Delete(item);
        }

        #endregion Provedor

        #region Servicio

        public IList<Servicio> getServicios()
        {
            return _servicioDA.GetList().ToList();
        }

        public Servicio getServicioById(long idServicio)
        {
            return _servicioDA.Get(t => t.servicioId == idServicio);
        }

        public void AddServicio(Servicio Servicio)
        {
            _servicioDA.Add(Servicio);
        }

        public void EditServicio(Servicio Servicio)
        {
            _servicioDA.Update(Servicio);
        }

        public void DeleteServicio(long idServicio)
        {
            var item = _servicioDA.Get(t => t.servicioId == idServicio);
            _servicioDA.Delete(item);
        }

        #endregion Servicio

        #region Clasificacion        

        public IList<ClasificacionServicio> getClasificacionServicios()
        {
            return _clasificacionServicioDA.GetList().ToList();
        }

        public ClasificacionServicio getClasificacionServicioById(long idClasificacionServicio)
        {
            return _clasificacionServicioDA.Get(t => t.clasificacionServicioId == idClasificacionServicio);
        }

        public void AddClasificacionServicio(ClasificacionServicio ClasificacionServicio)
        {
            _clasificacionServicioDA.Add(ClasificacionServicio);
        }

        public void EditClasificacionServicio(ClasificacionServicio ClasificacionServicio)
        {
            _clasificacionServicioDA.Update(ClasificacionServicio);
        }

        public void DeleteClasificacionServicio(long idClasificacion)
        {
            var item = _clasificacionServicioDA.Get(t => t.clasificacionServicioId == idClasificacion);
            _clasificacionServicioDA.Delete(item);
        }

        #endregion Clasificacion

        #region Frecuencia        

        public IList<FrecuenciaServicio> getFrecuenciaServicios()
        {
            return _frecuenciaServicioDA.GetList().ToList();
        }

        public FrecuenciaServicio getFrecuenciaServicioById(long idFrecuenciaServicio)
        {
           return _frecuenciaServicioDA.Get(t => t.frecuenciaServicioId == idFrecuenciaServicio);
        }

        public void AddFrecuenciaServicio(FrecuenciaServicio FrecuenciaServicio)
        {
            _frecuenciaServicioDA.Add(FrecuenciaServicio);
        }

        public void EditFrecuenciaServicio(FrecuenciaServicio FrecuenciaServicio)
        {
            _frecuenciaServicioDA.Update(FrecuenciaServicio);
        }

        public void DeleteFrecuenciaServicio(long idFrecuenciaServicio)
        {
            var item = _frecuenciaServicioDA.Get(t => t.frecuenciaServicioId == idFrecuenciaServicio);
            _frecuenciaServicioDA.Delete(item);
        }

        #endregion Frecuencia

        #region Moneda        

        public IList<Moneda> getMonedas()
        {
            return _monedaDA.GetList().ToList();
        }

        public Moneda getMonedaById(long idMoneda)
        {
            return _monedaDA.Get(t => t.monedaId == idMoneda);
        }

        public void AddMoneda(Moneda Moneda)
        {
            _monedaDA.Add(Moneda);
        }

        public void EditMoneda(Moneda Moneda)
        {
            _monedaDA.Update(Moneda);
        }

        public void DeleteMoneda(long idMoneda)
        {
            var item = _monedaDA.Get(t => t.monedaId == idMoneda);
            _monedaDA.Delete(item);
        }

        #endregion Moneda

        #region TiempoEntrega        

        public IList<TiempoEntrega> getTiempoEntregas()
        {
            return _tiempoEntregaDA.GetList().ToList();
        }

        public TiempoEntrega getTiempoEntregaById(long idTiempoEntrega)
        {
            return _tiempoEntregaDA.Get(t => t.tiempoEntregaId == idTiempoEntrega);
        }

        public void AddTiempoEntrega(TiempoEntrega TiempoEntrega)
        {
            _tiempoEntregaDA.Add(TiempoEntrega);
        }

        public void EditTiempoEntrega(TiempoEntrega TiempoEntrega)
        {
            _tiempoEntregaDA.Update(TiempoEntrega);
        }

        public void DeleteTiempoEntrega(long idTiempoEntrega)
        {
            var item = _tiempoEntregaDA.Get(t => t.tiempoEntregaId == idTiempoEntrega);
            _tiempoEntregaDA.Delete(item);
        }

        #endregion TiempoEntrega

        #region Configuracion        

        public IList<Configuracion> getConfiguraciones()
        {
            return _configuracionDA.GetList().ToList();
        }

        public Configuracion getConfiguracionById(long idConfiguracion)
        {
            return _configuracionDA.Get(t => t.configuracionId == idConfiguracion);
        }

        public void AddConfiguracion(Configuracion Configuracion)
        {
            _configuracionDA.Add(Configuracion);
        }

        public void EditConfiguracion(Configuracion Configuracion)
        {
            _configuracionDA.Update(Configuracion);
        }

        public void DeleteConfiguracion(long idConfiguracion)
        {
            var item = _configuracionDA.Get(t => t.configuracionId == idConfiguracion);
            _configuracionDA.Delete(item);
        }

        #endregion Configuracion

        #region StatusCotizacion        

        public IList<StatusCotizacion> getStatusCotizacion()
        {
            return _statusCotizacionDA.GetList().ToList();
        }

        public StatusCotizacion getStatusCotizacionStatus(string status)
        {
            return _statusCotizacionDA.Get(t=> t.codigo.Equals(status));
        }

        public long getStatusCotizacionId(string status)
        {
            return _statusCotizacionDA.Get(t => t.codigo.Equals(status)).statusCotizacionId;
        }



        #endregion StatusCotizacion

        #endregion Methods  
    }
}

