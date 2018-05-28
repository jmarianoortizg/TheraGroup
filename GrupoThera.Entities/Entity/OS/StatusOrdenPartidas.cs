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
    [Table("OT_SERVICIO_PART_STATUS")]

    public class StatusOrdenPartidas : IEntity
    {
        [Key]
        [Column("OSPS_ID")]
        public long statusOrdenPartidasId { get; set; }

        [Column("OSPS_STATUS")]
        public string descripcion { get; set; }

        [Column("OSPS_CLAVE")]
        public string codigo { get; set; }
    }
}


