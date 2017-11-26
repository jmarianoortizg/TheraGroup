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
    [Table("CLASIFICACION_SERVICIO")]
    public partial class ClasificacionServicio : IEntity
    {
        [Key]
        [Column("CLAS_ID")]
        public long clasificacionServicioId { get; set; }

        [Column("CLAS_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("CLAS_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("CLAS_MCOT_ID")]
        public long metodoCotizacionId { get; set; }
        public virtual MetodoCotizacion metodoCotizacion { get; set; }
    }
}
