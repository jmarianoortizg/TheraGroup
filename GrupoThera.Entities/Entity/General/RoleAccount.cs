using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.General
{
    public class RoleAccount
    {
        public long RoleAccountId { get; set; }

        public long RoleId { get; set; }

        public string Account { get; set; }

        public string EmployeeName { get; set; }

        public virtual Role Role { get; set; }
    }
}
