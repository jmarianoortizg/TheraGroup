using GrupoThera.BusinessModel.Contracts.General;
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
                listGiro = DropListHelper.GetGiro(_catalogService.getGiros())
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

        #region Giro
        [CustomAuthorizeAttribute(privilege = "Giro,GeneralCatalog")]

        public ActionResult Giro()
        {
            var allGiro = _catalogService.getGiros();
            var model = new CatalogModel()
            {
                AllGiro = allGiro,
                listGiro = DropListHelper.GetGiro(allGiro)
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateGiro(CatalogModel CatalogModel)
        {
            try
            {
                try
                {
                    if (_catalogService.ExistGiro(CatalogModel.Giro.descripcion))
                        throw new Exception("El concepto Giro con los datos ingresados ya existe ( Descripcion)");
                }
                catch (Exception ex)
                {
                    return View("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(ex, "CatalogController", "ExistClient"));
                }
                _catalogService.AddGiro(CatalogModel.Giro);
                return RedirectToAction("Giro");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult GiroEdit(int idGiroSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.Giro = _catalogService.getGiroById(idGiroSelected);
            return View(model);
        }

        public ActionResult PEEditGiro(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditGiro(CatalogModel.Giro);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit Giro : ID: {0}, New Name: {1} by {2}", CatalogModel.Giro.giroId, CatalogModel.Giro.descripcion, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("Giro");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("Giro");
        }

        public ActionResult DeleteGiro(int idGiroSelected)
        {
            try
            {
                _catalogService.DeleteGiro(idGiroSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete Giro : ID: {0} by {1}", idGiroSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion giro

        #region Provedor
        [CustomAuthorizeAttribute(privilege = "Provedor,GeneralCatalog")]

        public ActionResult Provedor()
        {
            var allProvedor = _catalogService.getProvedores();
            var model = new CatalogModel()
            {
                AllProvedor = allProvedor,
                listProvedor = DropListHelper.GetProvedor(allProvedor)
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateProvedor(CatalogModel CatalogModel)
        {
            try
            {
                try
                {
                    if (_catalogService.existProvedor(CatalogModel.Provedor.nombre, CatalogModel.Provedor.razonSocial, CatalogModel.Provedor.rfc))
                        throw new Exception("El Provedor con los datos ingresados ya existe ( Nombre, Razon Social, RFC)");
                }
                catch (Exception ex)
                {
                    return View("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(ex, "CatalogController", "ExistClient"));
                }
                _catalogService.AddProvedor(CatalogModel.Provedor);
                return RedirectToAction("Provedor");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult ProvedorEdit(int idProvedorSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.Provedor = _catalogService.getProvedorById(idProvedorSelected);
            return View(model);
        }

        public ActionResult PEEditProvedor(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditProvedor(CatalogModel.Provedor);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit Provedor : ID: {0}, New Name: {1} by {2}", CatalogModel.Provedor.provedorId, CatalogModel.Provedor.razonSocial, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("Provedor");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("Provedor");
        }

        public ActionResult DeleteProvedor(int idProvedorSelected)
        {
            try
            {
                _catalogService.DeleteProvedor(idProvedorSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete Provedor : ID: {0} by {1}", idProvedorSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion Provedor

        #region Clasificacion
        [CustomAuthorizeAttribute(privilege = "ClasificacionServicio,GeneralCatalog")]

        public ActionResult ClasificacionServicio()
        {
            var allClasificacionServicio = _catalogService.getClasificacionServicios();
            var model = new CatalogModel()
            {
                AllClasificacionServicio = allClasificacionServicio,
                listClasificacionServicio = DropListHelper.GetClasificacionServicio(allClasificacionServicio),
                listMetodoCotizacion = DropListHelper.GetMetodoCotizacion(_catalogService.getMetodoCotizaciones())
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateClasificacionServicio(CatalogModel CatalogModel)
        {
            try
            {   
                _catalogService.AddClasificacionServicio(CatalogModel.ClasificacionServicio);
                return RedirectToAction("ClasificacionServicio");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult ClasificacionServicioEdit(int idClasificacionServicioSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.ClasificacionServicio = _catalogService.getClasificacionServicioById(idClasificacionServicioSelected);
            return View(model);
        }

        public ActionResult PEEditClasificacionServicio(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditClasificacionServicio(CatalogModel.ClasificacionServicio);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit ClasificacionServicio : ID: {0}, New Name: {1} by {2}", CatalogModel.ClasificacionServicio.clasificacionServicioId, CatalogModel.ClasificacionServicio.descripcion, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("ClasificacionServicio");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("ClasificacionServicio");
        }

        public ActionResult DeleteClasificacionServicio(int idClasificacionServicioSelected)
        {
            try
            {
                _catalogService.DeleteClasificacionServicio(idClasificacionServicioSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete ClasificacionServicio : ID: {0} by {1}", idClasificacionServicioSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion Clasificacion

        #region Frecuencia
        [CustomAuthorizeAttribute(privilege = "FrecuenciaServicio,GeneralCatalog")]

        public ActionResult FrecuenciaServicio()
        {
            var allFrecuenciaServicio = _catalogService.getFrecuenciaServicios();
            var model = new CatalogModel()
            {
                AllFrecuenciaServicio = allFrecuenciaServicio,
                listFrecuenciaServicio = DropListHelper.GetFrecuenciaServicio(allFrecuenciaServicio)
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateFrecuenciaServicio(CatalogModel CatalogModel)
        {
            try
            {   
                _catalogService.AddFrecuenciaServicio(CatalogModel.FrecuenciaServicio);
                return RedirectToAction("FrecuenciaServicio");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult FrecuenciaServicioEdit(int idFrecuenciaServicioSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.FrecuenciaServicio = _catalogService.getFrecuenciaServicioById(idFrecuenciaServicioSelected);
            return View(model);
        }

        public ActionResult PEEditFrecuenciaServicio(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditFrecuenciaServicio(CatalogModel.FrecuenciaServicio);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit FrecuenciaServicio : ID: {0}, New Name: {1} by {2}", CatalogModel.FrecuenciaServicio.frecuenciaServicioId, CatalogModel.FrecuenciaServicio.frecuencia, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("FrecuenciaServicio");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("FrecuenciaServicio");
        }

        public ActionResult DeleteFrecuenciaServicio(int idFrecuenciaServicioSelected)
        {
            try
            {
                _catalogService.DeleteFrecuenciaServicio(idFrecuenciaServicioSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete FrecuenciaServicio : ID: {0} by {1}", idFrecuenciaServicioSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion Frecuencia

        #region Moneda
        [CustomAuthorizeAttribute(privilege = "Moneda,GeneralCatalog")]

        public ActionResult Moneda()
        {
            var allMoneda = _catalogService.getMonedas();
            var model = new CatalogModel()
            {
                AllMoneda = allMoneda,
                listMoneda = DropListHelper.GetMoneda(allMoneda)
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult MonedaEdit(int idMonedaSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.Moneda = _catalogService.getMonedaById(idMonedaSelected);
            return View(model);
        }

        public ActionResult PEEditMoneda(CatalogModel CatalogModel)
        {
            try
            {
                CatalogModel.Moneda.defaultt = "N";
                _catalogService.EditMoneda(CatalogModel.Moneda);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit Moneda : ID: {0}, New Name: {1} by {2}", CatalogModel.Moneda.monedaId, CatalogModel.Moneda.moneda, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("Moneda");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("Moneda");
        }

        public ActionResult DeleteMoneda(int idMonedaSelected)
        {
            try
            {
                _catalogService.DeleteMoneda(idMonedaSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete Moneda : ID: {0} by {1}", idMonedaSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion Moneda

        #region TiempoEntrega
        [CustomAuthorizeAttribute(privilege = "TiempoEntrega,GeneralCatalog")]

        public ActionResult TiempoEntrega()
        {
            var allTiempoEntrega = _catalogService.getTiempoEntregas();
            var model = new CatalogModel()
            {
                AllTiempoEntrega = allTiempoEntrega,
                listTiempoEntrega = DropListHelper.GetTiempoEntrega(allTiempoEntrega)
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateTiempoEntrega(CatalogModel CatalogModel)
        {
            try
            {   
                _catalogService.AddTiempoEntrega(CatalogModel.TiempoEntrega);
                return RedirectToAction("TiempoEntrega");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult TiempoEntregaEdit(int idTiempoEntregaSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.TiempoEntrega = _catalogService.getTiempoEntregaById(idTiempoEntregaSelected);
            return View(model);
        }

        public ActionResult PEEditTiempoEntrega(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditTiempoEntrega(CatalogModel.TiempoEntrega);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit TiempoEntrega : ID: {0}, New Name: {1} by {2}", CatalogModel.TiempoEntrega.tiempoEntregaId, CatalogModel.TiempoEntrega.tiempoEntrega, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("TiempoEntrega");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("TiempoEntrega");
        }

        public ActionResult DeleteTiempoEntrega(int idTiempoEntregaSelected)
        {
            try
            {
                _catalogService.DeleteTiempoEntrega(idTiempoEntregaSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete TiempoEntrega : ID: {0} by {1}", idTiempoEntregaSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion TiempoEntrega

        #region Configuracion
        [CustomAuthorizeAttribute(privilege = "Configuracion,GeneralCatalog")]

        public ActionResult Configuracion()
        {
            var allConfiguracion = _catalogService.getConfiguraciones();
            var model = new CatalogModel()
            {
                AllConfiguracion = allConfiguracion,
                listConfiguracion = DropListHelper.GetConfiguracion(allConfiguracion)
            };
            TempData["CatalogModel"] = model;
            return View(model);
        }

        public ActionResult CreateConfiguracion(CatalogModel CatalogModel)
        {
            try
            {   
                _catalogService.AddConfiguracion(CatalogModel.Configuracion);
                return RedirectToAction("Configuracion");
            }
            catch (Exception ex)
            {
                return View("~/Views/Shared/ErrorGeneral.cshtml", new HandleErrorInfo(ex, "CatalogController", "CreateClient"));
            }
        }

        public ActionResult ConfiguracionEdit(int idConfiguracionSelected)
        {
            var model = (CatalogModel)TempData["CatalogModel"];
            TempData.Keep("CatalogModel");
            model.Configuracion = _catalogService.getConfiguracionById(idConfiguracionSelected);
            return View(model);
        }

        public ActionResult PEEditConfiguracion(CatalogModel CatalogModel)
        {
            try
            {
                _catalogService.EditConfiguracion(CatalogModel.Configuracion);
                Logger.WriteLog(LogLevel.INFO, string.Format("Edit Configuracion : ID: {0}, New Name: {1} by {2}", CatalogModel.Configuracion.configuracionId, CatalogModel.Configuracion.descripcion, HttpContext.Session["UserName"].ToString()));
                return RedirectToAction("Configuracion");
            }
            catch (Exception ex)
            {
                View("~/Views/Shared/ErrorBoxForm.cshtml", ex.Message);
            }
            return RedirectToAction("Configuracion");
        }

        public ActionResult DeleteConfiguracion(int idConfiguracionSelected)
        {
            try
            {
                _catalogService.DeleteConfiguracion(idConfiguracionSelected);
                Logger.WriteLog(LogLevel.INFO, string.Format("Delete Configuracion : ID: {0} by {1}", idConfiguracionSelected, HttpContext.Session["UserName"].ToString()));
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

        #endregion Configuracion

        #endregion Methods
    }
}