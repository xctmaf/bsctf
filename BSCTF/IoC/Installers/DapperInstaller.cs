namespace BSCTF.IoC.Installers
{
    using ByndyuSoft.Infrastructure.Dapper;
    using ByndyuSoft.Infrastructure.Domain;
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.Resolvers.SpecializedResolvers;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Domain.DataAccess.User.Commands;
    using Domain.DataAccess.User.Queries;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class DapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<TypedFactoryFacility>();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            var commands = Classes.FromAssemblyContaining<RegisterNewUserCommand>()
                                  .BasedOn(typeof(ICommand<>))
                                  .WithService.AllInterfaces()
                                  .Configure(x => x.LifestyleTransient());

            var queries = Classes.FromAssemblyContaining<FindAllUsersQuery>()
                                .BasedOn(typeof(IQuery<,>))
                                .WithService.AllInterfaces()
                                .LifestyleTransient();

            container.Register(commands,
                               queries,
                               Component.For<IQueryBuilder>().AsFactory().LifestyleTransient(),
                               Component.For<IQueryFactory>().AsFactory().LifestyleTransient(),
                               Component.For(typeof(IQueryFor<>)).ImplementedBy(typeof(QueryFor<>)).LifestyleTransient(),
                               Component.For<IConnectionFactory>().ImplementedBy<ConnectionFactory>().LifestyleTransient(),
                               Component.For<ICommandBuilder>().ImplementedBy(typeof(CommandBuilder)).LifestyleTransient(),
                               Component.For<ICommandFactory>().AsFactory().LifestyleTransient());
        }
    }
}