namespace Domain.DataAccess.Image.CommandContexts
{
    using ByndyuSoft.Infrastructure.Domain.Commands;

    public class AddImageToUserCommandContext : ICommandContext
    {
        public AddImageToUserCommandContext(string fileName, string filePath, string login)
        {
            FileName = fileName;
            Login = login;
            FilePath = filePath;
        }

        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string Login { get; private set; }
    }
}