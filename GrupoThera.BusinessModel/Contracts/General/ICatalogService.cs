using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessModel.Contracts.General
{
    public interface ICatalogService
    {
        #region Methods 

        IList<Departamento> getDepartamentos(); 
        IList<Sucursal> getSucursales();
        IList<Rol> getRoles();
        IList<Sucursal> getSucursalesbyEmpresa(long idEmpresa); 
        Sucursal getSucursalById(long idSucursal);
        IList<AreaServicio> getAreaServicios();
        IList<ClasificacionServicio> getClasificacionServicio();
        IList<Estado> getEstado();
        IList<FormaPago> getFormasPago();
        FormaPago getFormaPagoById(long idFormaPago);
        IList<MetodoCotizacion> getMetodoCotizaciones();
        IList<Ciudad> getCiudad();
        void AddAreaServicio(AreaServicio AreaServicio);
        bool ExistAreaServicio(string descripcion, long clasificacionServicioId);
        AreaServicio getAreaServicioById(long idAreaServicio);
        void EditAreaServicio(AreaServicio AreaServicio);
        void DeleteAreaServicio(long idAreaServicio);
        IList<Empresa> getEmpresas();
        Empresa getEmpresaById(long idEmpresa);
        void AddEmpresa(Empresa Empresa);
        void EditEmpresa(Empresa Empresa);
        void DeleteEmpresa(long idEmpresa);
        IList<Cliente> getClientes();
        Cliente getClienteById(long idCliente);
        void AddCliente(Cliente Cliente);
        void EditCliente(Cliente Cliente);
        void DeleteCliente(long idCliente);
        bool existProvedor(string nombre, string razonSocial, string rfc);
        IList<Provedor> getProvedores();
        Provedor getProvedorById(long idProvedor);
        void AddProvedor(Provedor Provedor);
        void EditProvedor(Provedor Provedor);
        void DeleteProvedor(long idProvedor);
        bool ExistGiro(string descripcion);
        IList<Giro> getGiros();
        Giro getGiroById(long idCliente);
        void AddGiro(Giro Giro);
        void EditGiro(Giro Giro);
        void DeleteGiro(long idGiro);
        IList<ClasificacionServicio> getClasificacionServicios();
        ClasificacionServicio getClasificacionServicioById(long idClasificacion);
        void AddClasificacionServicio(ClasificacionServicio Clasificacion);
        void EditClasificacionServicio(ClasificacionServicio Clasificacion);
        void DeleteClasificacionServicio(long idClasificacion);

        IList<FrecuenciaServicio> getFrecuenciaServicios();
        FrecuenciaServicio getFrecuenciaServicioById(long idFrecuenciaServicio);
        void AddFrecuenciaServicio(FrecuenciaServicio FrecuenciaServicio);
        void EditFrecuenciaServicio(FrecuenciaServicio FrecuenciaServicio);
        void DeleteFrecuenciaServicio(long idFrecuenciaServicio);

        IList<Moneda> getMonedas();
        Moneda getMonedaById(long idMoneda);
        void AddMoneda(Moneda Moneda);
        void EditMoneda(Moneda Moneda);
        void DeleteMoneda(long idMoneda);

        IList<Configuracion> getConfiguraciones();
        Configuracion getConfiguracionById(long idConfiguracion);
        void AddConfiguracion(Configuracion Configuracion);
        void EditConfiguracion(Configuracion Configuracion);
        void DeleteConfiguracion(long idConfiguracion);

        IList<TiempoEntrega> getTiempoEntregas();
        TiempoEntrega getTiempoEntregaById(long idTiempoEntrega);
        void AddTiempoEntrega(TiempoEntrega TiempoEntrega);
        void EditTiempoEntrega(TiempoEntrega TiempoEntrega);
        void DeleteTiempoEntrega(long idTiempoEntrega);

        IList<Servicio> getServicios();
        Servicio getServicioById(long idServicio);
        void AddServicio(Servicio Servicio);
        void EditServicio(Servicio Servicio);
        void DeleteServicio(long idServicio);

        IList<StatusCotizacion> getStatusCotizacion();
        StatusCotizacion getStatusCotizacionStatus(string status);
        long getStatusCotizacionId(string status);


        #endregion Methods
    }
}
