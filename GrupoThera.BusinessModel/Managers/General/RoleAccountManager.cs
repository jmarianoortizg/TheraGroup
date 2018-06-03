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
        private IDepartamento _departamentDA;
        private IRol _rolDA;
        private IEmpresaSucursalMap _empresaSucursalMapDA;
        private IEmpresaSucursalUsRoleMap _empresaSucursalRoleMapDA;
        private IEmpresaSucursalUsuarioMap _empresaSucursalUsuarioMapDA;

        #endregion Fields 

        #region Constructor 

        public RoleAccountManager( IUsuario usuarioDA,
                                   IEmpresaSucursalMap empresaSucursalMapDA,
                                   IEmpresaSucursalUsRoleMap empresaSucursalRoleMapDA,
                                   IEmpresaSucursalUsuarioMap empresaSucursalUsuarioMapDA,
                                   IDepartamento departamentDA,
                                   IRol rolDA
                                 )
        {
            //Dependency Injection
            _usuarioDA = usuarioDA;
            _empresaSucursalMapDA = empresaSucursalMapDA;
            _empresaSucursalRoleMapDA = empresaSucursalRoleMapDA;
            _empresaSucursalUsuarioMapDA = empresaSucursalUsuarioMapDA;
            _departamentDA = departamentDA;
            _rolDA = rolDA;
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
            return _empresaSucursalRoleMapDA.GetList(t => t.empresaSucursalUsuarioMapId == empresaSucursalUs && t.Rol.enabled == true).ToList().Select(t => t.Rol).ToList();
        }

        public Usuario getAccountUser(string usuario, string password)
        {   
            return _usuarioDA.Get(t => t.usuario.ToUpper().Equals(usuario.ToUpper()) && t.password.ToUpper().Equals(password.ToUpper()));
        }

        public bool existAccountUser(string usuario)
        {
            var item = _usuarioDA.Get(t => t.usuario == usuario);
            if (item != null)
                return true;
            else
                return false;
        }

        public void addUser(Usuario Usuario)
        {
            _usuarioDA.Add(Usuario);
        }

        public void updateUser(Usuario Usuario)
        {
            _usuarioDA.Update(Usuario);
        }

        public Usuario getUserbyId(int usuarioId)
        {
            return _usuarioDA.Get(t=>t.usuarioId == usuarioId);
        }

        public IList<Usuario> getUsers()
        {
            return _usuarioDA.GetList().ToList();
        }

        public IList<Departamento> getDepartments()
        {
            return _departamentDA.GetList().ToList();
        }

        public IList<Usuario> GetEmpresaSucursalUsuarioMap(Empresa Empresa, Sucursal Sucursal)
        {   
            var empresaSucursal = _empresaSucursalMapDA.Get(t => t.empresaId == Empresa.empresaId && t.sucursalId == Sucursal.sucursalId);
            return _empresaSucursalUsuarioMapDA.GetList(t => t.EmpresasSucursalMap.empresaSucursalId == empresaSucursal.empresaSucursalId).Select(t=> t.Usuario).ToList();
        }

        public IList<Rol> getListRoleByUserSucursal(long empSucUsuMapId)
        {
            return _empresaSucursalRoleMapDA.GetList(t => t.empresaSucursalUsuarioMapId == empSucUsuMapId).Select(t => t.Rol).Distinct().ToList();
        }
        public IList<Rol> getMissingRoles(IList<Rol> rolesAssigned)
        {
            var listAllRoles = _rolDA.GetList().ToList();
            return listAllRoles.Where(p => !rolesAssigned.Any(l => p.rolId == l.rolId)).ToList();
        }

        public EmpresaSucursalUsuarioMap getEmpresaSucursalbyUsuario(int idEmpresa, int idSucursal, int idUsuario)
        {
            var empSuc = _empresaSucursalMapDA.Get(t => t.empresaId == idEmpresa && t.sucursalId == idSucursal);
            return _empresaSucursalUsuarioMapDA.Get(t => t.EmpresasSucursalMap.empresaSucursalId == empSuc.empresaSucursalId && t.usuarioId == idUsuario);
        }

        public EmpresaSucursalMap getEmpresaSucursal(int idEmpresa, int idSucursal)
        {
            return _empresaSucursalMapDA.Get(t => t.empresaId == idEmpresa && t.sucursalId == idSucursal);
        }

        public void addEmpSucUsuRolMap(int idEmpSucMap, int idRol)
        {
            _empresaSucursalRoleMapDA.Add(new EmpresaSucursalUsRoleMap() { empresaSucursalUsuarioMapId = idEmpSucMap, rolId = idRol });
        }

        public void DeleteRoleEmpSucUsuRolMap(int idEmpSucMap, int idRol)
        {
            var item = _empresaSucursalRoleMapDA.Get(t => t.empresaSucursalUsuarioMapId == idEmpSucMap && t.rolId == idRol);
            _empresaSucursalRoleMapDA.Delete(item);
        }

        public void addEmpSucUsuMap(int idEmpSucMap, int idUsuario)
        {
            _empresaSucursalUsuarioMapDA.Add(new EmpresaSucursalUsuarioMap() { empresaSucursalId = idEmpSucMap, usuarioId = idUsuario });
        }

        public void DeleteRoleEmpSucUsuMap(int idEmpSucMap, int idUsuario)
        {
            var item = _empresaSucursalUsuarioMapDA.Get(t => t.empresaSucursalId == idEmpSucMap && t.usuarioId == idUsuario);
            _empresaSucursalUsuarioMapDA.Delete(item);
        }

        public IList<Usuario> getListUsersBySucursal(long empSucUsuMapId)
        {
            return _empresaSucursalUsuarioMapDA.GetList(t => t.empresaSucursalId == empSucUsuMapId).Select(t => t.Usuario).Distinct().ToList();

        }

        public IList<Usuario> getMissingUsers(IList<Usuario> usuariosAssigned)
        {
            var listAllRoles = _usuarioDA.GetList().ToList();
            return listAllRoles.Where(p => !usuariosAssigned.Any(l => p.usuarioId == l.usuarioId)).ToList();
        }


        #endregion Methods  
    }
}

