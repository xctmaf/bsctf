namespace Domain.DataAccess.Image.Queries
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Dapper;
    using Entities;
    using QueryContexts;
    using QueryObjects;

    public class FindImagesByDescriptionQuery : QueryBase<FindImagesByDescription, UserFile[]>
    {
        public FindImagesByDescriptionQuery(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override UserFile[] Process(FindImagesByDescription context)
        {
            return Connection.Query<UserFile>(SelectFile.ByDescription(context.Term)).ToArray();
        }
    }
}