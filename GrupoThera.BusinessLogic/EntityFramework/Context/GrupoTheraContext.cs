using GrupoThera.Entities.Entity.General;
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
                public DbSet<Empresa> Empresa { get; set; }
                public DbSet<EmpresaSucursalMap> EmpresasSucursalMap { get; set; }
                public DbSet<EmpresaSucursalUsRoleMap> EmpresasSucursalUsRoleMap { get; set; }
                public DbSet<EmpresaSucursalUsuarioMap> EmpresaSucursalUsuarioMap { get; set; }
                public DbSet<Rol> Rol { get; set; }
                public DbSet<Sucursal> Sucursal { get; set; }
                public DbSet<Usuario> Usuario { get; set; }

            #endregion General 

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
            modelBuilder.HasDefaultSchema("LD_MDM_OWNER"); 
        }
        #endregion Methods
    }
}
