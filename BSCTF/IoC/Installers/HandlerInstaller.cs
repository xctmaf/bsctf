namespace BSCTF.IoC.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using JetBrains.Annotations;
    using Web.Application.Handlers.Temp;

    [UsedImplicitly]
    public class HandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITempHandler>().ImplementedBy<TempHandler>());
        }
    }
}