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

        #endregion Methods
    }
}
