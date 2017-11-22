using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.General
{
    [Table("CAT_ROL")]
    public partial class Rol : IEntity
    {
        [Key]
        [Column("CROL_ID")]
        public long rolId { get; set; }

        [Column("CROL_NAME")]
        public string name { get; set; }

        [Column("CROL_DESCRIPTION")]
        public string descripcion { get; set; }

        [Column("CROL_ENABLED")]
        public long enabled { get; set; }

        [Column("CROL_CODE")]
        public string code { get; set; }
    }
}
