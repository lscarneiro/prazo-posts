namespace PrazoPosts.Service.Core
{
    public interface ICryptoService
    {
        string Encrypt(string password);
        bool VerifyPassword(string hash, string password);
    }
}