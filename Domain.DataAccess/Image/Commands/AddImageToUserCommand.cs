namespace Domain.DataAccess.Image.Commands
{
    using System.Linq;
    using ByndyuSoft.Infrastructure.Dapper;
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using CommandContexts;
    using Entities;
    using QueryObjects;
    using User.QueryObjects;

    public class AddImageToUserCommand : CommandBase<AddImageToUserCommandContext>, ICommandContext
    {
        public AddImageToUserCommand(IConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        protected override void Process(AddImageToUserCommandContext context)
        {
            var user = Connection.Query<User>(SelectUser.ByLogin(context.Login)).Single();
            Connection.Execute(InsertImage.New(context.FileName, context.FilePath, user.Id, context.Descript));
        }
    }
}