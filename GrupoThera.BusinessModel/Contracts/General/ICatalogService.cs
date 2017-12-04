using GrupoThera.Entities.Entity.Catalogs;
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
        IList<Ciudad> getCiudad();
        IList<Giro> getGiro();
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

        #endregion Methods
    }
}
