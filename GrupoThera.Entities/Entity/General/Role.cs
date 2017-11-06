using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.General
{
    public class Role
    {
        public long RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool RoleDisabled { get; set; }
    }
}
