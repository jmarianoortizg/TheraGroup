using GrupoThera.Entities.Entity.OS;
using GrupoThera.Entities.Entity.OTPre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.BusinessModel.Contracts.OS
{
    public interface IOServicioService
    {
        #region Methods 

        string createOS(OTPreliminar OTPreliminar);
        IList<OrdenServicio> getAllOServicio();
        OrdenServicio getOServicioById(long idOTPreliminar);
        IList<OrdenServicioPartidas> getAllPArtidasByOServicio(long idOrdenServicio);
        IList<OrdenServicioPartidas> getAllPrePartidasByPreliminar(long idOTServicio);
        string updateOSPartida(long idOrdenServicioPartida, long idStatusOrdenPartida);
        string edicionOServicio(OrdenServicio OrdenServicio);

        #endregion Methods
    }
}

