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
FROM
    Users
WHERE
    Login = '" + login +"'");
        }
    }
}