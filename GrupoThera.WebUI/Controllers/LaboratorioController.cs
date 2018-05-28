using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Models.Laboratorio;
using GrupoThera.Entities.Models.OTPre;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class LaboratorioController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IOTPreliminarService _otPreliminarService;

        #endregion Fields

        #region Constructor

        public LaboratorioController(ICatalogService catalogService, IOTPreliminarService otPreliminarService)
        {
            _catalogService = catalogService;
            _otPreliminarService = otPreliminarService;
        }

        #endregion Constructor

        #region Methods

        [CustomAuthorizeAttribute(privilege = "LBLaboratorio,GeneralLaboratorio")]

        public ActionResult LabOTPreliminares()
        {
            var model = generateInitialModel();
            TempData["LaboratorioModel"] = model;
            return View(model);
        }

        public LaboratorioModel generateInitialModel()
        {
            var allOTPreliminares = _otPreliminarService.getAllOTPreliminar().Where(t=> t.StatusOTPreliminar.codigo.Equals("ENVLAB")).ToList();
            var actual = allOTPreliminares.Where(t => t.fechaCreacion.Date == DateTime.Now.Date).ToList();
            var model = new LaboratorioModel()
            {
                listOTPreliminares = allOTPreliminares,
                abiertas = actual,
                pendientes = allOTPreliminares.Where(t => t.fechaCreacion.Date < DateTime.Now.Date).ToList(),
                OTPreliminaresActual = actual
            };
            return model;
        }

        public ActionResult showOTPreliminarCounterView(string statusCounter) {
            var model = generateInitialModel();
            TempData.Keep("LaboratorioModel");

            if (statusCounter.Equals("TODAY"))
                model.OTPreliminaresActual = model.abiertas;
            else if (statusCounter.Equals("PENDING"))
                model.OTPreliminaresActual = model.pendientes;

            return Json(new
            {
                success = true,
                OTPreliminarHtml = StdClassWeb.RenderToString(PartialView("LabOTPreliminarItem", model), HttpContext)
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
                responseOTPreliminarView = StdClassWeb.RenderToString(PartialView("~/Views/Laboratorio/LabOTPItemView.cshtml", otpreliminarItem), HttpContext),
                responseOTPNotas = StdClassWeb.RenderToString(PartialView("~/Views/Laboratorio/LabOTPNotes.cshtml", model), HttpContext),
                responseOTPNewNotas = StdClassWeb.RenderToString(PartialView("~/Views/Laboratorio/LabOTPNewNotes.cshtml", model), HttpContext),
                responseOTPActions = StdClassWeb.RenderToString(PartialView("~/Views/Laboratorio/LabOTPActions.cshtml", model), HttpContext)
            }, JsonRequestBehavior.AllowGet);
        }

        public LaboratorioModel generateNoteModel(long noteID)
        {
            var model = new LaboratorioModel()
            {
                listNotes = _catalogService.getNotesByDocument(noteID),
                note = new Note()
            };
            return model;
        }

        public ActionResult newNoteOTPreliminar(LaboratorioModel modelSearch)
        {
            modelSearch.note.creation = DateTime.Now;
            modelSearch.note.document = "LAB";
            modelSearch.note.owner = (string)HttpContext.Session["UserName"];
            modelSearch.note.noteDocId = modelSearch.otpreliminar.noteId;

            _catalogService.AddNote(modelSearch.note);
            var model = generateNoteModel(modelSearch.otpreliminar.noteId);
            return PartialView("~/Views/Laboratorio/LabOTPNotes.cshtml", model);
        }

        public ActionResult changeOTPreliminarStatus(string statusOTPreliminar, long idOTPreliminar,string message)
        {
            try
            {
                TempData.Keep("LaboratorioModel");
                var preliminarOTItem = _otPreliminarService.getOTPreliminarById(idOTPreliminar);
                var messageSuccess = "";                
                if (statusOTPreliminar.Equals("RECHAZADA"))
                {
                    var status = _catalogService.getStatusOTPreliminarStatus(statusOTPreliminar);
                    preliminarOTItem.StatusOTPreliminar = status;
                    preliminarOTItem.statusOTPreliminarId = status.statusOTPreliminarId;
                    var result = _otPreliminarService.edicionOTPreliminar(preliminarOTItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);                    
                    messageSuccess = "OK: Cambio de estado correctemente";           
                }
                else if (statusOTPreliminar.Equals("ENVAT"))
                {
                    var status = _catalogService.getStatusOTPreliminarStatus(statusOTPreliminar);
                    preliminarOTItem.StatusOTPreliminar = status;
                    preliminarOTItem.statusOTPreliminarId = status.statusOTPreliminarId;
                    var result = _otPreliminarService.edicionOTPreliminar(preliminarOTItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Cambio de estado correctemente";
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
                    messagePartidas = "<h3 class='block-title text-primary'><strong>ACCIONES[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageQuestions = "<h3 class='block-title text-primary'><strong>ACCIONES[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    messageSuccess = messageSuccess                    
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




        #endregion Methods
    }
}