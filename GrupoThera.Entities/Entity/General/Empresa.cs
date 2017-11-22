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
    [Table("CAT_USUARIOS")]
    public partial class Empresa : IEntity
    {
        [Key]
        [Column("EMP_ID")]
        public long empresaId { get; set; }

        [Column("EMP_NOMBRE")]
        public string nombre { get; set; }

        [Column("EMP_RFC")]
        public string rfc { get; set; }

        [Column("EMP_RSOCIAL")]
        public string razonSocial { get; set; }

        [Column("EMP_DIRECCION")]
        public string direccion { get; set; }

        [Column("EMP_TELEFONO")]
        public string telefono { get; set; }

        [Column("EMP_TELEFONO2")]
        public string telefono2 { get; set; }

        [Column("EMP_EMAIL")]
        public string email { get; set; }

        [Column("EMP_CONTACTO")]
        public string contacto { get; set; }

        [Column("EMP_FORMATOS")]
        public string formato { get; set; }
    }
}
