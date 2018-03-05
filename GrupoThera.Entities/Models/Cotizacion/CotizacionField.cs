using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrupoThera.Entities.Models.Cotizacion
{
    public class CotizacionField
    {
        #region Fields

        public long qty { get; set; }
        public string noSerie { get; set; }
        public string marca { get; set; }
        public string model { get; set; }
        public string claveServicioCode { get; set; }
        public string claveServicioText { get; set; }
        public string comentarios { get; set; }
        public long idServicio { get; set; }
        public decimal priceSuggest { get; set; }
        public decimal priceSelected { get; set; }
        public decimal priceFinal { get; set; }
        public bool approval { get; set; }

        #endregion Fields 
    }
}
