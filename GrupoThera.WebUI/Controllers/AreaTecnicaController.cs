using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OS;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.OTPre;
using GrupoThera.Entities.Models.AreaTecnica;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class AreaTecnicaController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IOTPreliminarService _otPreliminarService;
        private IOServicioService _oServicioService;


        #endregion Fields

        #region Constructor

        public AreaTecnicaController(ICatalogService catalogService, IOTPreliminarService otPreliminarService, IOServicioService oServicioService)
        {
            _catalogService = catalogService;
            _otPreliminarService = otPreliminarService;
            _oServicioService = oServicioService;
        }

        #endregion Constructor

        #region Methods

        #region OTPreliminar

        [CustomAuthorizeAttribute(privilege = "ATPreliminar,GeneralAreaTecnica")]
        public ActionResult ATOTPreliminar()
        {
            var model = generateInitialModel();
            TempData["ATModel"] = model;
            return View(model);
        }

        public ATModel generateInitialModel()
        {
            var allOTPreliminares = _otPreliminarService.getAllOTPreliminar().Where(t => t.StatusOTPreliminar.codigo.Equals("ENVAT")).ToList();
            var actual = allOTPreliminares.Where(t => t.fechaCreacion.Date == DateTime.Now.Date).ToList();
            var model = new ATModel()
            {
                listOTPreliminares = allOTPreliminares,
                abiertas = actual,
                pendientes = allOTPreliminares.Where(t => t.fechaCreacion.Date < DateTime.Now.Date).ToList(),
                OTPreliminaresActual = actual
            };
            return model;
        }

        public ActionResult showOTPreliminarCounterView(string statusCounter)
        {
            var model = generateInitialModel();
            TempData.Keep("ATModel");

            if (statusCounter.Equals("TODAY"))
                model.OTPreliminaresActual = model.abiertas;
            else if (statusCounter.Equals("PENDING"))
                model.OTPreliminaresActual = model.pendientes;

            return Json(new
            {
                success = true,
                OTPreliminarHtml = StdClassWeb.RenderToString(PartialView("ATOTPreliminarItem", model), HttpContext)
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchOTPreliminarView(int idOTPreliminarSelected)
        {
            var otpreliminarItem = _otPreliminarService.getOTPreliminarById(idOTPreliminarSelected);
            otpreliminarItem.OTPrePartidas = _otPreliminarService.getAllPrePartidasByOTPreliminar(otpreliminarItem.otPreliminarId);
            var model = generateNoteModel(otpreliminarItem.noteId);
            model.otpreliminar = otpreliminarItem;
            return Json(new
            {
                responseOTPreliminarView = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOTPItemView.cshtml", otpreliminarItem), HttpContext),
                responseOTPNotas = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOTPNotes.cshtml", model), HttpContext),
                responseOTPNewNotas = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOTPNewNotes.cshtml", model), HttpContext),
                responseOTPActions = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOTPActions.cshtml", model), HttpContext),
                responseOTPIngresoAT = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOTPEntryAT.cshtml", otpreliminarItem), HttpContext)
            }, JsonRequestBehavior.AllowGet);
        }

        public ATModel generateNoteModel(long noteID)
        {
            var model = new ATModel()
            {
                listNotes = _catalogService.getNotesByDocument(noteID),
                note = new Note()
            };
            return model;
        }

        public ActionResult newNoteOTPreliminar(ATModel modelSearch)
        {
            modelSearch.note.creation = DateTime.Now;
            modelSearch.note.document = "ENVAT";
            modelSearch.note.owner = (string)HttpContext.Session["UserName"];
            modelSearch.note.noteDocId = modelSearch.otpreliminar.noteId;

            _catalogService.AddNote(modelSearch.note);
            var model = generateNoteModel(modelSearch.otpreliminar.noteId);
            return PartialView("~/Views/AreaTecnica/ATOTPNotes.cshtml", model);
        }

        public ActionResult changeOTPreliminarStatus(string statusOTPreliminar, long idOTPreliminar, string message)
        {
            try
            {
                TempData.Keep("ATModel");
                var preliminarOTItem = _otPreliminarService.getOTPreliminarById(idOTPreliminar);
                var messageSuccess = "";
                var messageHeader = false;
                if (statusOTPreliminar.Equals("RECHAZADAAT"))
                {
                    var status = _catalogService.getStatusOTPreliminarStatus(statusOTPreliminar);
                    preliminarOTItem.StatusOTPreliminar = status;
                    preliminarOTItem.statusOTPreliminarId = status.statusOTPreliminarId;
                    var result = _otPreliminarService.edicionOTPreliminar(preliminarOTItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Cambio de estado correctemente";
                }
                if (statusOTPreliminar.Equals("ACEPAT"))
                {
                    preliminarOTItem.OTPrePartidas = _otPreliminarService.getAllPrePartidasByOTPreliminar(preliminarOTItem.otPreliminarId);
                    var result = _oServicioService.createOS(preliminarOTItem);
                    if (result.Contains("Error"))
                    {
                        throw new Exception(result);
                    }
                    messageSuccess = "Nueva Orden de trabajo Creada #" + result;
                    messageHeader = true;
                    _otPreliminarService.disableParents(preliminarOTItem);
                }

                if (!message.Equals(""))
                {
                    var note = new Note();
                    note.creation = DateTime.Now;
                    note.document = statusOTPreliminar;
                    note.owner = (string)HttpContext.Session["UserName"];
                    note.noteDocId = preliminarOTItem.noteId;
                    note.content = message;
                    _catalogService.AddNote(note);
                }

                return Json(new
                {
                    success = true,
                    messageItems = "<h3 class='block-title text-primary'><strong>[</strong> Recargar Resultados <strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageView = "<h3 class='block-title text-primary'><strong>VISTA GENERAL[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageNotas = "<h3 class='block-title text-primary'><strong>NOTAS[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageNewNotas = "<h3 class='block-title text-primary'><strong>NUEVA NOTA[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageActions = "<h3 class='block-title text-primary'><strong>ACCIONES[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageIngresoAT = "<h3 class='block-title text-primary'><strong>DATOS INGRESO AREA TECNICA[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageSuccess = messageSuccess,
                    messageHeader = messageHeader
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    responseHtml = StdClassWeb.RenderToString(PartialView("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(new Exception(ex.Message), "CotizationController", "changePreliminarStatus")), HttpContext)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult updateOTPreliminar(OTPreliminar OTPreliminar)
        {
            var itemUpdate = _otPreliminarService.getOTPreliminarById(OTPreliminar.otPreliminarId);
            itemUpdate.check1 = OTPreliminar.check1;
            itemUpdate.check2 = OTPreliminar.check2;
            itemUpdate.check3 = OTPreliminar.check3;
            itemUpdate.check4 = OTPreliminar.check4;
            itemUpdate.fechaEntrega = OTPreliminar.fechaEntrega;
            itemUpdate.fechaMuestreo = OTPreliminar.fechaMuestreo;
            itemUpdate.comentarios = OTPreliminar.comentarios;
            itemUpdate.direccionServicio = OTPreliminar.direccionServicio;

            var result = _otPreliminarService.edicionOTPreliminar(itemUpdate);
            if (result.Equals("OK"))
            {
                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                success = false,
                responseHtml = StdClassWeb.RenderToString(PartialView("~/Views/Shared/ErrorFocus.cshtml", new HandleErrorInfo(new Exception(result), "CatalogController", "CreateClient")), HttpContext)
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion OTPreliminar

        #region OrdenServicio

        [CustomAuthorizeAttribute(privilege = "ATServicio,GeneralAreaTecnica")]
        public ActionResult ATOServicio()
        {
            var model = generateInitialModelOS();
            TempData["ATOSModel"] = model;
            return View(model);
        }

        public ATOSModel generateInitialModelOS()
        {
            var allOServicio = _oServicioService.getAllOServicio().ToList();
            var actual = allOServicio.Where(t => t.fechaCreacion.Date == DateTime.Now.Date).ToList();
            var model = new ATOSModel()
            {
                listOrdenServicio = allOServicio,
                today = actual,
                pendientes = allOServicio.Where(t => t.fechaCreacion.Date < DateTime.Now.Date).ToList(),
                abiertas = allOServicio.Where(t => t.StatusOrdenServicio.codigo.Equals("ABIERTA")).ToList(),
                cancelada = allOServicio.Where(t => t.StatusOrdenServicio.codigo.Equals("CANCEL")).ToList(),
                cerrada = allOServicio.Where(t => t.StatusOrdenServicio.codigo.Equals("CLOSE")).ToList(),
                proceso = allOServicio.Where(t => t.StatusOrdenServicio.codigo.Equals("PROCESS")).ToList(),
                parcial = allOServicio.Where(t => t.StatusOrdenServicio.codigo.Equals("PARTIAL")).ToList(),
                programada = allOServicio.Where(t => t.StatusOrdenServicio.codigo.Equals("PROGRAM")).ToList(),
                ordenServicioActual = actual
            };
            return model;
        }

        public ActionResult showOServicioCounterView(string statusCounter)
        {
            var model = generateInitialModelOS();
            TempData.Keep("ATOSModel");

            if (statusCounter.Equals("TODAY"))
                model.ordenServicioActual = model.today;
            else if (statusCounter.Equals("PENDING"))
                model.ordenServicioActual = model.pendientes;
            else if (statusCounter.Equals("ABIERTA"))
                model.ordenServicioActual = model.abiertas;
            else if (statusCounter.Equals("CANCEL"))
                model.ordenServicioActual = model.cancelada;
            else if (statusCounter.Equals("CLOSE"))
                model.ordenServicioActual = model.cerrada;
            else if (statusCounter.Equals("PROCESS"))
                model.ordenServicioActual = model.proceso;
            else if (statusCounter.Equals("PARTIAL"))
                model.ordenServicioActual = model.parcial;
            else if (statusCounter.Equals("PROGRAM"))
                model.ordenServicioActual = model.programada;

            return Json(new
            {
                success = true,
                OServicioHtml = StdClassWeb.RenderToString(PartialView("ATOServicioItem", model), HttpContext)
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchOServicioView(int idOServicioSelected)
        {
            var oServicioItem = _oServicioService.getOServicioById(idOServicioSelected);
            oServicioItem.OSPrePartidas = _oServicioService.getAllPrePartidasByPreliminar(oServicioItem.ordenServicioId);
            var model = generateNoteModelOS(oServicioItem.noteId);
            model.ordenServicio = oServicioItem;

            var modelOS = new ATOSModel()
            {
                listStatusOrdenPartidasInitial = DropListHelper.GetStatusOrdenPartidasAreaTecnicaInicial(_catalogService.getStatusOrdenPartidas()),
                listStatusOrdenPartidasProcess = DropListHelper.GetStatusOrdenPartidasAreaTecnicaProcess(_catalogService.getStatusOrdenPartidas()),
                ordenServicio = oServicioItem
            };

            return Json(new
            {
                responseOServicioView = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOServicioItemView.cshtml", modelOS), HttpContext),
                responseOSNotas = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOSNewNotes.cshtml", model), HttpContext),
                responseOSNewNotas = StdClassWeb.RenderToString(PartialView("~/Views/AreaTecnica/ATOSNotes.cshtml", model), HttpContext),
            }, JsonRequestBehavior.AllowGet);
        }

        public ATOSModel generateNoteModelOS(long noteID)
        {
            var model = new ATOSModel()
            {
                listNotes = _catalogService.getNotesByDocument(noteID),
                note = new Note()
            };
            return model;
        }

        public ActionResult newNoteOServicio(ATOSModel modelSearch)
        {
            modelSearch.note.creation = DateTime.Now;
            modelSearch.note.document = "OSAT";
            modelSearch.note.owner = (string)HttpContext.Session["UserName"];
            modelSearch.note.noteDocId = modelSearch.ordenServicio.noteId;

            _catalogService.AddNote(modelSearch.note);
            var model = generateNoteModelOS(modelSearch.ordenServicio.noteId);
            return PartialView("~/Views/AreaTecnica/ATOSNotes.cshtml", model);
        }

        public ActionResult changeStatusOrdenPartidas(long idOrdenServicioPartida, long idStatusOrdenPartida)
        {

            var result = _oServicioService.updateOSPartida(idOrdenServicioPartida, idStatusOrdenPartida);

            return Json(new
            {
                responseResult = result
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion OrdenServicio

        [CustomAuthorizeAttribute(privilege = "ATReporte,GeneralAreaTecnica")]
        public ActionResult ATReporte()
        {
            return View();
        }

        #endregion Methods
    }
}