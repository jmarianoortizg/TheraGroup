using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.OS
{
    [Table("OT_SERVICIO_STATUS")]

    public class StatusOrdenServicio : IEntity
    {
        [Key]
        [Column("OTSS_ID")]
        public long statusOrdenServicioId { get; set; }

        [Column("OTSS_STATUS")]
        public string descripcion { get; set; }

        [Column("OTSS_CLAVE")]
        public string codigo { get; set; }
    }
}


