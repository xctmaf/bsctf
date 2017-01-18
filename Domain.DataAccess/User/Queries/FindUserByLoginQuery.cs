namespace Domain.DataAccess.User.Queries
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Dapper;
    using Entities;
    using QueryContexts;
    using QueryObjects;

    public class FindUserByLoginQuery : QueryBase<FindUserByLogin, User>
    {
        public FindUserByLoginQuery(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override User Process(FindUserByLogin context)
        {
            return Connection.Query<User>(SelectUser.ByLogin(context.Login)).SingleOrDefault();
        }
    }
}