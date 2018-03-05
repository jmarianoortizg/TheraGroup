using GrupoThera.Entities.Contracts;
using GrupoThera.Entities.Entity.Catalogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.OTPre
{
    [Table("OT_PRELIMINARES_PARTIDAS")]
    public partial class OTPrePartidas : IEntity
    {
        [Key]
        [Column("OTPP_ID")]
        public long otPrePartidasId { get; set; }

        [Column("OTPP_CLAVE_SERVICIO")]
        public string claveServicio { get; set; }

        [Column("OTPP_DESCRIPCION_SERVICIO")]
        public string descripcionServicio { get; set; }

        [Column("OTPP_DESCRIPCION_SERVICIO2")]
        public string descripcionServicio2 { get; set; }

        [Column("OTPP_IVA")]
        public decimal iva { get; set; }

        [Column("OTPP_PRECIO")]
        public decimal precio { get; set; }

        [Column("OTPP_DESCUENTO")]
        public decimal descuento { get; set; }

        [Column("OTPP_TOTAL")]
        public decimal total { get; set; }

        [Column("OTPP_MARCA")]
        public string marca { get; set; }

        [Column("OTPP_MODELO")]
        public string modelo { get; set; }

        [Column("OTPP_SERIE")]
        public string serie { get; set; }

        [Column("OTPP_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("OTPP_CANTIDAD")]
        public long cantidad { get; set; }

        [Column("OTPP_SERV_ID")]
        public long servicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        [Column("OTPP_OTP_ID")]
        public long otPreliminarId { get; set; }
        public virtual OTPreliminar OTPreliminar { get; set; }


    }

}
