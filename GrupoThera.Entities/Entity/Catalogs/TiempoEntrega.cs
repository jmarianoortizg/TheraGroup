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
    [Table("TIEMPOS_ENTREGA")]
    public partial class TiempoEntrega : IEntity
    {
        [Key]
        [Column("TENT_ID")]
        public long tiempoEntregaId { get; set; }

        [Column("TENT_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("TENT_TIEMPOENT")]
        public string tiempoEntrega { get; set; }
    }
}
