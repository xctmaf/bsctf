namespace Domain.DataAccess.Image.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class InsertImage
    {
        public static QueryObject New(string filename, string filePath, int userId)
        {
            return new QueryObject(@"INSERT INTO Images (filename, filepath, userid) VALUES(@FileName,  @FilePath, @UserId)", new { Filename = filename, UserId = userId, FilePath = filePath });
        }
    }
}