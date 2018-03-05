using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Models.Cotizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GrupoThera.BusinessModel.Managers.Cotizacion
{
    public class CotizacionManager : ICotizacionService
    {
        #region Fields

        private IPreliminar _preliminarDA;
        private IPrePartida _prePartidaDA;
        private IConfiguracion _configuracionDA;
        private ICatalogService _catalogService;

        #endregion Fields 

        #region Constructor 

        public CotizacionManager(IPreliminar preliminarDA, IPrePartida prePartidaDA, IConfiguracion configuracionDA, ICatalogService catalogService)
        {
            //Dependency Injection
            _preliminarDA = preliminarDA;
            _prePartidaDA = prePartidaDA;
            _configuracionDA = configuracionDA;
            _catalogService = catalogService;
        }

        #endregion Constructor 

        #region Methods 

        public string createPreliminar(CotizacionModel CotizacionModel)
        {
            Preliminar item = null;
            try
            {
                var configuracion1 = _configuracionDA.GetList().ToList().Where(t => t.parametro.Equals("CVE-COT-PRE")).FirstOrDefault();
                var configuracion2 = _configuracionDA.GetList().ToList().Where(t => t.parametro.Equals("REPCOT-TIPO-DOC")).FirstOrDefault();
                var configuracion3 = _configuracionDA.GetList().ToList().Where(t => t.parametro.Equals("FORMAT_COT_WEL")).FirstOrDefault();

                item = new Preliminar()
                {
                    noDoc = 0,
                    noDocInt = 0,
                    empresaPrefijo = CotizacionModel.sucursal.prefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = CotizacionModel.preliminar.comentarios,
                    tipoCambio = CotizacionModel.moneda.tipoCambio,
                    viaticos = 0,
                    direccionServicio = CotizacionModel.preliminar.direccionServicio,
                    iva = CotizacionModel.iva,
                    subtotal = CotizacionModel.subtotal,
                    total = CotizacionModel.total,
                    confClave = configuracion1.valor1,
                    confEmision = configuracion1.valor2,
                    confVigencia = configuracion1.valor3,
                    confTipoDoc = configuracion2.valor1,
                    confRevision = configuracion1.valor2,
                    confFormato = configuracion3.valor1,
                    statusCotizacionId = 13, //Abierta
                    approbation = CotizacionModel.cotizacionFields.Where(t => t.approval == true).Count() == 0 ? false : true,
                    monedaId = CotizacionModel.selectedMoneda,
                    formaPagoId = CotizacionModel.selectedMoneda,
                    clienteId = CotizacionModel.selectedCliente,
                    clasificacionServicioId = CotizacionModel.selectedClasificacionServicio,
                    sucursalId = CotizacionModel.sucursal.sucursalId,
                    empresaId = CotizacionModel.empresa.empresaId
                };
                _preliminarDA.Add(item);

                foreach (CotizacionField cf in CotizacionModel.cotizacionFields)
                {
                    decimal iva = cf.priceFinal * .16m;
                    var prePartida = new PrePartidas()
                    {
                        claveServicio = "-",
                        descripcionServicio = cf.comentarios,
                        iva = iva,
                        precio = cf.priceFinal,
                        descuento = 0,
                        total = cf.priceFinal + iva,
                        marca = cf.marca,
                        modelo = cf.model,
                        serie = cf.noSerie,
                        comentarios = cf.comentarios,
                        cantidad = cf.qty,
                        servicioId = Convert.ToInt64(cf.claveServicioCode),
                        preliminaresId = item.preliminaresId,
                        approbation = cf.approval
                    };
                    _prePartidaDA.Add(prePartida);
                }
            }
            catch (Exception ex)
            {
                if (item.preliminaresId != 0)
                    _preliminarDA.Delete(item);
                return "ERROR: " + ex.Message;
            }
            return item.preliminaresId.ToString();
        }

        public string edicionPreliminar(CotizacionModel cotizacionModel)
        {
            try
            {
                var itemPar = cotizacionModel.preliminar;
                var itemEdit = _preliminarDA.Get(t => t.preliminaresId == itemPar.preliminaresId);

                itemEdit.comentarios = itemPar.comentarios;
                itemEdit.direccionServicio = itemPar.direccionServicio;
                itemEdit.clienteId = cotizacionModel.selectedCliente;
                itemEdit.clasificacionServicioId = cotizacionModel.selectedClasificacionServicio;
                itemEdit.monedaId = cotizacionModel.selectedMoneda;
                itemEdit.formaPagoId = cotizacionModel.selectedFormaPago;
                itemEdit.Cliente = null;
                itemEdit.ClasificacionServicio = null;
                itemEdit.Moneda = null;
                itemEdit.FormaPago = null;
                _preliminarDA.Update(itemEdit);
            }
            catch (Exception ex)
            {
                return "ERROR PRELIMINAR: " + ex.Message;
            }
            return "OK";
        }

        public string edicionPreliminar(Preliminar Preliminar)
        {
            try
            {
                _preliminarDA.Update(Preliminar);
            }
            catch (Exception ex)
            {
                return "ERROR PRELIMINAR: " + ex.Message;
            }
            return "OK";
        }

        public string edicionPartidasPreliminar(CotizacionModel cotizacionModel)
        {
            try
            {
                var idPreliminar = cotizacionModel.preliminar.preliminaresId;
                var listActual = getAllPrePartidasByPreliminar(idPreliminar);

                foreach (PrePartidas item in listActual)
                {
                    _prePartidaDA.Delete(item);
                }

                foreach (CotizacionField cf in cotizacionModel.cotizacionFields)
                {
                    decimal iva = cf.priceFinal * .16m;
                    var prePartida = new PrePartidas()
                    {
                        claveServicio = "-",
                        descripcionServicio = cf.comentarios,
                        iva = iva,
                        precio = cf.priceFinal,
                        descuento = 0,
                        total = cf.priceFinal + iva,
                        marca = cf.marca,
                        modelo = cf.model,
                        serie = cf.noSerie,
                        comentarios = cf.comentarios,
                        cantidad = cf.qty,
                        servicioId = Convert.ToInt64(cf.claveServicioCode),
                        preliminaresId = idPreliminar,
                        approbation = cf.approval
                    };
                    _prePartidaDA.Add(prePartida);
                }
            }
            catch (Exception ex)
            {
                return "ERROR PREPARTIDAS: " + ex.Message;
            }
            return "OK";
        }

        public Preliminar getPreliminarById(long idPreliminar)
        {
            return _preliminarDA.Get(t => t.preliminaresId == idPreliminar);
        }

        public IList<Preliminar> getAllPreliminar()
        {
            return _preliminarDA.GetList().ToList();
        }

        public IList<PrePartidas> getAllPrePartidasByPreliminar(long idPreliminar)
        {
            return _prePartidaDA.GetList(t => t.preliminaresId == idPreliminar).ToList();
        }

        public string newVersion(Preliminar preliminarItem)
        {
            Preliminar item = null;
            var idPadre = 0l;
            if (preliminarItem.noDoc != 0)
                idPadre = preliminarItem.noDoc;
            else
                idPadre = preliminarItem.preliminaresId;
            
            try
            {
                item = new Preliminar()
                {
                    noDoc = idPadre,
                    noDocInt = _preliminarDA.GetList(t => t.noDoc == idPadre).Count()+1,
                    empresaPrefijo = preliminarItem.empresaPrefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = preliminarItem.comentarios,
                    tipoCambio = preliminarItem.Moneda.tipoCambio,
                    viaticos = 0,
                    direccionServicio = preliminarItem.direccionServicio,
                    iva = preliminarItem.iva,
                    subtotal = preliminarItem.subtotal,
                    total = preliminarItem.total,
                    confClave = preliminarItem.confClave,
                    confEmision = preliminarItem.confEmision,
                    confVigencia = preliminarItem.confVigencia,
                    confTipoDoc = preliminarItem.confTipoDoc,
                    confRevision = preliminarItem.confRevision,
                    confFormato = preliminarItem.confFormato,
                    statusCotizacionId = 13, //Abierta
                    approbation = preliminarItem.approbation,
                    monedaId = preliminarItem.monedaId,
                    formaPagoId = preliminarItem.formaPagoId,
                    clienteId = preliminarItem.clienteId,
                    clasificacionServicioId = preliminarItem.clasificacionServicioId,
                    sucursalId = preliminarItem.sucursalId,
                    empresaId = preliminarItem.empresaId
                };
                _preliminarDA.Add(item);

                foreach (PrePartidas itemPre in preliminarItem.Prepartidas)
                {
                    var prePartida = new PrePartidas()
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
                        preliminaresId = itemPre.preliminaresId,
                        approbation = itemPre.approbation
                    };
                    _prePartidaDA.Add(prePartida);
                }
            }
            catch (Exception ex)
            {
                if (item.preliminaresId != 0)
                    _preliminarDA.Delete(item);
                return "ERROR: " + ex.Message;
            }
            return "OK";
        }

        public void disableParents(Preliminar preliminarItem)
        {
            var abierta = _catalogService.getStatusCotizacionId("ABIERTA");
            var enviadaCliente = _catalogService.getStatusCotizacionId("ENVCLIENTE");
            var obsoleta = _catalogService.getStatusCotizacionId("OBSOLETA");

            preliminarItem.statusCotizacionId = _catalogService.getStatusCotizacionId("CERRADA");
            preliminarItem.StatusCotizacion = null;
            preliminarItem.Prepartidas = null;
            _preliminarDA.Update(preliminarItem);

            //desabilita padre
            if (preliminarItem.noDoc != 0)
            {
                var padre = getPreliminarById(preliminarItem.noDoc);
                if (padre.statusCotizacionId == abierta || padre.statusCotizacionId == enviadaCliente)
                {
                    padre.statusCotizacionId = obsoleta;
                    padre.StatusCotizacion = null;
                    _preliminarDA.Update(padre);
                }
                disabledChildrens(padre.preliminaresId);
            } else
                disabledChildrens(preliminarItem.preliminaresId);
        }

        public void disabledChildrens(long idPadre)
        {
            var abierta = _catalogService.getStatusCotizacionId("ABIERTA");
            var enviadaCliente = _catalogService.getStatusCotizacionId("ENVCLIENTE");
            var obsoleta = _catalogService.getStatusCotizacionId("OBSOLETA");

            var listHijas = _preliminarDA.GetList().Where(t => t.noDoc == idPadre).ToList();
            foreach (Preliminar item in listHijas)
            {
                if (item.statusCotizacionId == abierta || item.statusCotizacionId == enviadaCliente)
                {
                    item.statusCotizacionId = obsoleta;
                    item.StatusCotizacion = null;
                    _preliminarDA.Update(item);
                }
            }
        }

        #endregion Methods  
    }
}
