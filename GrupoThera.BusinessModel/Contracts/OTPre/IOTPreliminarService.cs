using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.OTPre;
using GrupoThera.Entities.Models.Cotizacion;
using GrupoThera.Entities.Models.OTPre;
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
        OTPreliminar getOTPreliminarById(long idOTPreliminar);
        IList<OTPrePartidas> getAllPrePartidasByOTPreliminar(long idOTPreliminar);
        string edicionOTPreliminar(OTPreliminarSearch otpreliminarSearch);
        string edicionOTPreliminar(OTPreliminar OTPreliminar);
        string duplicateOTPreliminar(OTPreliminar OTPreliminar);
        string newVersion(OTPreliminar otpreliminarItem);

        #endregion Methods
    }
}
