namespace BSCTF.IoC.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Domain.Services;
    using Domain.Services.Implimetations;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IPasswordHasher>().ImplementedBy<PasswordHasher>().LifestyleSingleton()
                               , Component.For<ISaltGenerator>().ImplementedBy<SaltGenerator>().LifestyleSingleton()
                               , Component.For<IFileService>().ImplementedBy<FileService>().LifestyleSingleton());
        }
    }
}