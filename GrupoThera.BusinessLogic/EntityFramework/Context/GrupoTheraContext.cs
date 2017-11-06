using GrupoThera.Entities.Entity.Mock;
using System.Data.Entity;

namespace GrupoThera.BusinessLogic.EntityFramework.Context
{
    /// <summary>
    /// Represents a context class 
    /// </summary>
    public partial class GrupoTheraContext : DbContext
    {
        #region Fields

            #region Mock
                public DbSet<Product> Products { get; set; }
                public DbSet<Category> Categories { get; set; }

            #endregion Mock 

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
