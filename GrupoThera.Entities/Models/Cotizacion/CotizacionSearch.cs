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

        // Filter Section
        public string from { get; set; }
        public string to { get; set; }
        public long nCotizacion { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string nSerie { get; set; }
        public SelectList listCliente { get; set; }
        public int selectedCliente { get; set; }
        public SelectList listClasificacion { get; set; }
        public int selectedClasificacion { get; set; }
        public SelectList listStatus { get; set; }
        public int selectedStatus { get; set; }
    }
}
