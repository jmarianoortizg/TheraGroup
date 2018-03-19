using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessLogic.Contracts.OT;
using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.OTPre;
using GrupoThera.Entities.Models.Cotizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GrupoThera.BusinessModel.Managers.OT
{
    public class OTPreliminarManager : IOTPreliminarService
    {
        #region Fields

        private IOTPreliminar _otPreliminarDA;
        private IOTPrePartida _otPrePartidaDA;
        private IConfiguracion _configuracionDA;
        private ICatalogService _catalogService;

        #endregion Fields 

        #region Constructor 

        public OTPreliminarManager(IOTPreliminar otPreliminarDA, IOTPrePartida otPrePartidaDA, IConfiguracion configuracionDA, ICatalogService catalogService)
        {
            //Dependency Injection
            _otPreliminarDA = otPreliminarDA;
            _otPrePartidaDA = otPrePartidaDA;
            _configuracionDA = configuracionDA;
            _catalogService = catalogService;
        }

        #endregion Constructor 

        #region Methods 

        public string createOT(Preliminar Preliminar)
        {
            OTPreliminar item = null;
            try
            {
                item = new OTPreliminar()
                {
                    noDoc = 0,
                    noDocInt = 0,
                    empresaPrefijo = Preliminar.empresaPrefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = Preliminar.comentarios,
                    tipoCambio = Preliminar.tipoCambio,
                    viaticos = Preliminar.viaticos,
                    direccionServicio = Preliminar.direccionServicio,
                    iva = Preliminar.iva,
                    subtotal = Preliminar.subtotal,
                    totalIva = Preliminar.totalIva,
                    total = Preliminar.total,
                    confClave = Preliminar.confClave,
                    confEmision = Preliminar.confEmision,
                    confVigencia = Preliminar.confVigencia,
                    confTipoDoc = Preliminar.confTipoDoc,
                    confRevision = Preliminar.confRevision,
                    confFormato = Preliminar.confFormato,
                    monedaId = Preliminar.monedaId,
                    preliminaresId = Preliminar.preliminaresId,
                    formaPagoId = Preliminar.formaPagoId,
                    empresaId = Preliminar.empresaId,
                    sucursalId = Preliminar.sucursalId,
                    statusOTPreliminarId = _catalogService.getStatusOTPreliminarId("ABIERTA"),
                    clienteId = Preliminar.clienteId,
                    clasificacionServicioId = Preliminar.clasificacionServicioId
                };
                _otPreliminarDA.Add(item);

                foreach (PrePartidas itempp in Preliminar.Prepartidas)
                {
                    var prePartida = new OTPrePartidas()
                    {
                        claveServicio = "-",
                        descripcionServicio = itempp.comentarios,
                        iva = itempp.iva,
                        precio = itempp.precio,
                        descuento = 0,
                        total = itempp.precio + itempp.iva,
                        marca = itempp.marca,
                        modelo = itempp.modelo,
                        serie = itempp.serie,
                        comentarios = itempp.comentarios,
                        cantidad = itempp.cantidad,
                        servicioId = Convert.ToInt64(itempp.Servicio.servicioId),
                        otPreliminarId = item.otPreliminarId
                    };
                    _otPrePartidaDA.Add(prePartida);
                }
            }
            catch (Exception ex)
            {
                if (item.otPreliminarId != 0)
                    _otPreliminarDA.Delete(item);
                return "ERROR: " + ex.Message;
            }
            return item.otPreliminarId.ToString();
        }

        public IList<OTPreliminar> getAllOTPreliminar()
        {
            return _otPreliminarDA.GetList().ToList();
        }

        public OTPreliminar getOTPreliminarById(long idOTPreliminar)
        {
            return _otPreliminarDA.Get(t => t.otPreliminarId == idOTPreliminar);
        }

        public IList<OTPrePartidas> getAllPrePartidasByOTPreliminar(long idOTPreliminar)
        {
            return _otPrePartidaDA.GetList(t => t.otPreliminarId == idOTPreliminar).ToList();
        }

        #endregion Methods  
    }
}
