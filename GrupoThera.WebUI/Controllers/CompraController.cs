using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class CompraController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion Fields

        #region Constructor

        public CompraController(ICatalogService catalogService )
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

        [CustomAuthorizeAttribute(privilege = "FCNewCompras,GeneralCompra")]

        public ActionResult NewCompra()
        {
            return View();
        }

        #endregion Methods
    }
}