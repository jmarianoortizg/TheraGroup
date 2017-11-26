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
    [Table("METODOS_COTIZACION")]
    public partial class MetodoCotizacion : IEntity
    {
        [Key]
        [Column("MCOT_ID")]
        public long metodoCotizacionId { get; set; }

        [Column("MCOT_CLAVE")]
        public string clave { get; set; }

        [Column("MCOT_DESCRIPCION")]
        public string description { get; set; }

        [Column("MCOT_NOMBRE")]
        public string nombre { get; set; }
    }
}
