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
    [Table("MONEDAS")]
    public partial class Moneda : IEntity
    {
        [Key]
        [Column("MON_ID")]
        public long monedaId { get; set; }

        [Column("MON_MONEDA")]
        public string moneda { get; set; }

        [Column("MON_DEFAULT")]
        public string defaultt { get; set; }

        [Column("MON_SIMBOLO")]
        public string simbolo { get; set; }

        [Column("MON_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("MON_TIPOCAMBIOPESOS")]
        public decimal tipoCambio { get; set; }        
    }
}
