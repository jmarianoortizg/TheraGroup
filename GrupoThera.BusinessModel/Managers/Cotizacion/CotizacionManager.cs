using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.Cotizacion;
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

        #endregion Fields 

        #region Constructor 

        public CotizacionManager(IPreliminar preliminarDA, IPrePartida prePartidaDA)
        {
            //Dependency Injection
            _preliminarDA = preliminarDA;
            _prePartidaDA = prePartidaDA;
        }

        #endregion Constructor 

        #region Methods 

        public string createPreliminar(CotizacionModel CotizacionModel)
        {
            Preliminar item = null;
            try
            {
                item = new Preliminar()
                {
                    noDoc = 0,
                    noDocInt = 0,
                    empresaPrefijo = CotizacionModel.sucursal.prefijo,
                    fechaCreacion = DateTime.Now,
                    owner = (string)HttpContext.Current.Session["UserName"],
                    comentarios = CotizacionModel.preliminar.comentarios,
                    tipoCambio = CotizacionModel.moneda.tipoCambio.ToString(),
                    viaticos = 0,
                    direccionServicio = CotizacionModel.preliminar.direccionServicio,
                    iva = CotizacionModel.iva,
                    subtotal = CotizacionModel.subtotal,
                    total = CotizacionModel.total,
                    confClave = "",
                    confEmision = "",
                    confVigencia = "",
                    confTipoDoc = "",
                    confRevision = "",
                    confFormato = "",
                    statusCotizacionId = 13,
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

                    };
                    _prePartidaDA.Add(prePartida);
                }
            }
            catch (Exception ex)
            {
                if(item.preliminaresId != 0 )
                    _preliminarDA.Delete(item);
                return ex.Message;
            }
            return "OK";
        }
        #endregion Methods  
    }
}
