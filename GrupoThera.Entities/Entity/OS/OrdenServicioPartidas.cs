using GrupoThera.Entities.Contracts;
using GrupoThera.Entities.Entity.Catalogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.OS
{
    [Table("OT_SERVICIOS_PARTIDAS")]
    public class OrdenServicioPartidas : IEntity
    {
        [Key]
        [Column("OTSP_ID")]
        public long oServicioPartidasId { get; set; }

        [Column("OTSP_LABEL")]
        public string label { get; set; }

        [Column("OTSP_CLAVE_SERVICIO")]
        public string claveServicio { get; set; }

        [Column("OTSP_DESCRIPCION_SERVICIO2")]
        public string descripcionServicio { get; set; }

        [Column("OTSP_DESCRIPCION_SERVICIO")]
        public string descripcionServicio2 { get; set; }

        [Column("OTSP_IVA")]
        public decimal iva { get; set; }

        [Column("OTSP_PRECIO")]
        public decimal precio { get; set; }

        [Column("OTSP_DESCUENTO")]
        public decimal descuento { get; set; }

        [Column("OTSP_TOTAL")]
        public decimal total { get; set; }

        [Column("OTSP_MARCA")]
        public string marca { get; set; }

        [Column("OTSP_MODELO")]
        public string modelo { get; set; }

        [Column("OTSP_SERIE")]
        public string serie { get; set; }

        [Column("OTSP_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("OTSP_CANTIDAD")]
        public long cantidad { get; set; }

        [Column("OTSP_SERV_ID")]
        public long servicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        [Column("OTSP_OTS_ID")]
        public long ordenServicioId { get; set; }
        public virtual OrdenServicio OrdenServicio { get; set; }

        [Column("OTSP_OSPS_ID")]
        public long statusOrdenPartidasId { get; set; }
        public virtual StatusOrdenPartidas StatusOrdenPartidas { get; set; }
    }

}
