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
    }
}
