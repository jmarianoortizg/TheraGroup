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
    [Table("PROVEEDORES")]
    public partial class Provedor : IEntity
    {
        [Key]
        [Column("PROV_ID")]
        public long provedorId { get; set; }

        [Column("PROV_NOMBRE")]
        public string nombre { get; set; }

        [Column("PROV_RSOCIAL")]
        public string razonSocial { get; set; }

        [Column("PROV_RFC")]
        public string rfc { get; set; }

        [Column("PROV_DIRECCION")]
        public string direccion { get; set; }

        [Column("PROV_CONTACTO")]
        public string contacto { get; set; }

        [Column("PROV_MAIL")]
        public string mail { get; set; }

        [Column("PROV_TELEFONO")]
        public string telefono { get; set; }

        [Column("PROV_TELEFONO2")]
        public string telefono2 { get; set; }

        [Column("PROV_FAX")]
        public string fax { get; set; }

        [Column("PROV_CTABANCO")]
        public string cuentaBanco { get; set; }

        [Column("PROV_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("PROV_FECHAREG")]
        public DateTime fechaRegistro { get; set; }
    }
}
