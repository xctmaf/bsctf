namespace Domain.Services
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using ValueObject;

    public abstract class ImageLoaderBase : IImageLoader
    {
        public abstract bool IsAccept(string url);
        public abstract Task<FileInfo> GetFile(string url);

        protected byte[] GetFileContent(string url)
        {
            using (var webClient = new WebClient())
                return webClient.DownloadData(url);
        }
    }
}