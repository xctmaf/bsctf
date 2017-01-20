namespace Domain.DataAccess.Image.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class SelectFile
    {
        public static QueryObject ByUserLogin(string login)
        {
            return new QueryObject(@"SELECT 
	FileName
	, FilePath
	, Description
	, Images.Id Id
FROM
	Images 
	join Users on Users.Id = Images.UserId
WHERE 
	Users.Login = @Login", new {Login = login });
        }

        public static QueryObject ByDescription(string term)
        {
            return new QueryObject(@"SELECT 
	FileName
	, FilePath
	, Description
	, Id Id
FROM
	Images 
WHERE 
	Description like '%" + term + "%'");
        }
    }
}