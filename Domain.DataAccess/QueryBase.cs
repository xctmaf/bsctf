namespace Domain.DataAccess
{
    using System.Data;
    using ByndyuSoft.Infrastructure.Dapper;
    using ByndyuSoft.Infrastructure.Domain;

    public abstract class QueryBase<TCriterion, TResult> : IQuery<TCriterion, TResult> where TCriterion : ICriterion
    {
        private IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;

        protected QueryBase(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected IDbConnection Connection => _connection ?? (_connection = _connectionFactory.Create());

        protected abstract TResult Process(TCriterion context);

        public TResult Ask(TCriterion criterion)
        {
            try
            {
                return Process(criterion);
            }
            finally
            {
                _connection?.Close();
            }
        }
    }
}