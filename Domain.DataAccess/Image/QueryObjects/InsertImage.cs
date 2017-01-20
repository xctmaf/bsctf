namespace Domain.DataAccess.Image.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class InsertImage
    {
        public static QueryObject New(string filename, string filePath, int userId, string descript)
        {
            return new QueryObject(@"INSERT INTO Images (filename, filepath, userid, description) VALUES(@FileName, @FilePath, @UserId, @Description)",
                                   new {Filename = filename, UserId = userId, FilePath = filePath, Description = descript});
        }
    }
}