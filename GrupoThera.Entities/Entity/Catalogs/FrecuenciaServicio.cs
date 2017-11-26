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
    [Table("FRECUENCIAS_SERVICIO")]
    public partial class FrecuenciaServicio : IEntity
    {
        [Key]
        [Column("FRE_ID")]
        public long frecuenciaServicioId { get; set; }

        [Column("FRE_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("FRE_FRECUENCIA")]
        public string frecuencia { get; set; }
    }
}
