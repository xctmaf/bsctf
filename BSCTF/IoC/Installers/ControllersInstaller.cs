namespace BSCTF.IoC.Installers
{
    using System.Web.Http;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers.Temp;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<TempController>().BasedOn<ApiController>().Configure(x => x.LifestylePerWebRequest()));
        }
    }
}