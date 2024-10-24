[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Ecoporto.Agendamento.Portal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Ecoporto.Agendamento.Portal.App_Start.NinjectWebCommon), "Stop")]

namespace Ecoporto.Agendamento.Portal.App_Start
{
    using Ecoporto.Agendamento.Portal.Application.Interfaces;
    
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ecoporto.Agendamento.Portal.Domain.Interfaces.Repository;
    using Ecoporto.Agendamento.Portal.Domain.Interfaces.Services;
    using Ecoporto.Agendamento.Portal.Domain.Services;
    using Ecoporto.Agendamento.Portal.Application.Services;
    using Ecoporto.Agendamento.Portal.Data.Repository;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILoginAppServices>().To<LoginAppServices>();
            kernel.Bind<ITiposAgendamentosAppServices>().To<TiposAgendamentosAppServices>();
            kernel.Bind<IVeiculosAppServices>().To<VeiculosAppServices>();
            kernel.Bind<IMotoristaAppServices>().To<MotoristaAppServices>();

            kernel.Bind<ILoginServices>().To<LoginServices>();
            kernel.Bind<ITiposAgendamentosServices>().To<TiposAgendamentosServices>();
            kernel.Bind<IVeiculosServices>().To<VeiculosServices>();
            kernel.Bind<IMotoristaServices>().To<MotoristaServices>();


            kernel.Bind<ILoginRepository>().To<LoginRepository>();
            kernel.Bind<ITiposAgendamentosRepository>().To<TiposAgendamentosRepository>();
            kernel.Bind<IVeiculosRepository>().To<VeiculoRepository>();
            kernel.Bind<IMotoristaRepository>().To<MotoristaRepository>();


        }
    }
}