namespace Web.Application.Handlers.Image
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Domain.Services;
    using Models.Image.Input;
    using Models.Image.Output;

    public class ImageHandler : IImageHandler
    {
        private static readonly string RootFolder = ConfigurationManager.AppSettings["filepath"];
        private readonly IFileService _fileService;

        public ImageHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        private async Task<FileModel[]> GetFiles(HttpContent content)
        {
            var reqStream = await content.ReadAsStreamAsync();
            var tempStream = new MemoryStream();
            reqStream.CopyTo(tempStream);

            tempStream.Seek(0, SeekOrigin.End);
            var writer = new StreamWriter(tempStream);
            writer.WriteLine();
            writer.Flush();
            tempStream.Position = 0;

            var streamContent = new StreamContent(tempStream);
            foreach (var header in content.Headers)
                streamContent.Headers.Add(header.Key, header.Value);

            var provider = new MultipartMemoryStreamProvider();
            await streamContent.ReadAsMultipartAsync(provider);
            var tasks = provider.Contents.Select(
                                                 async x => new FileModel
                                                                {
                                                                    Filename = x.Headers.ContentDisposition.FileName.Trim('\"'),
                                                                    Content = await x.ReadAsByteArrayAsync(),
                                                                    Size = x.Headers.ContentDisposition.Size
                                                                })
                                .ToArray();
            return await Task.WhenAll(tasks);
        }

        public async Task<OutputFileModel[]> DoSome(HttpContent content, string userName)
        {
            var files = await GetFiles(content);
            var fileFolder = Path.Combine(RootFolder, userName);
            var result = new List<OutputFileModel>();

            foreach (var file in files)
            {
                var newFileName = Guid.NewGuid() + Path.GetExtension(file.Filename);
                _fileService.Save(newFileName, fileFolder, file.Content);
                result.Add(new OutputFileModel {UploadName = file.Filename, RealName = newFileName});
            }

            return result.ToArray();
        }
    }
}