namespace Domain.DataAccess.Image.Queries
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Dapper;
    using Entities;
    using QueryContexts;
    using QueryObjects;

    public class FindImagesByUserLoginQuery : QueryBase<FindImagesByUserLogin, UserFile[]>
    {
        public FindImagesByUserLoginQuery(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override UserFile[] Process(FindImagesByUserLogin context)
        {
            return Connection.Query<UserFile>(SelectFile.ByUserLogin(context.Login)).ToArray();
        }
    }
}