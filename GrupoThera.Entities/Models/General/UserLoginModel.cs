using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.General
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool stayLogin { get; set; }

        public SelectList listEmpresas { get; set; }
        public SelectList listSucursal { get; set; }

        public int selectedEmpresa { get; set; }
        public int selectedSucursal { get; set; }

    }
}
