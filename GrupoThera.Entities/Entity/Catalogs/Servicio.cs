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
    [Table("SERVICIOS")]
    public partial class Servicio : IEntity
    {
        [Key]
        [Column("SERV_ID")]
        public long servicioId { get; set; }

        [Column("SERV_CLAVE")]
        public string clave { get; set; }

        [Column("SERV_DESCRIPCION")]
        public string descripcion { get; set; }

        [Column("SERV_DESCRIPCION2")]
        public string descripcion2 { get; set; }

        [Column("SERV_ACREDITADO")]
        public string acreditado { get; set; }

        [Column("SERV_TIPO")]
        public string tipo { get; set; }

        [Column("SERV_SUBCONTRATO")]
        public string subcontrato { get; set; }

        [Column("SERV_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("SERV_PRECIOBASE")]
        public decimal precioBase { get; set; }

        [Column("SERV_PRECIOPROMEDIO")]
        public decimal precioPromedio { get; set; }

        [Column("SERV_PRECIOMINIMO")]
        public decimal precioMinimo { get; set; }

        [Column("SERV_FECHAREG")]
        public DateTime fechaRegistro { get; set; }

        [Column("SERV_CLAS_ID")]
        public long clasificacionServicioId { get; set; }
        public virtual ClasificacionServicio ClasificacionServicio { get; set; }

        [Column("SERV_ASER_ID")]
        public long areaServicioId { get; set; }
        public virtual AreaServicio AreaServicio { get; set; }

        [Column("SERV_TENT_ID")]
        public long tiempoEntregaId { get; set; }
        public virtual TiempoEntrega TiempoEntrega { get; set; }

        [Column("SERV_FRE_ID")]
        public long frecuenciaServicioId { get; set; }
        public virtual FrecuenciaServicio FrecuenciaServicio { get; set; }

        [Column("SERV_PROV_ID")]
        public long provedorId { get; set; }
        public virtual Provedor Provedor { get; set; }
    }
}
