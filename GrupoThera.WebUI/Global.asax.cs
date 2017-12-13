using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Core.Utils;
using GrupoThera.WebUI.App_Start;
using GrupoThera.WebUI.DependencyResolvers;
using Ninject;
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
        [Inject]
        public IRoleAccountService _accountManager { private get; set; }
        [Inject]
        public ICatalogService _catalogManager { private get; set; }

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
                string idEmpresa = Request.Cookies["userInfoThera"]["idEmpresa"];
                string idSucursal = Request.Cookies["userInfoThera"]["idSucursal"];

                byte[] decrypted = Convert.FromBase64String(userEncrypted);
                string account = Encoding.Unicode.GetString(decrypted);

                decrypted = Convert.FromBase64String(passwordEncrypted);
                string password = Encoding.Unicode.GetString(decrypted);

                if (_accountManager.getAccountUser(account, password) == null)
                {
                    HttpContext.Current.Session.Add("UserName", null);
                    HttpContext.Current.Session.Add("Account", null);
                    HttpContext.Current.Session.Add("Password", null);
                    HttpContext.Current.Session.Add("ListRoles", null);
                    HttpContext.Current.Session.Add("Empresa", null);
                    HttpContext.Current.Session.Add("Sucursal", null);
                    HttpContext.Current.Session.Add("EmpresaName", null);
                    HttpContext.Current.Session.Add("SucursalName", null);
                    return;
                } 
                    HttpContext.Current.Session.Add("UserName", UserCookie["Name"].ToString());
                    HttpContext.Current.Session.Add("Account", account);
                    HttpContext.Current.Session.Add("Password", password);
                    HttpContext.Current.Session.Add("Empresa", _catalogManager.getEmpresaById(Convert.ToInt16(idEmpresa)));
                    HttpContext.Current.Session.Add("Sucursal", _catalogManager.getSucursalById(Convert.ToInt16(idSucursal)));
 
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
                HttpContext.Current.Session.Add("EmpresaName", null);
                HttpContext.Current.Session.Add("SucursalName", null);
                HttpContext.Current.Session.Add("Empresa", null);
                HttpContext.Current.Session.Add("Sucursal", null);
            }
            #endregion
        }

    }
}
