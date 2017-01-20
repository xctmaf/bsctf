namespace Domain.ValueObject
{
    public class FileInfo
    {
        public string FileName { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
    }
}