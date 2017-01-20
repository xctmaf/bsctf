namespace Web.Application.Handlers.Image
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models.Image.Output;

    public interface IImageHandler
    {
        Task<OutputFileModel[]> UploadFiles(HttpContent content, string apiPath, string login, string description);
        OutputFileModel[] Find(string term);
        Task<OutputFileModel[]> AddFromUrl(string apiPath, string login, string url);
        OutputFileModel[] List(string login);
    }
}