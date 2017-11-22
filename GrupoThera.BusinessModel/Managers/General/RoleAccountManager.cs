using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.BusinessModel.Contracts.General;

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
        

        #endregion Methods  
    }
}

