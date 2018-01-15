using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.Cotizacion
{
    public class CotizacionSearch
    {
        public IList<Preliminar> listaPreliminares { get; set; }        
        public IList<Preliminar> abiertas { get; set; }
        public IList<Preliminar> aprobacion { get; set; }
        public IList<Preliminar> cerrados { get; set; }
        public IList<Preliminar> canceladas { get; set; }
        public IList<Preliminar> rechazadas { get; set; }
        public IList<Preliminar> clientes { get; set; }
        public string statusActual { get; set; }
        public IList<Preliminar> preliminaresActual { get; set; }
    }
}
