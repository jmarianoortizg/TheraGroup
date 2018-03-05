using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.OTPre;
using GrupoThera.Entities.Models.Cotizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessModel.Contracts.OT
{
    public interface IOTPreliminarService
    {
        #region Methods 

        string createOT(Preliminar Preliminar);
        IList<OTPreliminar> getAllOTPreliminar();

        #endregion Methods
    }
}
