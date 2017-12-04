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
    [Table("CLIENTES")]
    public partial class Cliente : IEntity
    {
        [Key]
        [Column("CLI_ID")]
        public long clienteId { get; set; }

        [Column("CLI_RFC")]
        public string rfc { get; set; }

        [Column("CLI_RSOCIAL")]
        public string razonSocial { get; set; }

        [Column("CLI_DIRECCION")]
        public string direccion { get; set; }

        [Column("CLI_DIRSERVICIO")]
        public string dirServicio { get; set; }

        [Column("CLI_CONTACTO")]
        public string contacto { get; set; }

        [Column("CLI_CONTACTO_CARGO")]
        public string contactoCargo { get; set; }

        [Column("CLI_TELEFONO")]
        public string telefono { get; set; }

        [Column("CLI_FAX")]
        public string fax { get; set; }

        [Column("CLI_MAIL")]
        public string email { get; set; }

        [Column("CLI_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("CLI_CONTACTO2")]
        public string contacto2 { get; set; }

        [Column("CLI_CONTACTO3")]
        public string contacto3 { get; set; }

        [Column("CLI_CONTACTO4")]
        public string contacto4 { get; set; }

        [Column("CLI_CONTACTO5")]
        public string contacto5 { get; set; }

        [Column("CLI_CONTACTO6")]
        public string contacto6 { get; set; }

        [Column("CLI_COMENTARIOS2")]
        public string comentarios2 { get; set; }

        [Column("CLI_ACTIVO")]
        public string activo { get; set; }

        [Column("CLI_TELEFONO2")]
        public string telefono2 { get; set; }

        [Column("CLI_GIRO_ID")]
        public long giroId { get; set; }
        public virtual Giro Giro { get; set; }

        [Column("CLI_CIU_ID")]
        public long ciudadId { get; set; }
        public virtual Ciudad Ciudad { get; set; }

        [Column("CLI_EDO_ID")]
        public long estadoId { get; set; }
        public virtual Estado Estado { get; set; }

    }
}
