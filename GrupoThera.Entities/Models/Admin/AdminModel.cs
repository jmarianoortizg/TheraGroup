using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.Admin
{   
    public class AdminModel
    {
        public SelectList listEmpresas { get; set; }
        public SelectList listSucursal { get; set; }
        public SelectList listDepartmentos { get; set; }
        public SelectList listSucAssignedByUser { get; set; }

        public int selectedEmpresa { get; set; }
        public int selectedSucursal { get; set; }
        public int selectedDepartmentos { get; set; }
        public int selectedSucAssignedByUser { get; set; }

        public IList<Usuario> AllUsuarios { get; set; }
        public Usuario Usuario { get; set; }
        public Empresa Empresa { get; set; }
        public Sucursal Sucursal { get; set; }
        public EmpresaSucursalUsuarioMap empSucUsuMap { get; set; }
        public EmpresaSucursalMap empSucMap { get; set; }


        public SelectList listUsuario { get; set; }
        public int selectedUsuario { get; set; }
        public IList<Rol> rolesAssigned  { get; set; }
        public IList<Rol> rolesMissing { get; set; }

        public IList<Usuario> usuarioAssigned { get; set; }
        public IList<Usuario> usuarioMissing { get; set; }

    }
}
