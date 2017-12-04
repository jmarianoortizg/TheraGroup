﻿using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Core.Logging;
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

        #region AreaServicio
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

        public ActionResult AreaServicioEdit(int idAreaServicioSelected)
        {
            var allAreaServicio = _catalogService.getAreaServicios();
            var model = new CatalogModel()
            {
                AreaServicio = _catalogService.getAreaServicioById(idAreaServicioSelected),
                AllAreaServicio = allAreaServicio,
                listAreaServicio = DropListHelper.GetAreaServicios(allAreaServicio),
                listClasificacionServicio = DropListHelper.GetClasificacionServicio(_catalogService.getClasificacionServicio())

            };
            return View(model);
        }

        public ActionResult PEEditAreaServicio(CatalogModel CatalogModel)
        {
            try
            {  
                _catalogService.EditAreaServicio(CatalogModel.AreaServicio);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit AreaServicio : ID: {0}, New Name: {1} by {2}", CatalogModel.AreaServicio.areaServicioId, CatalogModel.AreaServicio.descripcion, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("AreaServicio");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("AreaServicio");
        }

        public ActionResult DeleteAreaServicio(int idAreaServicioSelected)
        {
            try
            {
                _catalogService.DeleteAreaServicio(idAreaServicioSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete AreaServicio : ID: {0} by {1}", idAreaServicioSelected, HttpContext.Session["UserName"].ToString()));
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    responseText = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion AreaServicio

        #region Empresa
        [CustomAuthorizeAttribute(privilege = "Empresa,GeneralCatalog")]

        public ActionResult Empresa()
        {
            var allEmpresa = _catalogService.getEmpresas();
            var model = new CatalogModel()
            {
                AllEmpresa = allEmpresa,
                listEmpresa = DropListHelper.GetEmpresas(allEmpresa)

            };
            return View(model);
        }

        public ActionResult CreateEmpresa(CatalogModel CatalogModel)
        {
            try
            { 
                _catalogService.AddEmpresa(CatalogModel.Empresa);
                return RedirectToAction("Empresa");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult EmpresaEdit(int idEmpresaSelected)
        {
            var allEmpresa = _catalogService.getEmpresas();
            var model = new CatalogModel()
            {
                Empresa = _catalogService.getEmpresaById(idEmpresaSelected),
                AllEmpresa = allEmpresa,
                listEmpresa = DropListHelper.GetEmpresas(allEmpresa) 

            };
            return View(model);
        }

        public ActionResult PEEditEmpresa(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditEmpresa(CatalogModel.Empresa);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit Empresa : ID: {0}, New Name: {1} by {2}", CatalogModel.Empresa.empresaId, CatalogModel.Empresa.nombre, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("Empresa");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("Empresa");
        }

        public ActionResult DeleteEmpresa(int idEmpresaSelected)
        {
            try
            {
                _catalogService.DeleteEmpresa(idEmpresaSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete Empresa : ID: {0} by {1}", idEmpresaSelected, HttpContext.Session["UserName"].ToString()));
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    responseText = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Empresa

        #region Cliente
        [CustomAuthorizeAttribute(privilege = "Cliente,GeneralCatalog")]

        public ActionResult Cliente()
        {
            var allCliente = _catalogService.getClientes();
            var model = new CatalogModel()
            {
                AllCliente = allCliente,
                listCliente = DropListHelper.GetCliente(allCliente),
                listEstado = DropListHelper.GetEstado(_catalogService.getEstado()),
                listCiudad = DropListHelper.GetCiudad(_catalogService.getCiudad()),
                listGiro = DropListHelper.GetGiro(_catalogService.getGiro())
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateCliente(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.AddCliente(CatalogModel.Cliente);
                return RedirectToAction("Cliente");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult ClienteEdit(int idClienteSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.Cliente = _catalogService.getClienteById(idClienteSelected);
            return View(model);
        }

        public ActionResult PEEditCliente(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditCliente(CatalogModel.Cliente);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit Cliente : ID: {0}, New Name: {1} by {2}", CatalogModel.Cliente.clienteId, CatalogModel.Cliente.razonSocial, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("Cliente");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("Cliente");
        }

        public ActionResult DeleteCliente(int idClienteSelected)
        {
            try
            {
                _catalogService.DeleteCliente(idClienteSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete Cliente : ID: {0} by {1}", idClienteSelected, HttpContext.Session["UserName"].ToString()));
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    responseText = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion empresa

        

        #endregion Methods
    }
}