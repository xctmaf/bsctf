namespace Domain.DataAccess.User.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class InsertUser
    {
        public static QueryObject New(string login, string password, string username)
        {
            return new QueryObject(@"INSERT INTO USERS(login, password, username) VALUES (@Login, @Password, @Username)",
                                   new {Login = login, Password = password, Username = username});
        }
    }
}