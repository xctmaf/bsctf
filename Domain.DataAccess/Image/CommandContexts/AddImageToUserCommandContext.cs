namespace Domain.DataAccess.Image.CommandContexts
{
    using ByndyuSoft.Infrastructure.Domain.Commands;

    public class AddImageToUserCommandContext : ICommandContext
    {
        public AddImageToUserCommandContext(string fileName, string filePath, string login, string descript)
        {
            FileName = fileName;
            Login = login;
            Descript = descript;
            FilePath = filePath;
        }

        public string FileName { get; private set; }
        public string Descript { get; private set; }
        public string FilePath { get; private set; }
        public string Login { get; private set; }
    }
}