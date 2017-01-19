namespace Domain.Services.Implimetations
{
    using System.IO;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class FileService : IFileService
    {
        public async Task Save(string fileName, string folder, byte[] content)
        {
            if (File.Exists(folder) == false)
                Directory.CreateDirectory(folder);

            using (var fileStream = File.Create(Path.Combine(folder, fileName)))
                await fileStream.WriteAsync(content, 0, content.Length);
        }
    }
}