﻿namespace BSCTF
{
    using System.Web.Http;
    using Api.IoC;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RegisterWindsor(GlobalConfiguration.Configuration);
        }

        private static void RegisterWindsor(HttpConfiguration configuration)
        {
            Container.Install(FromAssembly.This());
            Container.Kernel.Resolver.AddSubResolver(new CollectionResolver(Container.Kernel, true));

            var dependencyResolver = new WindsorDependencyResolver(Container);
            configuration.DependencyResolver = dependencyResolver;
        }
    }
}
