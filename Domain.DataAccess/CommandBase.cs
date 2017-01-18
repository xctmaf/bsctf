namespace Domain.DataAccess
{
    using System.Data;
    using ByndyuSoft.Infrastructure.Dapper;
    using ByndyuSoft.Infrastructure.Domain.Commands;

    public abstract class CommandBase<TContext> : ICommand<TContext> where TContext : ICommandContext
    {
        private IDbConnection _connection;
        private readonly IConnectionFactory _connectionFactory;

        protected CommandBase(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected IDbConnection Connection
        {
            get { return _connection ?? (_connection = _connectionFactory.Create()); }
        }

        public void Execute(TContext commandContext)
        {
            try
            {
                Process(commandContext);
            }
            finally
            {
                if (_connection != null)
                    _connection.Close();
            }
        }

        protected abstract void Process(TContext context);
    }
}