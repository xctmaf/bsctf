namespace Web.Application.Handlers.Image
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models.Image.Output;

    public interface IImageHandler
    {
        Task<OutputRealFileModel[]> UploadFiles(HttpContent content, string apiPath, string login);
        OutputFileModel[] Find(string s);
        Task<OutputFileModel> AddFromUrl(string apiPath, string login, string url);
        OutputFileModel[] List();
    }
}