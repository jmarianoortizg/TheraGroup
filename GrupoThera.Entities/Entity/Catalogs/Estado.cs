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
    [Table("ESTADOS")]
    public partial class Estado : IEntity
    {
        [Key]
        [Column("EDO_ID")]
        public long estadoId { get; set; }

        [Column("EDO_NOMBRE")]
        public string nombre { get; set; }
    }
}
