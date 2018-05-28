using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.OTPre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Models.AreaTecnica
{
    public class ATModel
    {
        public ATModel()
        {
            OTPreliminaresActual = new List<OTPreliminar>();
        }

        public IList<OTPreliminar> listOTPreliminares { get; set; }
        public IList<OTPreliminar> OTPreliminaresActual { get; set; }
        public IList<OTPreliminar> abiertas { get; set; }
        public IList<OTPreliminar> pendientes { get; set; }

        public IList<Note> listNotes { get; set; }
        public Note note { get; set; }
        public OTPreliminar otpreliminar { get; set; }
    }
}
