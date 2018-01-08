using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.Cotizacion
{
    public class CotizacionModel
    {
        public IList<FormaPago> allFormaPago { get; set; }
        public IList<ClasificacionServicio> allClasificacionServicio { get; set; }
        public IList<Cliente> allCliente { get; set; }
        public IList<Servicio> allServicio { get; set; }

        public FormaPago formaPago { get; set; }
        public ClasificacionServicio clasificacionServicio { get; set; }
        public Cliente cliente { get; set; }
        public Servicio servicio { get; set; }
        public Moneda moneda { get; set; }
        public Empresa empresa { get; set; }
        public Sucursal sucursal { get; set; }


        public SelectList listFormaPago { get; set; }
        public SelectList listClasificacionServicio { get; set; }
        public SelectList listCliente { get; set; }
        public SelectList listServicio { get; set; }
        public SelectList listMoneda { get; set; }

        public int selectedFormaPago { get; set; }
        public int selectedClasificacionServicio { get; set; }
        public int selectedCliente { get; set; }
        public int selectedMoneda { get; set; }
        public int selectedServicio { get; set; }
        public long selectedSucursal { get; set; }


        public Preliminar preliminar { get; set; }
        public Collection<CotizacionField> cotizacionFields { get; set; }
        public CotizacionField cotizacionField { get; set; }
        public string selectedPrice { get; set; }

        public decimal subtotal { get; set; }
        public decimal iva { get; set; }
        public decimal total { get; set; }
        public decimal cantidad { get; set; }
        public decimal noPartidas { get; set; }

    }
}
