namespace Domain.DataAccess.User.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class InsertUser
    {
        public static QueryObject New(string login, string password, string username, string salt)
        {
            return new QueryObject(@"INSERT INTO USERS(login, password, username, salt) VALUES (@Login, @Password, @Username, @Salt)",
                                   new {Login = login, Password = password, Username = username, Salt = salt});
        }
    }
}