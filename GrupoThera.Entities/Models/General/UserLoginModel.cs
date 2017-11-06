using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Models.General
{
    public class UserLoginModel
    {
        public string UserName { get; set; }
        public string Password{ get; set; }
        public bool stayLogin { get; set; }
    }
}
