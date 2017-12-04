﻿using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Entity.Catalogs;
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


        #endregion Fields 

        #region Constructor 

        public CatalogManager(IDepartamento departamentoDA,
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
                               ITiempoEntrega tiempoEntregaDA
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

        #region Ciudad
        public IList<Ciudad> getCiudad()
        {
            return _ciudadDA.GetList().ToList();
        }

        #endregion Ciudad

        #region Giro
        public IList<Giro> getGiro()
        {
            return _giroDA.GetList().ToList();
        }
        #endregion Giro

        #endregion Methods  
    }
}

