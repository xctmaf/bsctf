namespace Domain.DataAccess.User.QueryContexts
{
    using ByndyuSoft.Infrastructure.Domain;

    public class FindUserByLogin : ICriterion
    {
        public FindUserByLogin(string login)
        {
            Login = login;
        }

        public string Login { get; private set; }
    }
}