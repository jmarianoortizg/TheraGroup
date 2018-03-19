using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.OTPre;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.OTPre
{
    public class OTPreliminarSearch
    {
        public OTPreliminarSearch()
        {
            OTPreliminaresActual = new List<OTPreliminar>();
        }

        public IList<OTPreliminar> listaOTPreliminares { get; set; }
        public IList<OTPreliminar> OTPreliminaresActual { get; set; }
        public IList<OTPreliminar> abiertas { get; set; }
        public IList<OTPreliminar> cerrada { get; set; }
        public IList<OTPreliminar> obsoleta { get; set; }
        public IList<OTPreliminar> cancelada { get; set; }
        public IList<OTPreliminar> aceptadaAT { get; set; }
        public IList<OTPreliminar> rechazada { get; set; }
        public IList<OTPreliminar> laboratorio { get; set; }

        // Filter Section
        public string from { get; set; }
        public string to { get; set; }
        public long nOTPreliminar { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string nSerie { get; set; }
        public SelectList listCliente { get; set; }
        public int selectedCliente { get; set; }
        public SelectList listClasificacion { get; set; }
        public int selectedClasificacion { get; set; }
        public SelectList listStatus { get; set; }
        public int selectedStatus { get; set; }

        public IList<Note> listNotes { get; set; }
        public Note note { get; set; }
        public OTPreliminar otpreliminar { get; set; }



    }
}
