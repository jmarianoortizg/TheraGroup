using GrupoThera.Entities.Models.General;
using GrupoThera.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoThera.Entities.Entity.General;

namespace GrupoThera.WebUI.Controllers
{
    public class RolesController : Controller
    {
        [HttpPost]
        public JsonResult loginUser(UserLoginModel userLogin)
        { 
            var listRoles = new List<Role>();

            var a = new Role()
            {
                RoleName = "Administrador"
            };

            var b = new Role()
            {
                RoleName = "Developer"
            };  

            listRoles.Add(a);

            if (listRoles.Count != 0)
            {
                HttpContext.Session["Account"] = userLogin.UserName;
                HttpContext.Session["UserName"] = "TODO";
                HttpContext.Session["ListRoles"] = listRoles.Select(x => x.RoleName.ToString()).ToList();
                HttpContext.Session["Password"] = userLogin.Password;

                if (userLogin.stayLogin)
                {
                    cookieData cookie = new cookieData();
                    cookie.Password = userLogin.Password;
                    cookie.UserName = userLogin.UserName.ToUpper();
                    cookie.ListRoles = listRoles.Select(x => x.RoleName.ToString()).ToList();
                    Response.Cookies.Add(StandardClass.CreateCookie(cookie));
                }
                return Json(new
                {
                    success = true,  
                },
                JsonRequestBehavior.AllowGet);
            }
            else
            {
                HttpContext.Session["Account"] = null;
                HttpContext.Session["UserName"] = null;
                HttpContext.Session["ListRoles"] = null;
                HttpContext.Session["Password"] = null;
                return Json(new
                {
                    success = false 
                },
                JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult LoginUnAuthorized(bool unAuthorized)
        {
            return View("~/Views/Shared/_Unauthorized.cshtml", unAuthorized);
        }

    }
}