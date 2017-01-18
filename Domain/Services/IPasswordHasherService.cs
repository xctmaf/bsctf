namespace Domain.Services
{
    public interface IPasswordHasherService
    {
        string GetHashedPassword(string password);
    }
}