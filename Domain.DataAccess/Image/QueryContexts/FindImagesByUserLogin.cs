namespace Domain.DataAccess.Image.QueryContexts
{
    using ByndyuSoft.Infrastructure.Domain;
    public class FindImagesByUserLogin : ICriterion
    {
        public FindImagesByUserLogin(string login)
        {
            Login = login;
        }

        public string Login { get; private set; }
    }
}