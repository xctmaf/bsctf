namespace Domain.Services
{
    using System.Threading.Tasks;
    using ValueObject;

    public interface IImageLoader
    {
        bool IsAccept(string url);
        Task<FileInfo> GetFile(string url);
    }
}