namespace Web.Application.Handlers.User
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Domain;
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using ByndyuSoft.Infrastructure.Domain.Extensions;
    using Domain.DataAccess.User.CommnadContexts;
    using Domain.Entities;
    using Domain.Exceptions;
    using Exceprtions;
    using JetBrains.Annotations;
    using Models.User.Output;

    [UsedImplicitly]
    public class UserHandler : IUserHandler
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public UserHandler(IQueryBuilder queryBuilder, ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }

        public UserModel[] GetInfo()
        {
            return _queryBuilder.For<User[]>().All().Select(x => new UserModel {Username = x.Username, Login = x.Login}).ToArray();
        }

        public void Register(string login, string password, string username)
        {
            try
            {
                _commandBuilder.Execute(new RegisterNewUserCommandContexts(login, password, username));
            }
            catch (DuplicateEntityException ex)
            {
                throw new DuplicateUserException(ex.Message);
            }
        }
    }
}