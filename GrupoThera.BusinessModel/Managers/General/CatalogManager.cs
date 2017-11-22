using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.BusinessModel.Contracts.General;
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
        private IEmpresa _empresaDA;
        private ISucursal _sucursalDA;
        private IRol _rolDA;

        #endregion Fields 

        #region Constructor 

        public CatalogManager( IDepartamento departamentoDA,
                               IEmpresa empresaDA,
                               ISucursal sucursalDA,
                               IRol rolDA
                             )
        {
            //Dependency Injection
            _departamentoDA = departamentoDA;
            _empresaDA = empresaDA;
            _sucursalDA = sucursalDA;
            _rolDA = rolDA;
        }

        #endregion Constructor 

        #region Methods 

        public IList<Departamento> getDepartamentos()
        {
            return _departamentoDA.GetList().ToList();
        }

        public IList<Empresa> getEmpresas()
        {
            return _empresaDA.GetList().ToList();
        }

        public IList<Sucursal> getSucursales()
        {
            return _sucursalDA.GetList().ToList();
        }

        public IList<Rol> getRoles()
        {
            return _rolDA.GetList().ToList();
        }


        #endregion Methods  
    }
}

