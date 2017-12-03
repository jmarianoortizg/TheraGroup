using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.General;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GrupoThera.Entities.Models.Catalog
{
    public class CatalogModel
    {
        public IList<AreaServicio> AllAreaServicio { get; set; }
        public IList<Ciudad> AllCiudad { get; set; }
        public IList<ClasificacionServicio> AllClasificacionServicio { get; set; }
        public IList<Cliente> AllCliente { get; set; }
        public IList<Configuracion> AllConfiguracion { get; set; }
        public IList<Empresa> AllEmpresa { get; set; }
        public IList<Estado> AllEstado { get; set; }
        public IList<FormaPago> AllFormaPago { get; set; }
        public IList<FrecuenciaServicio> AllFrecuenciaServicio { get; set; }
        public IList<Giro> AllGiro { get; set; }
        public IList<MetodoCotizacion> AllMetodoCotizacion { get; set; }
        public IList<Moneda> AllMoneda { get; set; }
        public IList<Provedor> AllProvedor { get; set; }
        public IList<Servicio> AllServicio { get; set; }
        public IList<TiempoEntrega> AllTiempoEntrega { get; set; }

        public AreaServicio AreaServicio { get; set; }
        public Ciudad Ciudad { get; set; }
        public ClasificacionServicio ClasificacionServicio { get; set; }
        public Cliente Cliente { get; set; }
        public Configuracion Configuracion { get; set; }
        public Empresa Empresa { get; set; }
        public Estado Estado{ get; set; }
        public FormaPago FormaPago { get; set; }
        public FrecuenciaServicio FrecuenciaServicio { get; set; }
        public Giro Giro { get; set; }
        public MetodoCotizacion MetodoCotizacion { get; set; }
        public Moneda Moneda { get; set; }
        public Provedor Provedor { get; set; }
        public Servicio Servicio { get; set; }
        public TiempoEntrega TiempoEntrega { get; set; }

        public SelectList listAreaServicio { get; set; }
        public SelectList listCiudad { get; set; }
        public SelectList listClasificacionServicio { get; set; }
        public SelectList listCliente { get; set; }
        public SelectList listConfiguracion { get; set; }
        public SelectList listEmpresa { get; set; }
        public SelectList listEstado { get; set; }
        public SelectList listFormaPago { get; set; }
        public SelectList listFrecuenciaServicio { get; set; }
        public SelectList listGiro { get; set; }
        public SelectList listMetodoCotizacion { get; set; }
        public SelectList listMoneda { get; set; }
        public SelectList listProvedor { get; set; }
        public SelectList listServicio { get; set; }
        public SelectList listTiempoEntrega { get; set; }

        public int selectedAreaServicio { get; set; }
        public int selectedCiudad { get; set; }
        public int selectedClasificacionServicio { get; set; }
        public int selectedCliente { get; set; }
        public int selectedConfiguracion { get; set; }
        public int selectedEmpresa { get; set; }
        public int selectedEstado { get; set; }
        public int selectedFormaMago { get; set; }
        public int selectedFrecuenciaServicio { get; set; }
        public int selectedGiro { get; set; }
        public int selectedMetodoCotizacion { get; set; }
        public int selectedMoneda { get; set; }
        public int selectedProvedor { get; set; }
        public int selectedServicio { get; set; }
        public int selectedTiempoEntrega { get; set; }


    }
}
