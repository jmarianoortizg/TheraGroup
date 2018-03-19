using GrupoThera.Entities.Entity.Catalogs;
using GrupoThera.Entities.Entity.Cotizaciones;
using GrupoThera.Entities.Entity.General;
using GrupoThera.Entities.Entity.OTPre;
using System.Data.Entity;

namespace GrupoThera.BusinessLogic.EntityFramework.Context
{
    /// <summary>
    /// Represents a context class 
    /// </summary>
    public partial class GrupoTheraContext : DbContext
    {
        #region Fields

            #region General

                public DbSet<Departamento> Departamento { get; set; }        
                public DbSet<EmpresaSucursalMap> EmpresasSucursalMap { get; set; }
                public DbSet<EmpresaSucursalUsRoleMap> EmpresasSucursalUsRoleMap { get; set; }
                public DbSet<EmpresaSucursalUsuarioMap> EmpresaSucursalUsuarioMap { get; set; }
                public DbSet<Rol> Rol { get; set; }
                public DbSet<Sucursal> Sucursal { get; set; }
                public DbSet<Usuario> Usuario { get; set; }

        #endregion General 

            #region Catalogs

                public DbSet<AreaServicio> AreaServicio { get; set; }
                public DbSet<Ciudad> Ciudad { get; set; }
                public DbSet<ClasificacionServicio> ClasificacionServicio { get; set; }
                public DbSet<Cliente> Cliente { get; set; }
                public DbSet<Configuracion> Configuracion { get; set; }
                public DbSet<Empresa> Empresa { get; set; }
                public DbSet<Estado> Estado { get; set; }
                public DbSet<FormaPago> FormaPago { get; set; }
                public DbSet<FrecuenciaServicio> FrecuenciaServicio { get; set; }
                public DbSet<Giro> Giro { get; set; }
                public DbSet<MetodoCotizacion> MetodoCotizacion { get; set; }
                public DbSet<Moneda> Moneda { get; set; }
                public DbSet<Provedor> Provedor { get; set; }
                public DbSet<Servicio> Servicio { get; set; }
                public DbSet<Note> Note { get; set; }

        #endregion Catalogs

        #region Cotizacion
        public DbSet<Preliminar> Preliminar { get; set; }
                public DbSet<PrePartidas> PrePartidas { get; set; }
                public DbSet<StatusCotizacion> StatusCotizacion { get; set; }

        #endregion Cotizacion

            #region OrdenTrabajo
                public DbSet<OTPreliminar> OrdenTrabajo { get; set; }
                public DbSet<OTPrePartidas> OTPrePartidas { get; set; }
                public DbSet<StatusOTPreliminar> StatusOrden { get; set; }
            #endregion OrdenTrabajo

        #endregion Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the context.
        /// </summary>
        public GrupoTheraContext() : base("Name = GrupoTheraContext")
        {
       
        }
        #endregion Constructors

        #region Methods 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("THERASYS_OWNER"); 
        }
        #endregion Methods
    }
}
