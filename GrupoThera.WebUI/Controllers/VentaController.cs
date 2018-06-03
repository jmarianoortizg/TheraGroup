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
    public class VentaController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IOServicioService _oServicioService;

        #endregion Fields

        #region Constructor

        public VentaController(ICatalogService catalogService, IOServicioService oServicioService)
        {
            _catalogService = catalogService;
            _oServicioService = oServicioService;
        }

        #endregion Constructor

        #region Methods

        #region OrdenServicio

        [CustomAuthorizeAttribute(privilege = "V004OTSERVICE")]
        public ActionResult OServicio()
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
                OServicioHtml = StdClassWeb.RenderToString(PartialView("OServicioItem", model), HttpContext)
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
                listStatusOrdenPartidasProcess = DropListHelper.GetStatusOrdenPartidasVentas(_catalogService.getStatusOrdenPartidas()),
                ordenServicio = oServicioItem
            };

            return Json(new
            {
                responseOServicioView = StdClassWeb.RenderToString(PartialView("~/Views/Venta/OServicioItemView.cshtml", modelOS), HttpContext),
                responseOSNotas = StdClassWeb.RenderToString(PartialView("~/Views/Venta/OSNewNotes.cshtml", model), HttpContext),
                responseOSNewNotas = StdClassWeb.RenderToString(PartialView("~/Views/Venta/OSNotes.cshtml", model), HttpContext),
                responseOSActions = StdClassWeb.RenderToString(PartialView("~/Views/Venta/OSActions.cshtml", model), HttpContext)
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
            modelSearch.note.document = "OSVENTA";
            modelSearch.note.owner = (string)HttpContext.Session["UserName"];
            modelSearch.note.noteDocId = modelSearch.ordenServicio.noteId;

            _catalogService.AddNote(modelSearch.note);
            var model = generateNoteModelOS(modelSearch.ordenServicio.noteId);
            return PartialView("~/Views/Venta/OSNotes.cshtml", model);
        }

        public ActionResult changeStatusOrdenPartidas(long idOrdenServicioPartida, long idStatusOrdenPartida)
        {

            var result = _oServicioService.updateOSPartida(idOrdenServicioPartida, idStatusOrdenPartida);

            return Json(new
            {
                responseResult = result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult changeOServicioStatus(string statusOServicio, long idOServicio, string message)
        {
            try
            {
                TempData.Keep("ATOSModel");
                var ordenServicioItem = _oServicioService.getOServicioById(idOServicio);
                var messageSuccess = "";
                if (statusOServicio.Equals("CANCEL"))
                {
                    var status = _catalogService.getStatusOrdenServicio(statusOServicio);
                    ordenServicioItem.StatusOrdenServicio = status;
                    ordenServicioItem.statusOrdenServicioId = status.statusOrdenServicioId;
                    var result = _oServicioService.edicionOServicio(ordenServicioItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Cambio de estado correctemente";
                }

                if (!message.Equals(""))
                {
                    var note = new Note();
                    note.creation = DateTime.Now;
                    note.document = "OSVENTA";
                    note.owner = (string)HttpContext.Session["UserName"];
                    note.noteDocId = ordenServicioItem.noteId;
                    note.content = message;
                    _catalogService.AddNote(note);
                }

                return Json(new
                {
                    success = true,
                    resposeItems = "<h3 class='block-title text-primary'><strong>[</strong> Recargar Resultados <strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    resposeView = "<h3 class='block-title text-primary'><strong>VISTA GENERAL[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    resposeNotas = "<h3 class='block-title text-primary'><strong>NOTAS[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    resposeNewNotas = "<h3 class='block-title text-primary'><strong>NUEVA NOTA[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
                    resposeActions = "<h3 class='block-title text-primary'><strong>ACCIONES[</strong> OT Preliminar<strong>] </strong> </h3><h3 class='block-title' style ='margin-left:10px'><strong> No Seleccionada</strong> </h3>",
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

        #endregion OrdenServicio

        [CustomAuthorizeAttribute(privilege = "ATReporte,GeneralAreaTecnica")]
        public ActionResult ATReporte()
        {
            return View();
        }

        #endregion Methods
    }
}