using GrupoThera.Entities.Contracts;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Entity.OTPre
{
    [Table("OT_PRELIMINARES")]
    public partial class OTPreliminar : IEntity
    {
        public OTPreliminar()
        {
            this.OTPrePartidas = new HashSet<OTPrePartidas>();
        }

        [Key]
        [Column("OTP_ID")]
        public long otPreliminarId { get; set; }

        [Column("OTP_NODOC")]
        public long noDoc { get; set; }

        [Column("OTP_NODOC_INTERNO")]
        public long noDocInt { get; set; }

        [Column("OTP_FECHA_MUESTREO")]
        public DateTime fechaMuestreo { get; set; }

        [Column("OTP_FECHA_ENTREGA")]
        public DateTime fechaEntrega { get; set; }

        [Column("OTP_EMP_PREFIJO")]
        public string empresaPrefijo { get; set; }

        [Column("OTP_FECHA_CREACION")]
        public DateTime fechaCreacion { get; set; }

        [Column("OTP_USUARIO_CREADOR")]
        public string owner { get; set; }

        [Column("OTP_COTP_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("OTP_TCAMBIO")]
        public decimal tipoCambio { get; set; }

        [Column("OTP_VIATICOS")]
        public decimal viaticos { get; set; }

        [Column("OTP_DIRSERVICIO")]
        public string direccionServicio { get; set; }

        [Column("OTP_IVAPORCIENTO")]
        public decimal iva { get; set; }

        [Column("OTP_SUBTOTAL")]
        public decimal subtotal { get; set; }

        [Column("OTP_TOTALIVA")]
        public decimal totalIva { get; set; }

        [Column("OTP_TOTAL")]
        public decimal total { get; set; }

        [Column("OTP_CONF_CLAVE")]
        public string confClave { get; set; }

        [Column("OTP_CONF_EMISION")]
        public string confEmision { get; set; }

        [Column("OTP_CONF_VIGENCIA")]
        public string confVigencia { get; set; }

        [Column("OTP_CONF_TIPODOC")]
        public string confTipoDoc { get; set; }

        [Column("OTP_CONF_REVISION")]
        public string confRevision { get; set; }
        
        [Column("OTP_FORMATO_OT")]
        public string confFormato { get; set; }

        [Column("OTP_NOTE_ID")]
        public long noteId { get; set; }

        [Column("OTP_CHECK1")]
        public bool check1 { get; set; }

        [Column("OTP_CHECK2")]
        public bool check2 { get; set; }

        [Column("OTP_CHECK3")]
        public bool check3 { get; set; }

        [Column("OTP_CHECK4")]
        public bool check4 { get; set; }

        [Column("OTP_MON_ID")]        
        public long monedaId { get; set; }
        public virtual Moneda Moneda { get; set; }

        [Column("OTP_COTP_ID")]
        public long preliminaresId { get; set; }
        public virtual Preliminar Preliminar { get; set; }

        [Column("OTP_FPAG_ID")]
        public long formaPagoId { get; set; }
        public virtual FormaPago FormaPago { get; set; }

        [Column("OTP_EMP_ID")]
        public long empresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        [Column("OTP_CSUC_ID")]
        public long sucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [Column("OTP_OTS_ID")]
        public long statusOTPreliminarId { get; set; }
        public virtual StatusOTPreliminar StatusOTPreliminar { get; set; }

        [Column("OTP_CLI_ID")]
        public long clienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("OTP_CLAS_ID")]
        public long clasificacionServicioId { get; set; }
        public virtual ClasificacionServicio ClasificacionServicio { get; set; }

        public virtual ICollection<OTPrePartidas> OTPrePartidas { get; set; }
    }

}
