namespace Web.Application.Handlers.Image
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ByndyuSoft.Infrastructure.Domain;
    using ByndyuSoft.Infrastructure.Domain.Commands;
    using Domain.DataAccess.Image.CommandContexts;
    using Domain.DataAccess.Image.QueryContexts;
    using Domain.Entities;
    using Domain.Services;
    using Exceprtions;
    using Models.Image.Input;
    using Models.Image.Output;
    using FileInfo = Domain.ValueObject.FileInfo;

    public class ImageHandler : IImageHandler
    {
        private static readonly string RootFolder = ConfigurationManager.AppSettings["filepath"];
        private readonly ICommandBuilder _commandBuilder;
        private readonly IQueryBuilder _queryBuilder;
        private readonly IFileService _fileService;
        private readonly IEnumerable<IImageLoader> _imageLoaders;

        public ImageHandler(IFileService fileService, ICommandBuilder commandBuilder, IEnumerable<IImageLoader> imageLoaders, IQueryBuilder queryBuilder)
        {
            _fileService = fileService;
            _commandBuilder = commandBuilder;
            _imageLoaders = imageLoaders;
            _queryBuilder = queryBuilder;
        }

        public async Task<OutputFileModel[]> UploadFiles(HttpContent content, string apiPath, string login, string description)
        {
            var files = await GetFiles(content);
            var fileFolder = GetFileFolder(apiPath, login);
            var result = new List<OutputFileModel>();

            foreach (var file in files)
            {
                var newFileName = GetNewFileName(file.Filename);
                var filePath = Path.Combine(fileFolder, newFileName);
                await SaveFile(login, filePath, file.Filename, description, file.Content);
                result.Add(new OutputFileModel { FileName = file.Filename, FilePath = filePath , Description = description });
            }

            return result.ToArray();
        }

        private async Task SaveFile(string login, string filePath, string fileName, string descript, byte[] content)
        {
            await _fileService.Save(filePath, content);
            _commandBuilder.Execute(new AddImageToUserCommandContext(fileName, filePath, login, descript));
        }

        private static string GetNewFileName(string oldFileName)
        {
            return Path.GetFileNameWithoutExtension(oldFileName)
                   + DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds
                   + Path.GetExtension(oldFileName);
        }

        public OutputFileModel[] Find(string term)
        {
            return _queryBuilder.For<UserFile[]>().With(new FindImagesByDescription(term)).Select(x => new OutputFileModel { FileName = x.FileName, Description = x.Description, FilePath = x.FilePath }).ToArray();
        }

        public OutputFileModel[] List(string login)
        {
            return _queryBuilder.For<UserFile[]>().With(new FindImagesByUserLogin(login)).Select(x => new OutputFileModel {FileName = x.FileName, Description = x.Description, FilePath = x.FilePath}).ToArray();
        }

        public async Task<OutputFileModel[]> AddFromUrl(string apiPath, string login, string url)
        {
            var allImageLoaders = _imageLoaders.Where(x => x.IsAccept(url)).ToArray();

            if (allImageLoaders.Length != 1)
                throw new UnsupportedUrlException();

            var imageLoader = allImageLoaders.Single();
            var image = await imageLoader.GetFile(url);

            if(image == null)
                throw new IncorrectUrlException();

            var newFileName = GetNewFileName(image.FileName);
            var filePath = Path.Combine(GetFileFolder(apiPath, login), newFileName);
            await SaveFile(login, filePath, image.FileName, image.Description, image.Content);

            //просто фронтендеры очень любят одинаковые форматы, и чтобы не обижать Иделюшку мы отадем это...
            return new [] { new OutputFileModel {FileName = newFileName, FilePath = filePath, Description = image.Description}};
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