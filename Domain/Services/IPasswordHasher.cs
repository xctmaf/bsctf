namespace Domain.Services
{
    public interface IPasswordHasher
    {
        string GetHashedPassword(string password, string salt);
    }
}