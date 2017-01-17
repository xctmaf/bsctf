namespace Web.Application.Handlers.User
{
    using Models.User;

    public interface IUserHandler
    {
        UserModel[] GetInfo();
    }
}