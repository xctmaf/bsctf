namespace Web.Application.Handlers.User
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Domain;
    using ByndyuSoft.Infrastructure.Domain.Extensions;
    using Domain.Entities;
    using JetBrains.Annotations;
    using Models.User;

    [UsedImplicitly]
    public class UserHandler : IUserHandler
    {
        private IQueryBuilder _queryBuilder;

        public UserHandler(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public UserModel[] GetInfo()
        {
            return _queryBuilder.For<User[]>().All().Select(x => new UserModel() {Username = x.Username, Login = x.Login}).ToArray();
        }
    }
}