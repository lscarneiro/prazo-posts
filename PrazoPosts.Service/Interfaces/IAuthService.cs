namespace PrazoPosts.Service.Interfaces
{
    public interface IAuthService
    {
        bool Authenticate(string email, string password);
    }
}