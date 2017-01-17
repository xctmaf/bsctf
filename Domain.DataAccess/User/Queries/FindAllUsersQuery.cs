namespace Domain.DataAccess.User.Queries
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Dapper;
    using ByndyuSoft.Infrastructure.Domain.Criteria;
    using Entities;
    using JetBrains.Annotations;
    using QueryObjects;

    [UsedImplicitly]
    public class FindAllUsersQuery : QueryBase<AllEntities, User[]>
    {
        public FindAllUsersQuery(IConnectionFactory connectionFactory) : base(connectionFactory){}

        protected override User[] Process(AllEntities context)
        {
            return Connection.Query<User>(SelectUser.All()).ToArray();
        }
    }
}