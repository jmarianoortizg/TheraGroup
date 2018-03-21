using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
using GrupoThera.Core.Utils;
using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.OTPre;
using GrupoThera.Entities.Models.OTPre;
using GrupoThera.WebUI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrupoThera.WebUI.Controllers
{
    public class OTPreliminarController : Controller
    {
        #region Fields

        private ICatalogService _catalogService;
        private IOTPreliminarService _otPreliminarService;

        #endregion Fields

        #region Constructor

        public OTPreliminarController(ICatalogService catalogService, IOTPreliminarService otPreliminarService)
        {
            _catalogService = catalogService;
            _otPreliminarService = otPreliminarService;
        }

        #endregion Constructor

        #region Methods

        #region OTPreliminar

        [CustomAuthorizeAttribute(privilege = "OTPreliminar,GeneralOrdenTrabajo")]

        public ActionResult OTPreliminar()
        {
            var model = generateInitialModel();
            TempData["OTPreliminarSearch"] = model;
            return View(model);
        }

        public OTPreliminarSearch generateInitialModel()
        {
            var allOTPreliminares = _otPreliminarService.getAllOTPreliminar();
            var model = new OTPreliminarSearch()
            {
                listaOTPreliminares = allOTPreliminares,
                abiertas = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ABIERTA")).ToList(),
                cerrada = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("CERRADA")).ToList(),
                obsoleta = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("OBSOLETA")).ToList(),
                cancelada = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("CANCEL")).ToList(),
                aceptadaAT = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ACEPAT")).ToList(),
                rechazada = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("RECHAZADA")).ToList(),
                laboratorio = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ENVLAB")).ToList(),
                OTPreliminaresActual = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ABIERTA")).ToList(),
                listClasificacion = DropListHelper.GetClasificacionServicioValue0(_catalogService.getClasificacionServicios()),
                listCliente = DropListHelper.GetClienteValue0(_catalogService.getClientes()),
                listStatus = DropListHelper.GetStatusOTPreliminar(_catalogService.getStatusOTPreliminar())
            };
            return model;
        }

        public OTPreliminarSearch generateNoteModel(long noteID)
        {
            var model = new OTPreliminarSearch()
            {
                listNotes = _catalogService.getNotesByDocument(noteID),
                note = new Note()
            };
            return model;
        }

        public ActionResult showOTPreliminarCounterView(string statusCounter)
        {
            var model = (OTPreliminarSearch)TempData["OTPreliminarSearch"];
            TempData.Keep("OTPreliminarSearch");

            if (statusCounter.Equals("ABIERTA"))
                model.OTPreliminaresActual = model.abiertas;
            else if (statusCounter.Equals("CERRADA"))
                model.OTPreliminaresActual = model.cerrada;
            else if (statusCounter.Equals("OBSOLETA"))
                model.OTPreliminaresActual = model.obsoleta;
            else if (statusCounter.Equals("CANCEL"))
                model.OTPreliminaresActual = model.cancelada;
            else if (statusCounter.Equals("ACEPAT"))
                model.OTPreliminaresActual = model.aceptadaAT;
            else if (statusCounter.Equals("RECHAZADA"))
                model.OTPreliminaresActual = model.rechazada;
            else if (statusCounter.Equals("ENVLAB"))
                model.OTPreliminaresActual = model.laboratorio;

            return Json(new
            {
                success = true,
                cotizacionHtml = StdClassWeb.RenderToString(PartialView("SearchOTPreliminarItem", model), HttpContext),
                cotizacionCountersHtml = StdClassWeb.RenderToString(PartialView("SearchOTPCounters", model), HttpContext)
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult searchOTPreliminarFilter(OTPreliminarSearch OTPreliminarSearch)
        {
            var predicate = PredicateBuilder.False<OTPreliminar>();
            var model = (OTPreliminarSearch)TempData["OTPreliminarSearch"];
            TempData.Keep("OTPreliminarSearch");

            var allOTPreliminares = _otPreliminarService.getAllOTPreliminar();

            if (OTPreliminarSearch.selectedCliente != 0)
            {
                allOTPreliminares = allOTPreliminares.Where(i => i.clienteId == OTPreliminarSearch.selectedCliente).ToList();
            }
            if (!string.IsNullOrEmpty(OTPreliminarSearch.from) && !string.IsNullOrEmpty(OTPreliminarSearch.to))
            {
                var dateFrom = DateTime.Parse(OTPreliminarSearch.from);
                var dateTo = DateTime.Parse(OTPreliminarSearch.to);
                allOTPreliminares = allOTPreliminares.Where(t => t.fechaCreacion.Date >= dateFrom.Date && t.fechaCreacion.Date <= dateTo).ToList();
            }
            if (OTPreliminarSearch.selectedStatus != 0)
            {
                allOTPreliminares = allOTPreliminares.Where(i => i.statusOTPreliminarId == OTPreliminarSearch.selectedStatus).ToList();
            }
            if (OTPreliminarSearch.selectedClasificacion != 0)
            {
                allOTPreliminares = allOTPreliminares.Where(i => i.clasificacionServicioId == OTPreliminarSearch.selectedClasificacion).ToList();
            }
            if (OTPreliminarSearch.nOTPreliminar != 0)
            {
                allOTPreliminares = allOTPreliminares.Where(i => i.preliminaresId == OTPreliminarSearch.nOTPreliminar || i.noDoc == OTPreliminarSearch.nOTPreliminar).ToList();
            }
            //if (!string.IsNullOrEmpty(OTPreliminarSearch.marca))
            //{
            //    allOTPreliminares = allOTPreliminares.Where(i => i.marca.Equals(OTPreliminarSearch.marca)).ToList();
            //}
            //if (!string.IsNullOrEmpty(OTPreliminarSearch.modelo))
            //{
            //    allOTPreliminares = allOTPreliminares.Where(i => i.modelo.Equals(OTPreliminarSearch.modelo)).ToList();
            //}
            //if (!string.IsNullOrEmpty(OTPreliminarSearch.nSerie))
            //{
            //    allOTPreliminares = allOTPreliminares.Where(i => i.serie.Equals(OTPreliminarSearch.nSerie)).ToList();
            //}

            OTPreliminarSearch.OTPreliminaresActual = allOTPreliminares;
            OTPreliminarSearch.abiertas = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ABIERTA")).ToList();
            OTPreliminarSearch.laboratorio = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ENVLAB")).ToList();
            OTPreliminarSearch.rechazada = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("RECHAZADA")).ToList();
            OTPreliminarSearch.cancelada = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("CANCEL")).ToList();
            OTPreliminarSearch.cerrada = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("CERRADA")).ToList();
            OTPreliminarSearch.obsoleta = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("OBSOLETA")).ToList();
            OTPreliminarSearch.aceptadaAT = allOTPreliminares.Where(t => t.StatusOTPreliminar.codigo.Equals("ACEPAT")).ToList();

            return Json(new
            {
                success = true,
                otpreliminarHtml = StdClassWeb.RenderToString(PartialView("SearchOTPreliminarItem", OTPreliminarSearch), HttpContext),
                otpreliminarCountersHtml = StdClassWeb.RenderToString(PartialView("SearchOTPCounters", OTPreliminarSearch), HttpContext)
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
                responseOTPreliminarView = StdClassWeb.RenderToString(PartialView("~/Views/OTPreliminar/SearchOTPView.cshtml", otpreliminarItem), HttpContext),
                responseOTPreliminarStatus = StdClassWeb.RenderToString(PartialView("~/Views/OTPreliminar/SearchOTPStatus.cshtml", otpreliminarItem), HttpContext),
                responseOTPNotas = StdClassWeb.RenderToString(PartialView("~/Views/OTPreliminar/SearchOTPNotes.cshtml", model), HttpContext),
                responseOTPNewNotas = StdClassWeb.RenderToString(PartialView("~/Views/OTPreliminar/SearchOTPNewNotes.cshtml", model), HttpContext),
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult newNoteOTPreliminar(OTPreliminarSearch modelSearch)
        {
            modelSearch.note.creation = DateTime.Now;
            modelSearch.note.document = "OTP";
            modelSearch.note.owner = (string)HttpContext.Session["UserName"];
            modelSearch.note.noteDocId = modelSearch.otpreliminar.noteId;

            _catalogService.AddNote(modelSearch.note);
            var model = generateNoteModel(modelSearch.otpreliminar.noteId);
            return PartialView("~/Views/OTPreliminar/SearchOTPNotes.cshtml", model);
        }

        public ActionResult changeOTPreliminarStatus(string statusOTPreliminar, long idOTPreliminar)
        {
            try
            {
                TempData.Keep("OTPreliminarSearch");
                var preliminarOTItem = _otPreliminarService.getOTPreliminarById(idOTPreliminar);
                var messageSuccess = "";
                var messageHeader = false;
                if (statusOTPreliminar.Equals("ABIERTA") || statusOTPreliminar.Equals("ENVLAB") || 
                    statusOTPreliminar.Equals("CANCEL")  || statusOTPreliminar.Equals("RECHAZADA") )
                {
                    var status = _catalogService.getStatusOTPreliminarStatus(statusOTPreliminar);
                    preliminarOTItem.StatusOTPreliminar = status;
                    preliminarOTItem.statusOTPreliminarId = status.statusOTPreliminarId;
                    var result = _otPreliminarService.edicionOTPreliminar(preliminarOTItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Cambio de estado correctemente";
                }
                else if (statusOTPreliminar.Equals("OT"))
                {
                    //preliminarOTItem.OTPrePartidas = _cotizacionService.getAllPrePartidasByPreliminar(preliminarOTItem.otPreliminarId);
                    //var result = _otService.createOT(preliminarItem);
                    //if (result.Contains("Error"))
                    //    throw new Exception(result);
                    //messageSuccess = "Nueva Pre-Orden de trabajo Creada #" + result;
                    //messageHeader = true;
                    //_cotizacionService.disableParents(preliminarItem);
                }
                else if (statusOTPreliminar.Equals("CERRADA"))
                {
                    var result = _otPreliminarService.duplicateOTPreliminar(preliminarOTItem);
                    if (result.Contains("Error"))
                        throw new Exception(result);
                    messageSuccess = "Nueva Orden de Trabajo Duplicada Creada #" + result;
                    messageHeader = true;
                }
                else if (statusOTPreliminar.Equals("NEWVERSION"))
                {
                    preliminarOTItem.OTPrePartidas = _otPreliminarService.getAllPrePartidasByOTPreliminar(preliminarOTItem.otPreliminarId);
                    var result = _otPreliminarService.newVersion(preliminarOTItem);
                    if (!result.Equals("OK"))
                        throw new Exception(result);
                    messageSuccess = "OK: Nueva version creada correctamente";
                }

                var model = generateInitialModel();
                return Json(new
                {
                    success = true,
                    messageSuccess = messageSuccess,
                    headerMessage = messageHeader,
                    cotizacionCounters = StdClassWeb.RenderToString(PartialView("SearchOTPCounters", model), HttpContext)
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


        #endregion OTPreliminar

        [CustomAuthorizeAttribute(privilege = "OTServicio,GeneralOrdenTrabajo")]

        public ActionResult OTServicio()
        {
            return View();
        }

        [CustomAuthorizeAttribute(privilege = "OTReporte,GeneralOrdenTrabajo")]

        public ActionResult OTReporte()
        {
            return View();
        }

        #endregion Methods
    }
}