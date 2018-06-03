using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessModel.Contracts.General
{
    public interface IRoleAccountService
    {
        #region Methods 

        IList<Rol> getListRoleByUser(long usuarioId);
        Usuario getAccountUser(string usuario, string password);
        EmpresaSucursalUsuarioMap getEmpresaSucursalUsuario(int empresaId, int sucursalId, long usuarioId);
        bool existAccountUser(string usuario);
        void addUser(Usuario Usuario);
        void updateUser(Usuario Usuario);
        Usuario getUserbyId(int usuarioId);
        IList<Usuario> getUsers();
        IList<Departamento> getDepartments();
        IList<Usuario> GetEmpresaSucursalUsuarioMap(Empresa Empresa, Sucursal Sucursal);
        IList<Rol> getListRoleByUserSucursal(long empSucUsuMapId);
        IList<Rol> getMissingRoles(IList<Rol> rolesAssigned);
        EmpresaSucursalUsuarioMap getEmpresaSucursalbyUsuario(int idEmpresa, int idSucursal, int idUsuario);
        void addEmpSucUsuRolMap(int idEmpSucMap, int idRol);
        void DeleteRoleEmpSucUsuRolMap(int idEmpSucMap, int idRol);

        EmpresaSucursalMap getEmpresaSucursal(int idEmpresa, int idSucursal);
        void addEmpSucUsuMap(int idEmpSucMap, int idUsuario);
        void DeleteRoleEmpSucUsuMap(int idEmpSucMap, int idUsuario);
        IList<Usuario> getListUsersBySucursal(long empSucUsuMapId);
        IList<Usuario> getMissingUsers(IList<Usuario> usuariosAssigned);


        #endregion Methods
    }
}