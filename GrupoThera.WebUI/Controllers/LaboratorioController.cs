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
            var model = (LaboratorioModel)TempData["LaboratorioModel"];
            TempData.Keep("LaboratorioModel");

            if (statusCounter.Equals("TODAY"))
                model.OTPreliminaresActual = model.abiertas;
            else if (statusCounter.Equals("PENDING"))
                model.OTPreliminaresActual = model.pendientes;

            return Json(new
            {
                success = true,
                OTPreliminarHtml = StdClassWeb.RenderToString(PartialView("OTPreliminarItem", model), HttpContext)
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




        #endregion Methods
    }
}