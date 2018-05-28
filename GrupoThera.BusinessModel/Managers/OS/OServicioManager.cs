using GrupoThera.BusinessLogic.Contracts.OS;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Contracts.OS;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.OS;
using GrupoThera.Entities.Entity.OTPre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GrupoThera.BusinessModel.Managers.OS
{
    public class OServicioManager : IOServicioService
    {
        #region Fields

        private IOServicio _oServicioDA;
        private IOServicioPartida _oServicioPartidaDA;
        private ICatalogService _catalogService;

        #endregion Fields 

        #region Constructor 

        public OServicioManager(IOServicio oServicioDA, IOServicioPartida oServicioPartidaDA, ICatalogService catalogService)
        {
            //Dependency Injection
            _oServicioDA = oServicioDA;
            _oServicioPartidaDA = oServicioPartidaDA;
            _catalogService = catalogService;
        }

        #endregion Constructor 

        #region Methods 

        public string createOS(OTPreliminar OTPreliminar)
        {
            OrdenServicio item = null;
            try
            {
                item = new OrdenServicio()
                {
                    empresaPrefijo = OTPreliminar.empresaPrefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = OTPreliminar.comentarios,
                    tipoCambio = OTPreliminar.tipoCambio,
                    viaticos = OTPreliminar.viaticos,
                    direccionServicio = OTPreliminar.direccionServicio,
                    iva = OTPreliminar.iva,
                    subtotal = OTPreliminar.subtotal,
                    totalIva = OTPreliminar.totalIva,
                    total = OTPreliminar.total,
                    confClave = OTPreliminar.confClave,
                    confEmision = OTPreliminar.confEmision,
                    confVigencia = OTPreliminar.confVigencia,
                    confTipoDoc = OTPreliminar.confTipoDoc,
                    confRevision = OTPreliminar.confRevision,
                    confFormato = OTPreliminar.confFormato,
                    monedaId = OTPreliminar.monedaId,
                    formaPagoId = OTPreliminar.formaPagoId,
                    empresaId = OTPreliminar.empresaId,
                    sucursalId = OTPreliminar.sucursalId,
                    statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("ABIERTA"),
                    clienteId = OTPreliminar.clienteId,
                    clasificacionServicioId = OTPreliminar.clasificacionServicioId,
                    preliminaresId = OTPreliminar.preliminaresId,
                    otPreliminarId = OTPreliminar.otPreliminarId,
                    noteId = OTPreliminar.noteId,
                    fechaEntrega = OTPreliminar.fechaEntrega,
                    fechaMuestreo = OTPreliminar.fechaMuestreo,
                    check1 = OTPreliminar.check1,
                    check2 = OTPreliminar.check2,
                    check3 = OTPreliminar.check3,
                    check4 = OTPreliminar.check4
                };
                _oServicioDA.Add(item);

                foreach (OTPrePartidas itempp in OTPreliminar.OTPrePartidas)
                {
                    for (int i = 0; i < itempp.cantidad; i++)
                    {
                        var prePartida = new OrdenServicioPartidas()
                        {
                            label = itempp.cantidad + "-" + (i + 1),
                            statusOrdenPartidasId = _catalogService.getStatusOrdenPartidasId("ABIERTA"),
                            claveServicio = "-",
                            descripcionServicio = itempp.comentarios,
                            iva = (itempp.precio / itempp.cantidad) * .16m,
                            precio = itempp.precio / itempp.cantidad,
                            descuento = 0,
                            total = (itempp.precio / itempp.cantidad) * .16m + (itempp.precio / itempp.cantidad),
                            marca = itempp.marca,
                            modelo = itempp.modelo,
                            serie = itempp.serie,
                            comentarios = itempp.comentarios,
                            cantidad = 1,
                            servicioId = Convert.ToInt64(itempp.Servicio.servicioId),
                            ordenServicioId = item.ordenServicioId
                        };
                        _oServicioPartidaDA.Add(prePartida);
                    }
                }
            }
            catch (Exception ex)
            {
                if (item.ordenServicioId != 0)
                    _oServicioDA.Delete(item);
                return "ERROR: " + ex.Message;
            }
            return item.ordenServicioId.ToString();
        }

        public IList<OrdenServicio> getAllOServicio()
        {
            return _oServicioDA.GetList().ToList();
        }

        public OrdenServicio getOServicioById(long idOTPreliminar)
        {
            return _oServicioDA.Get(t => t.ordenServicioId == idOTPreliminar);
        }

        public IList<OrdenServicioPartidas> getAllPArtidasByOServicio(long idOrdenServicio)
        {
            return _oServicioPartidaDA.GetList(t => t.ordenServicioId == idOrdenServicio).ToList();
        }

        public IList<OrdenServicioPartidas> getAllPrePartidasByPreliminar(long idOTServicio)
        {
            return _oServicioPartidaDA.GetList(t => t.ordenServicioId == idOTServicio).ToList();
        }

        public string updateOSPartida(long idOrdenServicioPartida, long idStatusOrdenPartida)
        {
            var itemPartida = _oServicioPartidaDA.Get(t => t.oServicioPartidasId == idOrdenServicioPartida);
            itemPartida.StatusOrdenPartidas = null;
            itemPartida.statusOrdenPartidasId = idStatusOrdenPartida;
            _oServicioPartidaDA.Update(itemPartida);

            var listItemPartida = _oServicioPartidaDA.GetList(t => t.ordenServicioId == itemPartida.ordenServicioId);
            var itemOS = _oServicioDA.Get(t => t.ordenServicioId == itemPartida.ordenServicioId);
            itemOS.StatusOrdenServicio = null;

            var todas = listItemPartida.Count();

            var abierta = listItemPartida.Where(t => t.statusOrdenPartidasId.Equals(_catalogService.getStatusOrdenPartidasId("ABIERTA"))).Count();
            var canceladas = listItemPartida.Where(t => t.statusOrdenPartidasId.Equals(_catalogService.getStatusOrdenPartidasId("CANCELA"))).Count();
            var realizadas = listItemPartida.Where(t => t.statusOrdenPartidasId.Equals(_catalogService.getStatusOrdenPartidasId("REALIZADA"))).Count();
            var proceso = listItemPartida.Where(t => t.statusOrdenPartidasId.Equals(_catalogService.getStatusOrdenPartidasId("PROCESO"))).Count();
            var programada = listItemPartida.Where(t => t.statusOrdenPartidasId.Equals(_catalogService.getStatusOrdenPartidasId("PROGRAMADA"))).Count();
            var xcancelar = listItemPartida.Where(t => t.statusOrdenPartidasId.Equals(_catalogService.getStatusOrdenPartidasId("XCANCELAR"))).Count();

            if (todas == canceladas)
            {
                itemOS.statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("CANCEL");
            }
            else if (realizadas > 0)
            {
                itemOS.statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("PARTIAL");
            }
            else if (proceso > 0)
            {
                itemOS.statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("PROCESS");
            }
            else if (programada > 0)
            {
                itemOS.statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("PROGRAM");
            }
            else if (abierta == 0 && xcancelar == 0)
            {
                itemOS.statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("CLOSE");
            }
            else
            {
                itemOS.statusOrdenServicioId = _catalogService.getStatusOrdenServicioId("ABIERTA");
            }
            _oServicioDA.Update(itemOS);
            return "OK";
        }

        public string edicionOServicio(OrdenServicio OrdenServicio)
        {
            try
            {
                _oServicioDA.Update(OrdenServicio);
            }
            catch (Exception ex)
            {
                return "ERROR ORDEN SERVICIO: " + ex.Message;
            }
            return "OK";
        }

        #endregion Methods  
    }
}
