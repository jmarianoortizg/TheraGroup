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
    [Table("GIROS")]
    public partial class Giro : IEntity
    {
        [Key]
        [Column("GIRO_ID")]
        public long giroId { get; set; }

        [Column("GIRO_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("GIRO_DETALLE")]
        public string detalle { get; set; }
    }
}
