namespace Web.Application.Handlers.User
{
    using Models.User.Output;

    public interface IUserHandler
    {
        UserModel[] GetInfo();
        void Register(string login, string password, string username);
        UserModel Login(string login, string password);
    }
}