namespace Web.Application.Handlers.User
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Domain;
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using ByndyuSoft.Infrastructure.Domain.Extensions;
    using Domain.DataAccess.User.CommnadContexts;
    using Domain.DataAccess.User.QueryContexts;
    using Domain.Entities;
    using Domain.Exceptions;
    using Domain.Services;
    using Exceprtions;
    using JetBrains.Annotations;
    using Models.User.Output;

    [UsedImplicitly]
    public class UserHandler : IUserHandler
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISaltGenerator _saltGenerator;

        public UserHandler(IQueryBuilder queryBuilder, ICommandBuilder commandBuilder, IPasswordHasher passwordHasher, ISaltGenerator saltGenerator)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
            _passwordHasher = passwordHasher;
            _saltGenerator = saltGenerator;
        }

        public UserModel[] GetInfo()
        {
            return _queryBuilder.For<User[]>().All().Select(x => new UserModel {Username = x.Username, Login = x.Login}).ToArray();
        }

        public void Register(string login, string password, string username)
        {
            try
            {
                var salt = _saltGenerator.GetBase64Salt();
                var hashedPassword = _passwordHasher.GetHashedPassword(password, salt);
                _commandBuilder.Execute(new RegisterNewUserCommandContexts(login, hashedPassword, username, salt));
            }
            catch (DuplicateEntityException ex)
            {
                throw new DuplicateUserException(ex.Message);
            }
        }

        public UserModel Login(string login, string password)
        {
            var user = _queryBuilder.For<User>().With(new FindUserByLogin(login));

            if (user == null || _passwordHasher.GetHashedPassword(password, user.Salt) != user.Password)
                return null;
            
            return new UserModel {Login = user.Login, Username = user.Username};
        }
    }
}