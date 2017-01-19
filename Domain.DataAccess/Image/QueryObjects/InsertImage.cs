namespace Domain.DataAccess.Image.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class InsertImage
    {
        public static QueryObject New(string filename, int userId)
        {
            return new QueryObject(@"INSERT INTO Images (filename, userid) VALUES(@Filename, @UserId)", new { Filename = filename, UserId = userId});
        }
    }
}