using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.OS;
using GrupoThera.Entities.Entity.OTPre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.AreaTecnica
{
    public class ATOSModel
    {
        public ATOSModel()
        {
            ordenServicioActual = new List<OrdenServicio>();
        }

        public IList<OrdenServicio> listOrdenServicio { get; set; }
        public IList<OrdenServicio> ordenServicioActual { get; set; }
        public IList<OrdenServicio> abiertas { get; set; }
        public IList<OrdenServicio> cancelada { get; set; }
        public IList<OrdenServicio> cerrada { get; set; }
        public IList<OrdenServicio> proceso { get; set; }
        public IList<OrdenServicio> parcial { get; set; }
        public IList<OrdenServicio> programada { get; set; }


        public IList<OrdenServicio> today { get; set; }
        public IList<OrdenServicio> pendientes { get; set; }


        public IList<Note> listNotes { get; set; }
        public Note note { get; set; }
        public OrdenServicio ordenServicio { get; set; }

        public IList<StatusOrdenPartidas> AllStatusOrdenPartidas { get; set; }
        public SelectList listStatusOrdenPartidasInitial { get; set; }
        public SelectList listStatusOrdenPartidasProcess { get; set; }
        public int selectedStatusOrdenPartidas { get; set; }
    }
}
