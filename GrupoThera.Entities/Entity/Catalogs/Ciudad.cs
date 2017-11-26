using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.Catalogs
{
    [Table("CIUDADES")]
    public partial class Ciudad : IEntity
    {
        [Key]
        [Column("CIU_ID")]
        public long giroId { get; set; }

        [Column("CIU_NOMBRE")]
        public string descripcion { get; set; }

        [Column("CIU_EDO_ID")]
        public long estadoId { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
