using GrupoThera.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.General
{
    [Table("CAT_SUCURSALES")]
    public partial class Sucursal : IEntity
    {
        [Key]
        [Column("CSUC_ID")]
        public long sucursalId { get; set; }

        [Column("CSUC_NOMBRE")]
        public string nombre { get; set; }

        [Column("CSUC_RFC")]
        public string rfc { get; set; }

        [Column("CSUC_RSOCIAL")]
        public string razonSocial { get; set; }

        [Column("CSUC_DIRECCION")]
        public string direccion { get; set; }

        [Column("CSUC_TELEFONO")]
        public string telefono { get; set; }

        [Column("CSUC_TELEFONO2")]
        public string telefono2 { get; set; }

        [Column("CSUC_EMAIL")]
        public string mail { get; set; }

        [Column("CSUC_CONTACTO")]
        public string contacto { get; set; }

        [Column("CSUC_CODIGO")]
        public string codigo { get; set; }

        [Column("CSUC_PREFIJO")]
        public string prefijo { get; set; }
    }
}
