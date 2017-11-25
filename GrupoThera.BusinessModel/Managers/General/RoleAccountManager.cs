using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Entity.General;
using System.Collections.Generic;
using System.Linq;

namespace GrupoThera.BusinessModel.Managers.General
{
    public class RoleAccountManager : IRoleAccountService
    {
        #region Fields

        private IUsuario _usuarioDA;
        private IEmpresaSucursalMap _empresaSucursalMapDA;
        private IEmpresaSucursalUsRoleMap _empresaSucursalRoleMapDA;
        private IEmpresaSucursalUsuarioMap _empresaSucursalUsuarioMapDA;

        #endregion Fields 

        #region Constructor 

        public RoleAccountManager( IUsuario usuarioDA,
                                   IEmpresaSucursalMap empresaSucursalMapDA,
                                   IEmpresaSucursalUsRoleMap empresaSucursalRoleMapDA,
                                   IEmpresaSucursalUsuarioMap empresaSucursalUsuarioMapDA
                                 )
        {
            //Dependency Injection
            _usuarioDA = usuarioDA;
            _empresaSucursalMapDA = empresaSucursalMapDA;
            _empresaSucursalRoleMapDA = empresaSucursalRoleMapDA;
            _empresaSucursalUsuarioMapDA = empresaSucursalUsuarioMapDA;
        }

        #endregion Constructor 

        #region Methods 

        public EmpresaSucursalUsuarioMap getEmpresaSucursalUsuario(int empresaId,int sucursalId,long usuarioId)
        {
            var empresaSucursal = _empresaSucursalMapDA.Get(t => t.empresaId == empresaId && t.sucursalId == sucursalId);
            return _empresaSucursalUsuarioMapDA.Get(t => t.empresaSucursalId == empresaSucursal.empresaSucursalId && t.usuarioId == usuarioId);
        }

        public IList<Rol> getListRoleByUser(long empresaSucursalUs)
        {   
            return _empresaSucursalRoleMapDA.GetList(t => t.empresaSucursalUsId == empresaSucursalUs).ToList().Select(t => t.Rol).ToList();
        }

        public Usuario getAccountUser(string usuario, string password)
        {   
            return _usuarioDA.Get(t => t.usuario.ToUpper().Equals(usuario.ToUpper()) && t.password.ToUpper().Equals(password.ToUpper()));
        }

        #endregion Methods  
    }
}

