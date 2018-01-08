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
    [Table("COT_PRELIMINARES")]
    public partial class Preliminar : IEntity
    {
        public Preliminar()
        {
            this.Prepartidas = new HashSet<PrePartidas>();
        }

        [Key]
        [Column("COTP_ID")]
        public long preliminaresId { get; set; }

        [Column("COTP_NODOC")]
        public long noDoc  { get; set; }

        [Column("COTP_NODOC_INTERNO")]
        public long noDocInt { get; set; }

        [Column("COTP_EMP_PREFIJO")]
        public string empresaPrefijo { get; set; }

        [Column("COTP_FECHA_CREACION")]
        public DateTime fechaCreacion { get; set; }

        [Column("COTP_USUARIO_CREADOR")]
        public string owner { get; set; }

        [Column("COTP_MARCA")]
        public string marca { get; set; }

        [Column("COTP_MODELO")]
        public string modelo { get; set; }

        [Column("COTP_SERIE")]
        public string serie { get; set; }

        [Column("COTP_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("COTP_TCAMBIO")]
        public string tipoCambio { get; set; }

        [Column("COTP_VIATICOS")]
        public decimal viaticos { get; set; }

        [Column("COTP_DIRSERVICIO")]
        public string direccionServicio { get; set; }

        [Column("COTP_IVAPORCIENTO")]
        public decimal iva { get; set; }

        [Column("COTP_SUBTOTAL")]
        public decimal subtotal { get; set; }

        [Column("COTP_TOTALIVA")]
        public decimal totalIva { get; set; }

        [Column("COTP_TOTAL")]
        public decimal total { get; set; }
                
        [Column("COTP_CONF_CLAVE")]
        public string confClave{ get; set; }

        [Column("COTP_CONF_EMISION")]
        public string confEmision{ get; set; }

        [Column("COTP_CONF_VIGENCIA")]
        public string confVigencia{ get; set; }

        [Column("COTP_CONF_TIPODOC")]
        public string confTipoDoc { get; set; }

        [Column("COTP_CONF_REVISION")]
        public string confRevision{ get; set; }

        [Column("COTP_FORMATO_COT")]
        public string confFormato { get; set; }

        [Column("COTP_MON_ID")]
        public long monedaId { get; set; }
        public virtual Moneda Moneda { get; set; }

        [Column("COTP_FPAG_ID")]
        public long formaPagoId { get; set; }
        public virtual FormaPago FormaPago { get; set; }

        [Column("COTP_EMP_ID")]
        public long empresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        [Column("COTP_CSUC_ID")]
        public long sucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [Column("COTP_CSTC_ID")]
        public long statusCotizacionId { get; set; }
        public virtual StatusCotizacion StatusCotizacion { get; set; }

        [Column("COTP_CLI_ID")]
        public long clienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("COTP_CLAS_ID")]
        public long clasificacionServicioId { get; set; }
        public virtual ClasificacionServicio ClasificacionServicio { get; set; }
        public virtual ICollection<PrePartidas> Prepartidas { get; set; }

    }
}
