namespace Domain.Services
{
    public interface IFileService
    {
        void Save(string fileName, string folder, byte[] content);
    }
}