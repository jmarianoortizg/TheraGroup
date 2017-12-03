using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class LaboratorioController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion Fields

        #region Constructor

        public LaboratorioController(ICatalogService catalogService )
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

        [CustomAuthorizeAttribute(privilege = "LBLaboratorio,GeneralLaboratorio")]

        public ActionResult LBLaboratorio()
        {
            return View();
        }

        #endregion Methods
    }
}