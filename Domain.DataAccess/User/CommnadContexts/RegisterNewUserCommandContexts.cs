namespace Domain.DataAccess.User.CommnadContexts
{
    using ByndyuSoft.Infrastructure.Domain.Commands;

    public class RegisterNewUserCommandContexts : ICommandContext
    {
        public RegisterNewUserCommandContexts(string login, string password, string username, string salt)
        {
            Username = username;
            Salt = salt;
            Login = login;
            Password = password;
        }

        public string Username { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
    }
}