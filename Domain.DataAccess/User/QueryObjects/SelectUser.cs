namespace Domain.DataAccess.User.QueryObjects
{
    using ByndyuSoft.Infrastructure.Dapper;

    public static class SelectUser
    {
        public static QueryObject All()
        {
            return new QueryObject(@"
SELECT
    Login
    , Password
    , Username
    , Id
    , Salt
FROM
    Users");
        }

        public static QueryObject ByLogin(string login)
        {
            return new QueryObject(@"
SELECT
    Login
    , Password
    , Username
    , Id
    , Salt
FROM
    Users
WHERE
    Login = @Login" , new {Login = login});
        }
    }
}