namespace Domain.Entities
{
    public class UserFile : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
    }
}