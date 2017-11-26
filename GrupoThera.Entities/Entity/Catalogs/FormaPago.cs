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
    [Table("FORMAS_PAGO")]
    public partial class FormaPago : IEntity
    {
        [Key]
        [Column("FPAG_ID")]
        public long formaPagoId { get; set; }

        [Column("FPAG_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("FPAG_PAGO")]
        public string pago { get; set; }

        [Column("FPAG_DEFAULT")]
        public string defaultt { get; set; }
    }
}
