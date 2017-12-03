using GrupoThera.BusinessModel.Contracts.General;
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