using GrupoThera.Core.Utils;
using GrupoThera.WebUI.App_Start;
using GrupoThera.WebUI.DependencyResolvers;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GrupoThera.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        protected void Session_Start()
        {
            var UserCookie = Request.Cookies["userInfoThera"];
            List<string> TempRolesList = new List<string>();
            List<string> Roles = new List<string>();

            #region If exists a cookie
            if (UserCookie != null && UserCookie["Name"] != null)
            {
                List<string> listRoles = new List<string>();
                string userEncrypted = Request.Cookies["userInfoThera"]["userName"];
                string passwordEncrypted = Request.Cookies["userInfoThera"]["password"];

                byte[] decrypted = Convert.FromBase64String(userEncrypted);
                string account = Encoding.Unicode.GetString(decrypted);

                decrypted = Convert.FromBase64String(passwordEncrypted);
                string password = Encoding.Unicode.GetString(decrypted);

                if (!StandardClass.SessionValid(account, password))
                {
                    HttpContext.Current.Session.Add("UserName", null);
                    HttpContext.Current.Session.Add("Account", null);
                    HttpContext.Current.Session.Add("Password", null);
                    HttpContext.Current.Session.Add("ListRoles", null);
                    HttpContext.Current.Session.Add("Identificated", false);
                    return;
                } 
                    HttpContext.Current.Session.Add("UserName", UserCookie["Name"].ToString());
                    HttpContext.Current.Session.Add("Account", account);
                    HttpContext.Current.Session.Add("Password", password);
                    HttpContext.Current.Session.Add("Identificated", true); 

                    var roles = Request.Cookies["userInfoThera"]["ListRoles"];
                    if (roles != null)
                    {
                        TempRolesList = roles.ToString().Split(',').ToList();
                        Roles.AddRange(TempRolesList);
                        HttpContext.Current.Session.Add("ListRoles", TempRolesList);
                        foreach (string role in TempRolesList)
                            HttpContext.Current.User.IsInRole(role);
                    }
            }
            else if (HttpContext.Current.Session["Account"] == null)
            {
                HttpContext.Current.Session.Add("UserName", null);
                HttpContext.Current.Session.Add("Account", null);
                HttpContext.Current.Session.Add("Password", null);
                HttpContext.Current.Session.Add("ListRoles", null);
                HttpContext.Current.Session.Add("Identificated", false);
            }
            #endregion
        }

    }
}
