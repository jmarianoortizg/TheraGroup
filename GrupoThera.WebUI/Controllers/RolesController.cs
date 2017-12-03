using GrupoThera.Entities.Models.General;
using GrupoThera.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrupoThera.Entities.Entity.General;
using GrupoThera.BusinessModel.Contracts.General;

namespace GrupoThera.WebUI.Controllers
{
    public class RolesController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IRoleAccountService _roleAccountService;

        #endregion Fields

        #region Constructor

        public RolesController(ICatalogService catalogService,
                                IRoleAccountService roleAccountService
                              )
        {
            _catalogService = catalogService;
            _roleAccountService = roleAccountService;
        }

        #endregion Constructor

        #region Methods

        [HttpPost]
        public JsonResult loginUser(UserLoginModel userLogin)
        {
            try
            {
                var userLoginAccount = _roleAccountService.getAccountUser(userLogin.UserName, userLogin.Password);

                if (userLoginAccount == null)
                    throw new Exception("User y/o Password son incorrectos");

                var empresaSucursalUs = _roleAccountService.getEmpresaSucursalUsuario(userLogin.selectedEmpresa,userLogin.selectedSucursal,userLoginAccount.usuarioId);

                if (empresaSucursalUs == null)
                    throw new Exception("El usuario no pertenece a esta empresa y/o sucursal");

                var userRoleAccount = _roleAccountService.getListRoleByUser(empresaSucursalUs.empresaSucursalUsuarioMapId);

                if (userRoleAccount.Count == 0)
                    throw new Exception("Usuario no tiene privilegios asignados");

                HttpContext.Session["Account"] = userLoginAccount.usuarioId;
                HttpContext.Session["UserName"] = userLoginAccount.nombre;
                HttpContext.Session["ListRoles"] = userRoleAccount.Select(x => x.name.ToString()).ToList();
                HttpContext.Session["Password"] = userLogin.Password;
                HttpContext.Session["Empresa"] = _catalogService.getEmpresaById(userLogin.selectedEmpresa);
                HttpContext.Session["Sucursal"] = _catalogService.getSucursalById(userLogin.selectedSucursal);

                if (userLogin.stayLogin)
                {
                    cookieData cookie = new cookieData();
                    cookie.Password   = userLogin.Password;
                    cookie.UserName   = userLogin.UserName.ToUpper();
                    cookie.idEmpresa  = userLogin.selectedEmpresa.ToString();
                    cookie.idSucursal = userLogin.selectedSucursal.ToString();
                    cookie.ListRoles  = userRoleAccount.Select(x => x.name.ToString()).ToList();
                    Response.Cookies.Add(StandardClass.CreateCookie(cookie));
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session["Account"] = null;
                HttpContext.Session["UserName"] = null;
                HttpContext.Session["ListRoles"] = null;
                HttpContext.Session["Password"] = null;
                HttpContext.Session["Empresa"] = null;
                HttpContext.Session["Sucursal"] = null;

                return Json(new
                {
                    message = ex.Message,
                    success = false
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new
            {
                message = "Login correcto, Cargando Perfil de usuario",
                success = true,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoginUnAuthorized(bool unAuthorized)
        {
            return View("~/Views/Shared/_Unauthorized.cshtml", unAuthorized);
        }

        #endregion Methods

    }
}