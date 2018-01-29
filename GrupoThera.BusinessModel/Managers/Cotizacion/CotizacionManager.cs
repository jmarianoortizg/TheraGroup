using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Models.Cotizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GrupoThera.BusinessModel.Managers.Cotizacion
{
    public class CotizacionManager : ICotizacionService
    {
        #region Fields

        private IPreliminar _preliminarDA;
        private IPrePartida _prePartidaDA;
        private IConfiguracion _configuracionDA;

        #endregion Fields 

        #region Constructor 

        public CotizacionManager(IPreliminar preliminarDA, IPrePartida prePartidaDA, IConfiguracion configuracionDA)
        {
            //Dependency Injection
            _preliminarDA = preliminarDA;
            _prePartidaDA = prePartidaDA;
            _configuracionDA = configuracionDA;
        }

        #endregion Constructor 

        #region Methods 

        public string createPreliminar(CotizacionModel CotizacionModel)
        {
            Preliminar item = null;
            try
            {
                var configuracion1 = _configuracionDA.GetList().ToList().Where(t => t.parametro.Equals("CVE-COT-PRE")).FirstOrDefault();
                var configuracion2 = _configuracionDA.GetList().ToList().Where(t => t.parametro.Equals("REPCOT-TIPO-DOC")).FirstOrDefault();
                var configuracion3 = _configuracionDA.GetList().ToList().Where(t => t.parametro.Equals("FORMAT_COT_WEL")).FirstOrDefault();

                item = new Preliminar()
                {
                    noDoc = 0,
                    noDocInt = 0,
                    empresaPrefijo = CotizacionModel.sucursal.prefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = CotizacionModel.preliminar.comentarios,
                    tipoCambio = CotizacionModel.moneda.tipoCambio,
                    viaticos = 0,
                    direccionServicio = CotizacionModel.preliminar.direccionServicio,
                    iva = CotizacionModel.iva,
                    subtotal = CotizacionModel.subtotal,
                    total = CotizacionModel.total,
                    confClave = configuracion1.valor1,
                    confEmision = configuracion1.valor2,
                    confVigencia = configuracion1.valor3,
                    confTipoDoc = configuracion2.valor1,
                    confRevision = configuracion1.valor2,
                    confFormato = configuracion3.valor1,
                    statusCotizacionId = 13, //Abierta
                    approbation = CotizacionModel.cotizacionFields.Where(t => t.approval == true).Count() == 0 ? false : true,
                    monedaId = CotizacionModel.selectedMoneda,
                    formaPagoId = CotizacionModel.selectedMoneda,
                    clienteId = CotizacionModel.selectedCliente,
                    clasificacionServicioId = CotizacionModel.selectedClasificacionServicio,
                    sucursalId = CotizacionModel.sucursal.sucursalId,
                    empresaId = CotizacionModel.empresa.empresaId
                };
                _preliminarDA.Add(item);

                foreach (CotizacionField cf in CotizacionModel.cotizacionFields)
                {
                    decimal iva = Convert.ToDecimal(cf.priceFinal * .16);
                    var prePartida = new PrePartidas()
                    {
                        claveServicio = cf.claveServicioText,
                        descripcionServicio = cf.comentarios,
                        iva = iva,
                        precio = cf.priceFinal,
                        descuento = 0,
                        total = cf.priceFinal + iva,
                        marca = cf.marca,
                        modelo = cf.model,
                        serie = cf.noSerie,
                        comentarios = cf.comentarios,
                        cantidad = cf.qty,
                        servicioId = Convert.ToInt64(cf.claveServicioCode),
                        preliminaresId = item.preliminaresId,
                        approbation = cf.approval
                    };
                    _prePartidaDA.Add(prePartida);
                }
            }
            catch (Exception ex)
            {
                if (item.preliminaresId != 0)
                    _preliminarDA.Delete(item);
                return "ERROR: " + ex.Message;
            }
            return item.preliminaresId.ToString();
        }

        public Preliminar getPreliminarById(long idPreliminar)
        {
            return _preliminarDA.Get(t => t.preliminaresId == idPreliminar);
        }

        public IList<Preliminar> getAllPreliminar()
        {
            return _preliminarDA.GetList().ToList();
        }

        public IList<PrePartidas> getAllPrePartidasByPreliminar(long idPreliminar)
        {
            return _prePartidaDA.GetList(t=> t.preliminaresId == idPreliminar).ToList();
        }


        #endregion Methods  
    }
}
