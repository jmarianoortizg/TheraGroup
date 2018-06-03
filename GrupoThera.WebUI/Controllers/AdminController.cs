using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Models.Admin;
using GrupoThera.Entities.Models.Catalog;
using GrupoThera.WebUI.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class AdminController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IRoleAccountService _roleAccountService;
        private int idEmpresa = 1;

        #endregion Fields

        #region Constructor

        public AdminController( ICatalogService catalogService,
                                IRoleAccountService roleAccountService
                              )
        {
            _catalogService = catalogService;
            _roleAccountService = roleAccountService;
        }


        #endregion Fields

        #region Methods

        #region NewUser
        [CustomAuthorizeAttribute(privilege = "A001NEUSER")]
        public ActionResult NewEditUser()
        {
            var allUsuarios = _roleAccountService.getUsers();
            var model = new AdminModel()
            {
                AllUsuarios = allUsuarios,
                listDepartmentos = DropListHelper.GetDepartamentos(_roleAccountService.getDepartments())
            };
            return View(model);
        }

        public ActionResult CreateUsuario(AdminModel AdminModel)
        {
            try
            {
                try
                {
                    if (_roleAccountService.existAccountUser(AdminModel.Usuario.usuario))
                        throw new Exception("El Usuario ya existe con los datos ingresados");
                }
                catch (Exception ex)
                {
                    return generateErrorFocus(ex);
                }
                _roleAccountService.addUser(AdminModel.Usuario);
                return RedirectToAction("NewEditUser");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult UsuarioEdit(int idUsuarioSelected)
        {
            var allUsuarios = _roleAccountService.getUsers();
            var model = new AdminModel()
            {
                AllUsuarios = allUsuarios,
                Usuario = _roleAccountService.getUserbyId(idUsuarioSelected),
                listDepartmentos = DropListHelper.GetDepartamentos(_roleAccountService.getDepartments())
            };
            return View(model);
        }

        public ActionResult PEEditUsuario(AdminModel AdminModel)
        {
            try
            {
                _roleAccountService.updateUser(AdminModel.Usuario);
                return RedirectToAction("NewEditUser");
            }
            catch (Exception ex)
            {
                return generateErrorFocus(ex);
            }
        }

        #endregion NewUser

        #region AssignRoles

        [CustomAuthorizeAttribute(privilege = "A002ROLASS")]
        public ActionResult AssignRoles()
        {
            var allUsuarios = _roleAccountService.getUsers();
            var model = new AdminModel()
            {
                AllUsuarios = allUsuarios,
                listEmpresas = DropListHelper.GetEmpresas(_catalogService.getEmpresas()),
                listSucursal = DropListHelper.GetSucursales(_catalogService.getSucursalesbyEmpresa(idEmpresa)),
                listSucAssignedByUser = DropListHelper.GetUsuario(_roleAccountService.GetEmpresaSucursalUsuarioMap(_catalogService.getEmpresas().FirstOrDefault(),_catalogService.getSucursalesbyEmpresa(1).FirstOrDefault()))
            };
            return View(model);
        }

        public JsonResult getUserByEmpresaSucursal(int idSucursal)
        {
            var listUsuarios = DropListHelper.GetUsuario(_roleAccountService.GetEmpresaSucursalUsuarioMap(_catalogService.getEmpresas().FirstOrDefault(), _catalogService.getSucursalById(idSucursal)));
            return Json(listUsuarios,JsonRequestBehavior.AllowGet);
        }

        public ActionResult getRolesbyUser(AdminModel AdminModel)
        {
            var empSucUsuMap = _roleAccountService.getEmpresaSucursalbyUsuario(idEmpresa, AdminModel.selectedSucursal, AdminModel.selectedUsuario);
            var rolAss = _roleAccountService.getListRoleByUserSucursal(empSucUsuMap.empresaSucursalUsuarioMapId);
            var model = new AdminModel()
            {
                rolesAssigned = rolAss.OrderBy(t => t.code).ToList(),
                rolesMissing = _roleAccountService.getMissingRoles(rolAss).OrderBy(t=>t.code).ToList(),
                Empresa = _catalogService.getEmpresaById(idEmpresa),
                Sucursal = _catalogService.getSucursalById(AdminModel.selectedSucursal),
                Usuario = _roleAccountService.getUserbyId(AdminModel.selectedUsuario),
                empSucUsuMap = empSucUsuMap
            };
            return PartialView("_ASROByUser",model);
        }

        public ActionResult AddRoleEmpSucUsuMap(int idEmpSucMap,int idRol)
        {
            _roleAccountService.addEmpSucUsuRolMap(idEmpSucMap, idRol);
            return null;
        }

        public ActionResult DeleteRoleEmpSucUsuMap(int idEmpSucMap, int idRol)
        {
            _roleAccountService.DeleteRoleEmpSucUsuRolMap(idEmpSucMap, idRol);
            return null;
        }

        #endregion AssignRoles

        #region AssignSucursales

        [CustomAuthorizeAttribute(privilege = "A003SUCASS")]
        public ActionResult AssignSucursales()
        {
            var model = new AdminModel()
            {
                listEmpresas = DropListHelper.GetEmpresas(_catalogService.getEmpresas()),
                listSucursal = DropListHelper.GetSucursales(_catalogService.getSucursalesbyEmpresa(idEmpresa)),
            };
            return View(model);
        }

        public ActionResult getUsersbySucursal(AdminModel AdminModel)
        {
            var empSucUsuMap = _roleAccountService.getEmpresaSucursal(idEmpresa, AdminModel.selectedSucursal);
            var userAss = _roleAccountService.getListUsersBySucursal(empSucUsuMap.empresaSucursalId);
            var model = new AdminModel()
            {
                usuarioAssigned = userAss.OrderBy(t => t.apellidos).ToList(),
                usuarioMissing = _roleAccountService.getMissingUsers(userAss).OrderBy(t => t.apellidos).ToList(),
                Empresa = _catalogService.getEmpresaById(idEmpresa),
                Sucursal = _catalogService.getSucursalById(AdminModel.selectedSucursal),
                empSucMap = empSucUsuMap
            };
            return PartialView("_ASUSBySucursal", model);
        }

        public ActionResult AddEmpSucUsuMap(int idEmpSucMap, int idUsuario)
        {
            _roleAccountService.addEmpSucUsuMap(idEmpSucMap, idUsuario);
            return null;
        }

        public ActionResult DeleteEmpSucUsuMap(int idEmpSucMap, int idUsuario)
        {
            _roleAccountService.DeleteRoleEmpSucUsuMap(idEmpSucMap, idUsuario);
            return null;
        }



        #endregion AssignSucursales

        #region General

        private JsonResult generateErrorFocus(Exception ex)
        {
            return Json(new
            {
                success = false,
                htmlError = StdClassWeb.RenderToString(PartialView("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(ex, "ProcessController", "Function")), HttpContext)
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion General

        #endregion Methods
    }

}