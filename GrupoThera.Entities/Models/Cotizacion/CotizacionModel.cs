using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.Cotizacion
{
    public class CotizacionModel
    {
        public IList<FormaPago> AllFormaPago { get; set; }
        public IList<ClasificacionServicio> AllClasificacionServicio { get; set; }
        public IList<Cliente> AllCliente { get; set; }
        public IList<Servicio> AllServicio { get; set; }

        public FormaPago FormaPago { get; set; }
        public ClasificacionServicio ClasificacionServicio { get; set; }
        public Cliente Cliente { get; set; }
        public Servicio Servicio { get; set; }

        public SelectList listFormaPago { get; set; }
        public SelectList listClasificacionServicio { get; set; }
        public SelectList listCliente { get; set; }
        public SelectList listAreaServicio { get; set; }

        public int selectedFormaMago { get; set; }
        public int selectedClasificacionServicio { get; set; }
        public int selectedCliente { get; set; }
        public int selectedAreaServicio { get; set; }

        public Preliminar Preliminar { get; set; }

    }
}
