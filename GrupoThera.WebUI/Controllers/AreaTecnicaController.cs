using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class AreaTecnicaController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion Fields

        #region Constructor

        public AreaTecnicaController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

        [CustomAuthorizeAttribute(privilege = "ATPreliminar,GeneralAreaTecnica")]
        public ActionResult ATPreliminar()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "ATServicio,GeneralAreaTecnica")]
        public ActionResult ATServicio()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "ATReporte,GeneralAreaTecnica")]
        public ActionResult ATReporte()
        {
            return View();
        }

        #endregion Methods
    }
}