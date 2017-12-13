using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.Cotizaciones
{
    [Table("CAT_STATUS_COT")]
    public partial class StatusCotizacion : IEntity
    {
        [Key]
        [Column("CSTC_ID")]
        public long statusCotizacionId { get; set; }

        [Column("CSTC_DESCRIPCION")]
        public long descripcion { get; set; }

        [Column("CSTC_CODIGO")]
        public long codigo { get; set; }
    }
}
