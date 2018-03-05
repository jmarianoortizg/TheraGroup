using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.OTPre
{
    [Table("OT_PRELIMINAR_STATUS")]
    public partial class StatusOrden : IEntity
    {
        [Key]
        [Column("OTS_ID")]
        public long statusOrdenId { get; set; }

        [Column("OTS_STATUS")]
        public string descripcion { get; set; }

        [Column("OTS_CLAVE")]
        public string codigo { get; set; }
    }
}
