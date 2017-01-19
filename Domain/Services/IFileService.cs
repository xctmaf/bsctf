namespace Domain.Services
{
    using System.Threading.Tasks;

    public interface IFileService
    {
        Task Save(string fileName, string folder, byte[] content);
    }
}