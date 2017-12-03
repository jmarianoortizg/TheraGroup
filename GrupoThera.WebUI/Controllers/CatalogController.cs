using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Models.Catalog;
using GrupoThera.WebUI.Utils;
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

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        #endregion Constructor

        #region Methods

        #region areaServicio
        [CustomAuthorizeAttribute(privilege = "AreaServicio,GeneralCatalog")]

        public ActionResult AreaServicio()
        {
            var allAreaServicio = _catalogService.getAreaServicios();
            var model = new CatalogModel()
            {
                AllAreaServicio = allAreaServicio,
                listAreaServicio = DropListHelper.GetAreaServicios(allAreaServicio),
                listClasificacionServicio = DropListHelper.GetClasificacionServicio(_catalogService.getClasificacionServicio())

            };
            return View(model);
        }

        public ActionResult CreateAreaServicio(CatalogModel CatalogModel)
        {
            try
            {
                try
                {
                    if (_catalogService.ExistAreaServicio(CatalogModel.AreaServicio.descripcion, CatalogModel.AreaServicio.clasificacionServicioId))
                        throw new Exception("El Area de servicio con los datos ingresados ya existe");
                }
                catch (Exception ex)
                {
                    return View("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(ex, "CatalogController", "ExistClient"));
                }
                _catalogService.AddAreaServicio(CatalogModel.AreaServicio);
                return RedirectToAction("AreaServicio");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }
        #endregion areaServicio

        #endregion Methods
    }
}