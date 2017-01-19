namespace Web.Application.Models.Image.Input
{
    public class FileModel
    {
        public string Filename { get; set; }
        public byte[] Content { get; set; }
        public long? Size { get; set; }
    }
}