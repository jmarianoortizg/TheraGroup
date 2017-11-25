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
        IList<Empresa> getEmpresas();
        IList<Sucursal> getSucursales();
        IList<Rol> getRoles();
        IList<Sucursal> getSucursalesbyEmpresa(long idEmpresa);
        Empresa getEmpresaById(long idEmpresa);
        Sucursal getSucursalById(long idSucursal);


        #endregion Methods
    }
}
