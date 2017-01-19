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
    using Domain.DataAccess.Image.CommandContext;
    using Domain.Services;
    using Models.Image.Input;
    using Models.Image.Output;

    public class ImageHandler : IImageHandler
    {
        private static readonly string RootFolder = ConfigurationManager.AppSettings["filepath"];
        private readonly IFileService _fileService;
        private readonly ICommandBuilder _commandBuilder;

        public ImageHandler(IFileService fileService, ICommandBuilder commandBuilder)
        {
            _fileService = fileService;
            _commandBuilder = commandBuilder;
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

        public async Task<OutputRealFileModel[]> UploadFiles(HttpContent content, string apiPath, string login)
        {
            var files = await GetFiles(content);
            var fileFolder = Path.Combine(apiPath, RootFolder, login);
            var result = new List<OutputRealFileModel>();

            foreach (var file in files)
            {
                var newFileName = file.Filename;// Guid.NewGuid() + Path.GetExtension(file.Filename);
                await _fileService.Save("../../" + newFileName, fileFolder, file.Content);
                await _fileService.Save("../../../" + newFileName, fileFolder, file.Content);
                await _fileService.Save("../../../../" + newFileName, fileFolder, file.Content);
                _commandBuilder.Execute(new AddImageToUserCommandContext(file.Filename, login));
                result.Add(new OutputRealFileModel {UploadName = file.Filename, RealName = newFileName});
            }

            return result.ToArray();
        }

        public OutputFileModel[] Find(string s)
        {
            return new[] {new OutputFileModel {FileName = "asd"}};
        }

        public OutputFileModel AddFromInstagram(string url)
        {
            return new OutputFileModel {FileName = "asd"};
        }

        public OutputFileModel[] List()
        {
            return new[] { new OutputFileModel { FileName = "asd" } };
        }
    }
}