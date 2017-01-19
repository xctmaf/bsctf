namespace Web.Application.Handlers.Image
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models.Image.Output;

    public interface IImageHandler
    {
        Task<OutputRealFileModel[]> UploadFiles(HttpContent content, string apiPath, string login);
        OutputFileModel[] Find(string s);
        OutputFileModel AddFromInstagram(string url);
        OutputFileModel[] List();
    }
}