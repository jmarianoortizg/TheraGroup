using GrupoThera.BusinessModel.Contracts.General;
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
            if (HttpContext.Session["Identificated"].ToString().Equals("False")) {
                var model = new UserLoginModel()
                {
                    listEmpresas = DropListHelper.GetDepartamentos(_catalogService.getDepartamentos()),
                    listSucursal = DropListHelper.GetSucursales(_catalogService.getSucursales())
                };
                return View("~/Views/General/Loginview.cshtml",model);
            }
            else
            {
                DashboardModel model = null;
                var TempRolesList = (List<string>)HttpContext.Session["ListRoles"];
                if (TempRolesList != null)
                {
                    model = new DashboardModel()
                    {
                        priviledgesList = TempRolesList,
                        userName = HttpContext.Session["UserName"].ToString()
                    };
                }
                return View("Dashboard", model);
            }
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
                listEmpresas = DropListHelper.GetDepartamentos(_catalogService.getDepartamentos()),
                listSucursal = DropListHelper.GetSucursales(_catalogService.getSucursales())
            };
            return View("~/Views/General/Loginview.cshtml",model);
        }

        private void RemoveSessionVariables()
        {
            HttpContext.Session["Account"] = null;
            HttpContext.Session["UserName"] = null;
            HttpContext.Session["ListRoles"] = null;
            HttpContext.Session["Password"] = null;
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