using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OT;
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
                        abiertas = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("ABIERTA")).ToList(),
                        cerrada = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("CERRADA")).ToList(),
                        obsoleta = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("OBSOLETA")).ToList(),
                        cancelada = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("CANCEL")).ToList(),
                        aceptadaAT = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("ACEPAT")).ToList(),
                        rechazada = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("RECHAZADA")).ToList(),
                        laboratorio = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("ENVLAB")).ToList(),
                        OTPreliminaresActual = allOTPreliminares.Where(t => t.StatusOrden.codigo.Equals("ABIERTA")).ToList(),
                        listClasificacion = DropListHelper.GetClasificacionServicioValue0(_catalogService.getClasificacionServicios()),
                        listCliente = DropListHelper.GetClienteValue0(_catalogService.getClientes()),
                        listStatus = DropListHelper.GetStatusCotizacion(_catalogService.getStatusCotizacion()),
                    };
                    return model;
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