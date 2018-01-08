using GrupoThera.Entities.Contracts;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.Cotizaciones
{
    [Table("COT_PRELIMINAR_PARTIDAS")]
    public partial class PrePartidas : IEntity
    {
        [Key]
        [Column("CPP_ID")]
        public long prePartidasId { get; set; }

        [Column("CPP_CLAVE_SERVICIO")]
        public string claveServicio  { get; set; }

        [Column("CPP_DESCRIPCION_SERVICIO")]
        public string descripcionServicio { get; set; }

        [Column("CPP_DESCRIPCION_SERVICIO2")]
        public string descripcionServicio2 { get; set; }

        [Column("CPP_IVA")]
        public decimal iva { get; set; }

        [Column("CPP_PRECIO")]
        public decimal precio { get; set; }

        [Column("CPP_DESCUENTO")]
        public decimal descuento { get; set; }

        [Column("CPP_TOTAL")]
        public decimal total { get; set; }

        [Column("CPP_MARCA")]
        public string marca { get; set; }

        [Column("CPP_MODELO")]
        public string modelo { get; set; }

        [Column("CPP_SERIE")]
        public string serie { get; set; }

        [Column("CPP_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("CPP_CANTIDAD")]
        public long cantidad { get; set; }

        [Column("CPP_SERV_ID")]
        public long servicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        [Column("CPP_COTP_ID")]
        public long preliminaresId { get; set; }
        public virtual Preliminar Preliminares { get; set; }
    }
}
