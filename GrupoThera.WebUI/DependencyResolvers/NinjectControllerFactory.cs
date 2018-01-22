
using GrupoThera.BusinessLogic.Contracts.Catalogs;
using GrupoThera.BusinessLogic.Contracts.Cotizacion;
using GrupoThera.BusinessLogic.Contracts.General;
using GrupoThera.BusinessLogic.DataAccess.Catalogs;
using GrupoThera.BusinessLogic.DataAccess.Cotizacion;
using GrupoThera.BusinessLogic.DataAccess.General;
using GrupoThera.BusinessModel.Contracts.Cotizacion;
using GrupoThera.BusinessModel.Contracts.General;
using GrupoThera.BusinessModel.Managers.Cotizacion;
using GrupoThera.BusinessModel.Managers.General;
using GrupoThera.Entities.Entity.Catalogs;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace GrupoThera.WebUI.DependencyResolvers
{
    /// <summary>
    /// Represents the definition of the dependecy injection 
    /// </summary>
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        #region Fields

        private IKernel _kernel;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the NinjectControllerFactory class.
        /// Create the kernel for ninject 
        /// Define the services for the injection of dependencies
        /// </summary> 
        /// 
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            RegisterServices(_kernel);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Register services for the ninject
        /// </summary>
        /// <param name="kernel"> The kernel of the register services.</param> 
        public static void RegisterServices(IKernel kernel)
        {
            #region General

            kernel.Bind<IRoleAccountService>().To<RoleAccountManager>().InSingletonScope();
            kernel.Bind<IDepartamento>().To<DepartamentoDA>().InSingletonScope();
            kernel.Bind<IEmpresa>().To<EmpresaDA>().InSingletonScope();
            kernel.Bind<IEmpresaSucursalMap>().To<EmpresaSucursalMapDA>().InSingletonScope();
            kernel.Bind<IEmpresaSucursalUsRoleMap>().To<EmpresaSucursalUsRoleMapDA>().InSingletonScope();
            kernel.Bind<IEmpresaSucursalUsuarioMap>().To<EmpresaSucursalUsuarioMapDA>().InSingletonScope();
            kernel.Bind<IRol>().To<RolDA>().InSingletonScope();
            kernel.Bind<ISucursal>().To<SucursalDA>().InSingletonScope();
            kernel.Bind<IUsuario>().To<UsuarioDA>().InSingletonScope();

            #endregion General 

            #region Catalog

            kernel.Bind<ICatalogService>().To<CatalogManager>().InSingletonScope();
            kernel.Bind<IAreaServicio>().To<AreaServicioDA>().InSingletonScope();
            kernel.Bind<ICiudad>().To<CiudadDA>().InSingletonScope();
            kernel.Bind<IClasificacionServicio>().To<ClasificacionServicioDA>().InSingletonScope();
            kernel.Bind<ICliente>().To<ClienteDA>().InSingletonScope();
            kernel.Bind<IConfiguracion>().To<ConfiguracionDA>().InSingletonScope();
            kernel.Bind<IEstado>().To<EstadoDA>().InSingletonScope();
            kernel.Bind<IFormaPago>().To<FormaPagoDA>().InSingletonScope();
            kernel.Bind<IFrecuenciaServicio>().To<FrecuenciaServicioDA>().InSingletonScope();
            kernel.Bind<IGiro>().To<GiroDA>().InSingletonScope();
            kernel.Bind<IMetodoCotizacion>().To<MetodoCotizacionDA>().InSingletonScope();
            kernel.Bind<IMoneda>().To<MonedaDA>().InSingletonScope();
            kernel.Bind<IProvedor>().To<ProvedorDA>().InSingletonScope();
            kernel.Bind<IServicio>().To<ServicioDA>().InSingletonScope();
            kernel.Bind<ITiempoEntrega>().To<TiempoEntregaDA>().InSingletonScope();
            kernel.Bind<IStatusCotizacion>().To<StatusCotizacionDA>().InSingletonScope();


            #endregion

            #region Cotizacion

            kernel.Bind<ICotizacionService>().To<CotizacionManager>().InSingletonScope();
            kernel.Bind<IPreliminar>().To<PreliminarDA>().InSingletonScope();
            kernel.Bind<IPrePartida>().To<PrePartidaDA>().InSingletonScope();

            #endregion Cotizacion 
        }

        /// <summary>
        /// Get the controller according to the url pattern
        /// </summary>
        /// <param name="requestContext"> Definition of the url route.</param> 
        /// <param name="controllerType"> Class type of the controller.</param> 
        /// /// <returns>
        /// The instance of the controller
        /// </returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }

        #endregion Methods
    }
}