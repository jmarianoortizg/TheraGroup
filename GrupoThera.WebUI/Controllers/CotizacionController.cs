using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
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

        public CotizacionController(ICatalogService catalogService,ICotizacionService cotizacionService)
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
                if (result.Equals("OK"))
                {
                    return Json(new
                    {
                        success = true,                    
                    }, JsonRequestBehavior.AllowGet);
                }
                else {
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
                    statusActual = "ABIERTA"
                };
                TempData["CotizacionSearch"] = model;
                return View(model);
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