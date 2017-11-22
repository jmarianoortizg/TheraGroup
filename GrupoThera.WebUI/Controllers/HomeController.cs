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
        public ActionResult Index()
        {
            if (HttpContext.Session["Identificated"].ToString().Equals("False"))
                return View("~/Views/General/Loginview.cshtml");
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
            return View("~/Views/General/Loginview.cshtml");
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


        /// borrame
        /// 
        [CustomAuthorizeAttribute(privilege = "Developer")]
        public ActionResult Developer()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "Administrador")]
        public ActionResult Admin()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "Other")]
        public ActionResult Other()
        {
            return View();
        }




    }
}