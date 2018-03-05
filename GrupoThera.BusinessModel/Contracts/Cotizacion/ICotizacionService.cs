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
        string edicionPreliminar(CotizacionModel CotizationModel);
        string edicionPreliminar(Preliminar Preliminar);
        string edicionPartidasPreliminar(CotizacionModel CotizationModel);
        IList<Preliminar> getAllPreliminar();
        Preliminar getPreliminarById(long idPreliminar);
        IList<PrePartidas> getAllPrePartidasByPreliminar(long idPreliminar);
        string newVersion(Preliminar preliminarItem);
        void disableParents(Preliminar preliminarItem);

        #endregion Methods
    }
}
