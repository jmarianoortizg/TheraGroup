using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessLogic.Contracts.OT;
using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.OTPre;
using GrupoThera.Entities.Models.Cotizacion;
using GrupoThera.Entities.Models.OTPre;
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

        public string edicionOTPreliminar(OTPreliminar OTPreliminar)
        {
            try
            {
                _otPreliminarDA.Update(OTPreliminar);
            }
            catch (Exception ex)
            {
                return "ERROR OTPRELIMINAR: " + ex.Message;
            }
            return "OK";
        }

        public string edicionOTPreliminar(OTPreliminarSearch otpreliminarSearch)
        {
            try
            {
                var itemPar = otpreliminarSearch.otpreliminar;
                var itemEdit = _otPreliminarDA.Get(t => t.otPreliminarId == itemPar.otPreliminarId);

                if (itemEdit.StatusOTPreliminar.Equals("ABIERTA"))
                {
                    itemEdit.Cliente = null;
                    itemEdit.ClasificacionServicio = null;
                    itemEdit.Moneda = null;
                    itemEdit.FormaPago = null;
                    _otPreliminarDA.Update(itemEdit);
                }
                else
                {
                    throw new Exception("El estatus de la OT Preliminar ha cambiado y no esta abierta actualmente");
                }
            }
            catch (Exception ex)
            {
                return "ERROR OTPRELIMINAR: " + ex.Message;
            }
            return "OK";
        }

        public string newVersion(OTPreliminar otpreliminarItem)
        {
            OTPreliminar item = null;
            var idPadre = 0l;
            if (otpreliminarItem.noDoc != 0)
                idPadre = otpreliminarItem.noDoc;
            else
                idPadre = otpreliminarItem.otPreliminarId;

            try
            {
                item = new OTPreliminar()
                {
                    noDoc = idPadre,
                    noDocInt = _otPreliminarDA.GetList(t => t.noDoc == idPadre).Count() + 1,
                    empresaPrefijo = otpreliminarItem.empresaPrefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = otpreliminarItem.comentarios,
                    tipoCambio = otpreliminarItem.Moneda.tipoCambio,
                    viaticos = 0,
                    direccionServicio = otpreliminarItem.direccionServicio,
                    iva = otpreliminarItem.iva,
                    subtotal = otpreliminarItem.subtotal,
                    total = otpreliminarItem.total,
                    confClave = otpreliminarItem.confClave,
                    confEmision = otpreliminarItem.confEmision,
                    confVigencia = otpreliminarItem.confVigencia,
                    confTipoDoc = otpreliminarItem.confTipoDoc,
                    confRevision = otpreliminarItem.confRevision,
                    confFormato = otpreliminarItem.confFormato,
                    statusOTPreliminarId = _catalogService.getStatusOTPreliminarId("ABIERTA"),
                    monedaId = otpreliminarItem.monedaId,
                    formaPagoId = otpreliminarItem.formaPagoId,
                    clienteId = otpreliminarItem.clienteId,
                    clasificacionServicioId = otpreliminarItem.clasificacionServicioId,
                    sucursalId = otpreliminarItem.sucursalId,
                    empresaId = otpreliminarItem.empresaId,
                    preliminaresId = otpreliminarItem.preliminaresId
                };
                _otPreliminarDA.Add(item);

                foreach (OTPrePartidas itemPre in otpreliminarItem.OTPrePartidas)
                {
                    var prePartida = new OTPrePartidas()
                    {
                        claveServicio = "-",
                        descripcionServicio = itemPre.descripcionServicio,
                        iva = itemPre.iva,
                        precio = itemPre.precio,
                        descuento = 0,
                        total = itemPre.total,
                        marca = itemPre.marca,
                        modelo = itemPre.modelo,
                        serie = itemPre.serie,
                        comentarios = itemPre.comentarios,
                        cantidad = itemPre.cantidad,
                        servicioId = itemPre.servicioId,
                        otPreliminarId = item.otPreliminarId,
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
            return "OK";
        }

        public void disableParents(OTPreliminar otpreliminarItem)
        {
            var abierta = _catalogService.getStatusOTPreliminarId("ABIERTA");
            var enviadaLab = _catalogService.getStatusOTPreliminarId("ENVLAB");
            var obsoleta = _catalogService.getStatusOTPreliminarId("OBSOLETA");

            otpreliminarItem.statusOTPreliminarId = _catalogService.getStatusOTPreliminarId("CERRADA");
            otpreliminarItem.StatusOTPreliminar = null;
            otpreliminarItem.OTPrePartidas = null;
            _otPreliminarDA.Update(otpreliminarItem);

            //desabilita padre
            if (otpreliminarItem.noDoc != 0)
            {
                var padre = getOTPreliminarById(otpreliminarItem.noDoc);
                if (padre.statusOTPreliminarId == abierta || padre.statusOTPreliminarId == enviadaLab)
                {
                    padre.statusOTPreliminarId = obsoleta;
                    padre.StatusOTPreliminar = null;
                    _otPreliminarDA.Update(padre);
                }
                disabledChildrens(padre.preliminaresId);
            }
            else
                disabledChildrens(otpreliminarItem.preliminaresId);
        }

        public void disabledChildrens(long idPadre)
        {
            var abierta = _catalogService.getStatusOTPreliminarId("ABIERTA");
            var enviadaLab = _catalogService.getStatusOTPreliminarId("ENVLAB");
            var obsoleta = _catalogService.getStatusOTPreliminarId("OBSOLETA");

            var listHijas =_otPreliminarDA.GetList().Where(t => t.noDoc == idPadre).ToList();
            foreach (OTPreliminar item in listHijas)
            {
                if (item.statusOTPreliminarId == abierta || item.statusOTPreliminarId == enviadaLab)
                {
                    item.statusOTPreliminarId = obsoleta;
                    item.StatusOTPreliminar = null;
                    _otPreliminarDA.Update(item);
                }
            }
        }

        public string duplicateOTPreliminar(OTPreliminar OTPreliminar)
        {
            OTPreliminar item = null;
            try
            {
                item = new OTPreliminar()
                {
                    noDoc = 0,
                    noDocInt = 0,
                    empresaPrefijo = OTPreliminar.empresaPrefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = OTPreliminar.comentarios,
                    tipoCambio = OTPreliminar.tipoCambio,
                    viaticos = OTPreliminar.viaticos,
                    direccionServicio = OTPreliminar.direccionServicio,
                    iva = OTPreliminar.iva,
                    subtotal = OTPreliminar.subtotal,
                    totalIva = OTPreliminar.totalIva,
                    total = OTPreliminar.total,
                    confClave = OTPreliminar.confClave,
                    confEmision = OTPreliminar.confEmision,
                    confVigencia = OTPreliminar.confVigencia,
                    confTipoDoc = OTPreliminar.confTipoDoc,
                    confRevision = OTPreliminar.confRevision,
                    confFormato = OTPreliminar.confFormato,
                    monedaId = OTPreliminar.monedaId,
                    preliminaresId = OTPreliminar.preliminaresId,
                    formaPagoId = OTPreliminar.formaPagoId,
                    empresaId = OTPreliminar.empresaId,
                    sucursalId = OTPreliminar.sucursalId,
                    statusOTPreliminarId = _catalogService.getStatusCotizacionId("ENVLAB"), 
                    clienteId = OTPreliminar.clienteId,
                    clasificacionServicioId = OTPreliminar.clasificacionServicioId
                };
                _otPreliminarDA.Add(item);

                item.OTPrePartidas = getAllPrePartidasByOTPreliminar(item.otPreliminarId);

                foreach (OTPrePartidas itempp in item.OTPrePartidas)
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
                        otPreliminarId = item.preliminaresId
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


        #endregion Methods  
    }
}
