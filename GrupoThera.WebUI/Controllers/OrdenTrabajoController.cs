using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class OrdenTrabajoController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion Fields

        #region Constructor

        public OrdenTrabajoController(ICatalogService catalogService )
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

        [CustomAuthorizeAttribute(privilege = "OTPreliminar,GeneralOrdenTrabajo")]

        public ActionResult OTPreliminar()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "OTServicio,GeneralOrdenTrabajo")]

        public ActionResult OTServicio()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "OTReporte,GeneralOrdenTrabajo")]

        public ActionResult OTReporte()
        {
            return View();
        }

        #endregion Methods
    }
}