using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
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
        private ICotizacionService _cotizacionService;

        #endregion Fields

        #region Constructor

        public CotizacionController(ICatalogService catalogService, ICotizacionService cotizacionService)
        {
            _catalogService = catalogService;
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
            TempData["CotizacionSearch"] = model;
            return View(model);
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
                allPreliminares = allPreliminares.Where(i => i.preliminaresId == CotizacionSearch.nCotizacion).ToList();
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
            return PartialView("~/Views/Cotizacion/SearchCotizacionView.cshtml",preliminarItem);
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
            else if (statusCounter.Equals("SENDCLIENT"))
                model.preliminaresActual = model.rechazadas;
            return PartialView("SearchCotizacionItem", model);
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