namespace Web.Application.Handlers.Image
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using Domain.DataAccess.Image.CommandContexts;
    using Domain.Services;
    using Exceprtions;
    using Models.Image.Input;
    using Models.Image.Output;

    public class ImageHandler : IImageHandler
    {
        private static readonly string RootFolder = ConfigurationManager.AppSettings["filepath"];
        private readonly ICommandBuilder _commandBuilder;
        private readonly IFileService _fileService;
        private readonly IEnumerable<IImageLoader> _imageLoaders;

        public ImageHandler(IFileService fileService, ICommandBuilder commandBuilder, IEnumerable<IImageLoader> imageLoaders)
        {
            _fileService = fileService;
            _commandBuilder = commandBuilder;
            _imageLoaders = imageLoaders;
        }

        public async Task<OutputRealFileModel[]> UploadFiles(HttpContent content, string apiPath, string login)
        {
            var files = await GetFiles(content);
            var fileFolder = GetFileFolder(apiPath, login);
            var result = new List<OutputRealFileModel>();

            foreach (var file in files)
            {
                var newFileName = GetNewFileName(file.Filename);
                var filePath = Path.Combine(fileFolder, newFileName);
                await _fileService.Save(filePath, file.Content);
                _commandBuilder.Execute(new AddImageToUserCommandContext(file.Filename, filePath, login));
                result.Add(new OutputRealFileModel {UploadName = file.Filename, RealName = newFileName});
            }

            return result.ToArray();
        }

        private static string GetNewFileName(string oldFileName)
        {
            return Path.GetFileNameWithoutExtension(oldFileName)
                   + DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds
                   + Path.GetExtension(oldFileName);
        }

        public OutputFileModel[] Find(string s)
        {
            return new[] {new OutputFileModel {FileName = "asd"}};
        }

        public OutputFileModel[] List()
        {
            return new[] {new OutputFileModel {FileName = "asd"}};
        }

        public async Task<OutputFileModel> AddFromUrl(string apiPath, string login, string url)
        {
            var allImageLoaders = _imageLoaders.Where(x => x.IsAccept(url)).ToArray();

            if (allImageLoaders.Length != 1)
                throw new UnsupportedUrlException();

            var imageLoader = allImageLoaders.Single();
            var image = await imageLoader.GetFile(url);

            if(image == null)
                throw new IncorrectUrlException();

            var newFileName = GetNewFileName(image.FileName);
            await _fileService.Save(Path.Combine(GetFileFolder(apiPath, login), newFileName), image.Content);

            return new OutputFileModel {FileName = newFileName};
        }

        private static async Task<FileModel[]> GetFiles(HttpContent content)
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
            var tasks = provider.Contents.Select(async x => new FileModel
                                                                {
                                                                    Filename = x.Headers.ContentDisposition.FileName.Trim('\"'),
                                                                    Content = await x.ReadAsByteArrayAsync(),
                                                                    Size = x.Headers.ContentDisposition.Size
                                                                })
                                .ToArray();
            return await Task.WhenAll(tasks);
        }

        private static string GetFileFolder(string apiPath, string login)
        {
            return Path.Combine(apiPath, RootFolder, login);
        }
    }
}