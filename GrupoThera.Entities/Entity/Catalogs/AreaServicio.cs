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
    [Table("AREAS_SERVICIO")]
    public partial class AreaServicio : IEntity
    {
        [Key]
        [Column("ASER_ID")]
        public long areaServicioId { get; set; }

        [Column("ASER_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("ASER_COMENTARIOS")]
        public string comentario { get; set; }

        [Column("ASER_CLAS_ID")]
        public long clasificacionServicioId { get; set; }
        public virtual ClasificacionServicio clasificacionServicio { get; set; }
    }
}
