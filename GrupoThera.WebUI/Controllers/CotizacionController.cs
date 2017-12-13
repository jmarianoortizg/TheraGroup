using GrupoThera.BusinessModel.Contracts.General;
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

        #endregion Fields

        #region Constructor

        public CotizacionController(ICatalogService catalogService )
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

        [CustomAuthorizeAttribute(privilege = "NuevaCotizacion,GeneralCotizacion")]

        public ActionResult NuevaCotizacion()
        {
            var model = new CotizacionModel()
            {
                listClasificacionServicio = DropListHelper.GetClasificacionServicio(_catalogService.getClasificacionServicios()),
                listAreaServicio = DropListHelper.GetAreaServicios(_catalogService.getAreaServicios()),
                listCliente = DropListHelper.GetCliente(_catalogService.getClientes()),
                listFormaPago = DropListHelper.GetFormaPago(_catalogService.getFormasPago())
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateCotizacion()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "SearchCotizacion,GeneralCotizacion")]

        public ActionResult SearchCotizacion()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "ReportCotizacion,GeneralCotizacion")]

        public ActionResult ReportCotizacion()
        {
            return View();
        }

        #endregion Methods
    }
}