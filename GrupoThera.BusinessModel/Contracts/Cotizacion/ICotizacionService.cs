using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Models.Cotizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessModel.Contracts.Cotizacion
{
    public interface ICotizacionService
    {
        #region Methods 

        string createPreliminar(CotizacionModel CotizacionModel);
        IList<Preliminar> getAllPreliminar();

        #endregion Methods
    }
}
