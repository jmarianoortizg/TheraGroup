using GrupoThera.BusinessModel.Contracts.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class CatalogController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion Fields

        #region Constructor

        public CatalogController( ICatalogService catalogService )
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

            #region areaServicio
                public ActionResult AreaServicio()
                {
                    return View();
                }
            #endregion areaServicio

        #endregion Methods
    }
}