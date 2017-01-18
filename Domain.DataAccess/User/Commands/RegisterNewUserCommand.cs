namespace Domain.DataAccess.User.Commands
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Dapper;
    using CommnadContexts;
    using Entities;
    using Exceptions;
    using JetBrains.Annotations;
    using QueryObjects;

    [UsedImplicitly]
    public class RegisterNewUserCommand : CommandBase<RegisterNewUserCommandContexts>
    {
        public RegisterNewUserCommand(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override void Process(RegisterNewUserCommandContexts context)
        {
            var existUser = Connection.Query<User>(SelectUser.ByLogin(context.Login));

            if (existUser.Any())
                throw new DuplicateEntityException();

            Connection.Execute(InsertUser.New(context.Login, context.Password, context.Username, context.Salt));
        }
    }
}