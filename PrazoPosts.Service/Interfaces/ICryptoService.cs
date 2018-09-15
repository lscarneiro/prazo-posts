namespace PrazoPosts.Service.Interfaces
{
    public interface ICryptoService
    {
        string Encrypt(string password);
        bool VerifyPassword(string hash, string password);
    }
}