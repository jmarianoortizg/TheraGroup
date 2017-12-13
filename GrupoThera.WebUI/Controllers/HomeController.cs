using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Entity.General;
using GrupoThera.Entities.Models.General;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IRoleAccountService _roleAccountService;

        #endregion Fields

        #region Constructor

        public HomeController( ICatalogService catalogService,
                               IRoleAccountService roleAccountService
                             )
        {
            _catalogService = catalogService;
            _roleAccountService = roleAccountService;
        }

        #endregion Constructor

        #region Methods
        public ActionResult Index()
        {
            var username = (string)HttpContext.Session["UserName"];
            if (username == null) {
                var model = new UserLoginModel()
                {
                    listEmpresas = DropListHelper.GetEmpresas(_catalogService.getEmpresas()),
                    listSucursal = DropListHelper.GetSucursales(_catalogService.getSucursalesbyEmpresa(1))
                };
                return View("~/Views/General/Loginview.cshtml",model);
            }
            else
            {
                var empresa = (Empresa)HttpContext.Session["Empresa"];
                HttpContext.Session["EmpresaName"] = empresa.nombre;
                var sucursal = (Sucursal)HttpContext.Session["Sucursal"];
                HttpContext.Session["SucursalName"] = sucursal.nombre;
                return View("Dashboard");
            }
        }

        public ActionResult getSucursalByEmpresa(string term)
        {
            return null;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            RemoveSessionVariables();
            var model = new UserLoginModel()
            {
                listEmpresas = DropListHelper.GetEmpresas(_catalogService.getEmpresas()),
                listSucursal = DropListHelper.GetSucursales(_catalogService.getSucursalesbyEmpresa(1))
            };
            return View("~/Views/General/Loginview.cshtml",model);
        }

        private void RemoveSessionVariables()
        {
            HttpContext.Session["Account"] = null;
            HttpContext.Session["UserName"] = null;
            HttpContext.Session["ListRoles"] = null;
            HttpContext.Session["Password"] = null;
            HttpContext.Session["Empresa"] = null;
            HttpContext.Session["Sucursal"] = null;
            HttpContext.Session["EmpresaName"] = null;
            HttpContext.Session["SucursalName"] = null;
            HttpContext.Session["Identificated"] = false;

            var userInfo = Request.Cookies["userInfoThera"];

            if (userInfo != null)
            {
                var c = new HttpCookie("userInfoThera");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
        }

        #endregion Methods
    }
}