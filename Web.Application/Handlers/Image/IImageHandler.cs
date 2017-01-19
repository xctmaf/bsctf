namespace Web.Application.Handlers.Image
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models.Image.Output;

    public interface IImageHandler
    {
        Task<OutputFileModel[]> DoSome(HttpContent content);
    }
}