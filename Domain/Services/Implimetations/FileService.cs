namespace Domain.Services.Implimetations
{
    using System.IO;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class FileService : IFileService
    {
        public async Task Save(string fileName, byte[] content)
        {
            var folder = Path.GetDirectoryName(fileName);
            if (Directory.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            using (var fileStream = File.Create(fileName))
                await fileStream.WriteAsync(content, 0, content.Length);
        }
    }
}