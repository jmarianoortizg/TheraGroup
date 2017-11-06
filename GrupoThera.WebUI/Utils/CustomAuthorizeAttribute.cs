using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GrupoThera.WebUI.Utils
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    { 
        //Custom named parameters for annotation
        public string privilege { get; set; }

        public StatesAuthorization status { get; set; }

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (status == StatesAuthorization.NoPriviledge)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "Home", action = "Index" })
                );
            }
            else if (status == StatesAuthorization.UnAuthorization)
            {
                filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary(new { controller = "Roles", action = "LoginUnAuthorized", unAuthorized = false })
               );

            }
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var TempRolesList = (List<string>)HttpContext.Current.Session["ListRoles"];

            if (TempRolesList == null)
            {
                status = StatesAuthorization.NoPriviledge;
                return false;
            }

            var controllerRoles = privilege.Split(',').ToList();
            foreach (string role in controllerRoles)
            {
                if (TempRolesList.Contains(role) || TempRolesList.Contains("Developer"))
                {
                    return true;
                }
            }

            status = StatesAuthorization.UnAuthorization;
            return false;
        }
    }
    public enum StatesAuthorization
    {
        UnAuthorization,
        NoPriviledge
    }
}