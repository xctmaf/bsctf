namespace Domain.Services
{
    public interface ISaltGenerator
    {
        string GetBase64Salt();
    }
}