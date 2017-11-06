using GrupoThera.Business.Interfaces;
using GrupoThera.Business.Managers;
using GrupoThera.BusinessLogic.Contracts.Mock;
using GrupoThera.BusinessLogic.DataAccess;
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
            #region Mock

                kernel.Bind<IProductService>().To<ProductManager>().InSingletonScope();
                kernel.Bind<IProduct>().To<ProductDAO>().InSingletonScope();

            #endregion Mock 
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