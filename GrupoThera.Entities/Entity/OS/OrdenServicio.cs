using GrupoThera.Entities.Contracts;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using GrupoThera.Entities.Entity.OTPre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GrupoThera.Entities.Entity.OS
{
    [Table("OT_SERVICIOS")]
    public class OrdenServicio : IEntity
    {
        public OrdenServicio()
        {
            this.OSPrePartidas = new HashSet<OrdenServicioPartidas>();
        }

        [Key]
        [Column("OTS_ID")]
        public long ordenServicioId { get; set; }

        [Column("OTS_FECHA_MUESTREO")]
        public DateTime fechaMuestreo { get; set; }

        [Column("OTS_FECHA_ENTREGA")]
        public DateTime fechaEntrega { get; set; }

        [Column("OTS_EMP_PREFIJO")]
        public string empresaPrefijo { get; set; }

        [Column("OTS_FECHA_CREACION")]
        public DateTime fechaCreacion { get; set; }

        [Column("OTS_USUARIO_CREADOR")]
        public string owner { get; set; }

        [Column("OTS_COMENTARIOS")]
        public string comentarios { get; set; }

        [Column("OTS_TCAMBIO")]
        public decimal tipoCambio { get; set; }

        [Column("OTS_VIATICOS")]
        public decimal viaticos { get; set; }

        [Column("OTS_DIRSERVICIO")]
        public string direccionServicio { get; set; }

        [Column("OTS_IVAPORCIENTO")]
        public decimal iva { get; set; }

        [Column("OTS_SUBTOTAL")]
        public decimal subtotal { get; set; }

        [Column("OTS_TOTALIVA")]
        public decimal totalIva { get; set; }

        [Column("OTS_TOTAL")]
        public decimal total { get; set; }

        [Column("OTS_CONF_CLAVE")]
        public string confClave { get; set; }

        [Column("OTS_CONF_EMISION")]
        public string confEmision { get; set; }

        [Column("OTS_CONF_VIGENCIA")]
        public string confVigencia { get; set; }

        [Column("OTS_CONF_TIPODOC")]
        public string confTipoDoc { get; set; }

        [Column("OTS_CONF_REVISION")]
        public string confRevision { get; set; }

        [Column("OTS_FORMATO_OT")]
        public string confFormato { get; set; }

        [Column("OTS_NOTEID")]
        public long noteId { get; set; }

        [Column("OTS_CHECK1")]
        public bool check1 { get; set; }

        [Column("OTS_CHECK2")]
        public bool check2 { get; set; }

        [Column("OTS_CHECK3")]
        public bool check3 { get; set; }

        [Column("OTS_CHECK4")]
        public bool check4 { get; set; }

        [Column("OTS_MON_ID")]
        public long monedaId { get; set; }
        public virtual Moneda Moneda { get; set; }

        [Column("OTS_COTP_ID")]
        public long preliminaresId { get; set; }
        public virtual Preliminar Preliminar { get; set; }

        [Column("OTS_OTP_ID")]
        public long otPreliminarId { get; set; }
        public virtual OTPreliminar OTPreliminar { get; set; }

        [Column("OTS_FPAG_ID")]
        public long formaPagoId { get; set; }
        public virtual FormaPago FormaPago { get; set; }

        [Column("OTS_EMP_ID")]
        public long empresaId { get; set; }
        public virtual Empresa Empresa { get; set; }

        [Column("OTS_CSUC_ID")]
        public long sucursalId { get; set; }
        public virtual Sucursal Sucursal { get; set; }

        [Column("OTS_OTSS_ID")]
        public long statusOrdenServicioId { get; set; }
        public virtual StatusOrdenServicio StatusOrdenServicio { get; set; }

        [Column("OTS_CLI_ID")]
        public long clienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Column("OTS_CLAS_ID")]
        public long clasificacionServicioId { get; set; }
        public virtual ClasificacionServicio ClasificacionServicio { get; set; }

        public virtual ICollection<OrdenServicioPartidas> OSPrePartidas { get; set; }
    }
}