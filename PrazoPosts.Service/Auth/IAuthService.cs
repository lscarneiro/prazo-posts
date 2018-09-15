namespace PrazoPosts.Service.Auth
{
    public interface IAuthService
    {
        bool Authenticate(string email, string password);
    }
}