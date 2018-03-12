using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.Core.Utils;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using GrupoThera.Entities.Models.Catalog;
using GrupoThera.Entities.Models.Cotizacion;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class CotizacionController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IOTPreliminarService _otService;
        private ICotizacionService _cotizacionService;

        #endregion Fields

        #region Constructor

        public CotizacionController(ICatalogService catalogService, ICotizacionService cotizacionService, IOTPreliminarService otService)
        {
            _catalogService = catalogService;
            _otService = otService;
            _cotizacionService = cotizacionService;
        }

        #endregion Constructor

        #region Methods

        #region NuevaCotizacion

        [CustomAuthorizeAttribute(privilege = "NuevaCotizacion,GeneralCotizacion")]

        public ActionResult NuevaCotizacion()
        {
            var allClasificacionServicios = _catalogService.getClasificacionServicio();
            var allServicios = _catalogService.getServicios();
            var model = new CotizacionModel()
            {
                listClasificacionServicio = DropListHelper.GetClasificacionServicio(_catalogService.getClasificacionServicios()),
                listServicio = DropListHelper.GetServicio(allServicios),
                listCliente = DropListHelper.GetCliente(_catalogService.getClientes()),
                listFormaPago = DropListHelper.GetFormaPago(_catalogService.getFormasPago()),
                listMoneda = DropListHelper.GetMoneda(_catalogService.getMonedas()),
                allServicio = allServicios,
                allClasificacionServicio = allClasificacionServicios
            };
            TempData["CotizacionModel"] = model;
            return View(model);
        }

        public ActionResult CreateCotizacion(CotizacionModel cotizacionModel)
        {
            var sucursal = (Sucursal)HttpContext.Session["Sucursal"];
            var empresa = (Empresa)HttpContext.Session["Empresa"];

            cotizacionModel.moneda = _catalogService.getMonedaById(cotizacionModel.selectedMoneda);
            cotizacionModel.formaPago = _catalogService.getFormaPagoById(cotizacionModel.selectedFormaPago);
            cotizacionModel.clasificacionServicio = _catalogService.getClasificacionServicioById(cotizacionModel.selectedClasificacionServicio);
            cotizacionModel.cliente = _catalogService.getClienteById(cotizacionModel.selectedCliente);
            cotizacionModel.servicio = _catalogService.getServicioById(cotizacionModel.selectedServicio);
            cotizacionModel.sucursal = sucursal;
            cotizacionModel.empresa = empresa;

            var result = _cotizacionService.createPreliminar(cotizacionModel);
            if (!result.Contains("ERROR"))
            {
                return Json(new
                {
                    idPreliminar = result,
                    success = true,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    responseHtml = StdClassWeb.RenderToString(PartialView("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(new Exception(result), "CatalogController", "CreateClient")), HttpContext)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult addLineConcepts(CotizacionField cotizacionField)
        {
            var model = new CotizacionModel()
            {
                cotizacionField = cotizacionField
            };
            return PartialView("~/Views/Cotizacion/NuevaCotizacionNewLineConcept.cshtml", model);
        }

        public ActionResult refreshPrices(CotizacionField cotizacionField)
        {
            var model = (CotizacionModel)TempData["CotizacionModel"];
            TempData.Keep("CotizacionModel");
            model.servicio = model.allServicio.Where(t => t.servicioId == cotizacionField.idServicio).FirstOrDefault();
            return PartialView("~/Views/Cotizacion/NuevaCotizacionNewPrices.cshtml", model);
        }

        #endregion NuevaCotizacion

        #region SearchCotizacion

        [CustomAuthorizeAttribute(privilege = "SearchCotizacion,GeneralCotizacion")]

        public ActionResult SearchCotizacion()
        {
            var model = generateInitialModel();
            TempData["CotizacionSearch"] = model;
            return View(model);
        }

        public CotizacionSearch generateInitialModel()
        {
            var allPreliminares = _cotizacionService.getAllPreliminar();
            var model = new CotizacionSearch()
            {
                listaPreliminares = allPreliminares,
                abiertas = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("ABIERTA")).ToList(),
                aprobacion = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("APROBACION")).ToList(),
                cerrados = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("CERRADA")).ToList(),
                canceladas = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("CANCEL")).ToList(),
                rechazadas = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("RECHAZADA")).ToList(),
                clientes = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("ENVCLIENTE")).ToList(),
                preliminaresActual = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("ABIERTA")).ToList(),
                listClasificacion = DropListHelper.GetClasificacionServicioValue0(_catalogService.getClasificacionServicios()),
                listCliente = DropListHelper.GetClienteValue0(_catalogService.getClientes()),
                listStatus = DropListHelper.GetStatusCotizacion(_catalogService.getStatusCotizacion()),
            };
            return model;
        } 

        public ActionResult searchCotizacionFilter(CotizacionSearch CotizacionSearch)
        {
            var predicate = PredicateBuilder.False<Preliminar>();
            var model = (CotizacionSearch)TempData["CotizacionSearch"];
            TempData.Keep("CotizacionSearch");

            var allPreliminares = _cotizacionService.getAllPreliminar();

            if (CotizacionSearch.selectedCliente != 0)
            {
                allPreliminares = allPreliminares.Where(i => i.clienteId == CotizacionSearch.selectedCliente).ToList();
            }
            if (!string.IsNullOrEmpty(CotizacionSearch.from) && !string.IsNullOrEmpty(CotizacionSearch.to))
            {
                var dateFrom = DateTime.Parse(CotizacionSearch.from);
                var dateTo = DateTime.Parse(CotizacionSearch.to);
                allPreliminares = allPreliminares.Where(t => t.fechaCreacion.Date >= dateFrom.Date && t.fechaCreacion.Date <= dateTo).ToList();
            }
            if (CotizacionSearch.selectedStatus != 0)
            {
                allPreliminares = allPreliminares.Where(i => i.statusCotizacionId == CotizacionSearch.selectedStatus).ToList();
            }
            if (CotizacionSearch.selectedClasificacion != 0)
            {
                allPreliminares = allPreliminares.Where(i => i.clasificacionServicioId == CotizacionSearch.selectedClasificacion).ToList();
            }
            if (CotizacionSearch.nCotizacion != 0)
            {
                allPreliminares = allPreliminares.Where(i => i.preliminaresId == CotizacionSearch.nCotizacion || i.noDoc == CotizacionSearch.nCotizacion).ToList();
            }
            if (!string.IsNullOrEmpty(CotizacionSearch.marca))
            {
                allPreliminares = allPreliminares.Where(i => i.marca.Equals(CotizacionSearch.marca)).ToList();
            }
            if (!string.IsNullOrEmpty(CotizacionSearch.modelo))
            {
                allPreliminares = allPreliminares.Where(i => i.modelo.Equals(CotizacionSearch.modelo)).ToList();
            }
            if (!string.IsNullOrEmpty(CotizacionSearch.nSerie))
            {
                allPreliminares = allPreliminares.Where(i => i.serie.Equals(CotizacionSearch.nSerie)).ToList();
            }

            CotizacionSearch.preliminaresActual = allPreliminares;
            CotizacionSearch.abiertas = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("ABIERTA")).ToList();
            CotizacionSearch.aprobacion = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("APROBACION")).ToList();
            CotizacionSearch.cerrados = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("CERRADA")).ToList();
            CotizacionSearch.canceladas = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("CANCEL")).ToList();
            CotizacionSearch.rechazadas = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("RECHAZADA")).ToList();
            CotizacionSearch.clientes = allPreliminares.Where(t => t.StatusCotizacion.codigo.Equals("ENVCLIENTE")).ToList();

            return Json(new
            {
                success = true,
                cotizacionHtml = StdClassWeb.RenderToString(PartialView("SearchCotizacionItem", CotizacionSearch), HttpContext),
                cotizacionCountersHtml = StdClassWeb.RenderToString(PartialView("SearchCotizacionCounters", CotizacionSearch), HttpContext)
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchPreliminarView(int idPreliminarSelected)
        {
            var preliminarItem = _cotizacionService.getPreliminarById(idPreliminarSelected);
            preliminarItem.Prepartidas = _cotizacionService.getAllPrePartidasByPreliminar(preliminarItem.preliminaresId);
            return Json(new
            {
                responseCotizationView = StdClassWeb.RenderToString(PartialView("~/Views/Cotizacion/SearchCotizacionView.cshtml", preliminarItem), HttpContext),
                responseCotizationStatus = StdClassWeb.RenderToString(PartialView("~/Views/Cotizacion/SearchCotizacionStatus.cshtml", preliminarItem), HttpContext)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult showPreliminarCounterView(string statusCounter)
        {
            var model = (CotizacionSearch)TempData["CotizacionSearch"];
            TempData.Keep("CotizacionSearch");

            if (statusCounter.Equals("OPEN"))
                model.preliminaresActual = model.abiertas;
            else if (statusCounter.Equals("APROVE"))
                model.preliminaresActual = model.aprobacion;
            else if (statusCounter.Equals("CLOSED"))
                model.preliminaresActual = model.cerrados;
            else if (statusCounter.Equals("CANCEL"))
                model.preliminaresActual = model.canceladas;
            else if (statusCounter.Equals("REJECTED"))
                model.preliminaresActual = model.rechazadas;
            else if (statusCounter.Equals("ENVCLIENTE"))
                model.preliminaresActual = model.clientes;
            return Json(new
            {
                success = true,
                cotizacionHtml = StdClassWeb.RenderToString(PartialView("SearchCotizacionItem", model), HttpContext),
                cotizacionCountersHtml = StdClassWeb.RenderToString(PartialView("SearchCotizacionCounters", model), HttpContext)
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchCotizcionEdicion(string preliminarId)
        {
            int intAgain = int.Parse(preliminarId, System.Globalization.NumberStyles.HexNumber);
            var preliminarItem = _cotizacionService.getPreliminarById(intAgain);
            preliminarItem.Prepartidas = _cotizacionService.getAllPrePartidasByPreliminar(preliminarItem.preliminaresId);

            var allClasificacionServicios = _catalogService.getClasificacionServicio();
            var allServicios = _catalogService.getServicios();
            var model = new CotizacionModel()
            {
                listClasificacionServicio = DropListHelper.GetClasificacionServicio(_catalogService.getClasificacionServicios()),
                listServicio = DropListHelper.GetServicio(allServicios),
                listCliente = DropListHelper.GetCliente(_catalogService.getClientes()),
                listFormaPago = DropListHelper.GetFormaPago(_catalogService.getFormasPago()),
                listMoneda = DropListHelper.GetMoneda(_catalogService.getMonedas()),
                allServicio = allServicios,
                allClasificacionServicio = allClasificacionServicios,
                preliminar = preliminarItem
            };

            model.preliminar.comentarios = preliminarItem.comentarios;
            model.preliminar.direccionServicio = preliminarItem.direccionServicio;
            model.preliminar.preliminaresId = preliminarItem.preliminaresId;
            model.selectedCliente = Convert.ToInt16(preliminarItem.clienteId);
            model.selectedClasificacionServicio = Convert.ToInt16(preliminarItem.clasificacionServicioId);
            model.selectedFormaPago = Convert.ToInt16(preliminarItem.formaPagoId);
            model.selectedMoneda = Convert.ToInt16(preliminarItem.monedaId);

            TempData["CotizacionModel"] = model;
            return View("SearchCotizacionEdition", model);
        }

        public ActionResult EditCotizacion(CotizacionModel cotizacionModel)
        {
            var result = _cotizacionService.edicionPreliminar(cotizacionModel);
            if (result.Equals("OK"))
            {
                result = _cotizacionService.edicionPartidasPreliminar(cotizacionModel);
                if (result.Equals("OK"))
                {
                    return Json(new
                    {
                        success = true
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                success = false,
                responseHtml = StdClassWeb.RenderToString(PartialView("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(new Exception(result), "CatalogController", "CreateClient")), HttpContext)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult changePreliminarStatus(string statusPreliminar, long idPreliminar)
        {
            try
            {   
                var preliminarItem = _cotizacionService.getPreliminarById(idPreliminar);
                var messageSuccess = "";
                var messageHeader = false;
                if (statusPreliminar.Equals("ABIERTA") || statusPreliminar.Equals("ENVCLIENTE") || statusPreliminar.Equals("CANCEL"))
                {
                    var status = _catalogService.getStatusCotizacion().Where(t => t.codigo.Equals(statusPreliminar)).FirstOrDefault();
                    preliminarItem.StatusCotizacion = status;
                    preliminarItem.statusCotizacionId = status.statusCotizacionId;
                    var result = _cotizacionService.edicionPreliminar(preliminarItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Cambio de estado correctemente";
                }
                else if (statusPreliminar.Equals("OT"))
                {
                    preliminarItem.Prepartidas = _cotizacionService.getAllPrePartidasByPreliminar(preliminarItem.preliminaresId);
                    var result = _otService.createOT(preliminarItem);
                    if (result.Contains("Error"))
                        throw new Exception(result);
                    messageSuccess = "Nueva Pre-Orden de trabajo Creada #" + result;
                    messageHeader = true;
                    _cotizacionService.disableParents(preliminarItem);
                }
                else if (statusPreliminar.Equals("CERRADA"))
                {   
                    var result = _cotizacionService.duplicateCotizacion(preliminarItem);
                    if (result.Contains("Error"))
                        throw new Exception(result);
                    messageSuccess = "Nueva Cotizacion Duplicada Creada #" + result;
                    messageHeader = true;
                }
                else if (statusPreliminar.Equals("NEWVERSION"))
                {
                    preliminarItem.Prepartidas = _cotizacionService.getAllPrePartidasByPreliminar(preliminarItem.preliminaresId);
                    var result = _cotizacionService.newVersion(preliminarItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Nueva version creada correctamente";
                }

                var model = generateInitialModel();     
                return Json(new
                {
                    success = true,
                    messageSuccess = messageSuccess,
                    headerMessage = messageHeader, 
                    cotizacionCounters = StdClassWeb.RenderToString(PartialView("SearchCotizacionCounters", model), HttpContext)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    responseHtml = StdClassWeb.RenderToString(PartialView("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(new Exception(ex.Message), "CotizationController", "changePreliminarStatus")), HttpContext)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion SearchCotizacion

        #region ReportCotizacion

        [CustomAuthorizeAttribute(privilege = "ReportCotizacion,GeneralCotizacion")]

        public ActionResult ReportCotizacion()
        {
            return View();
        }

        #endregion ReportCotizacion

        #endregion Methods
    }
}