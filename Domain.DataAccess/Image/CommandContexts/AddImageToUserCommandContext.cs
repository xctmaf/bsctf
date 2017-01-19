namespace Domain.DataAccess.Image.CommandContext
{
    using ByndyuSoft.Infrastructure.Domain.Commands;

    public class AddImageToUserCommandContext : ICommandContext
    {
        public AddImageToUserCommandContext(string filename, string login)
        {
            Filename = filename;
            Login = login;
        }

        public string Filename { get; private set; }
        public string Login { get; private set; }
    }
}