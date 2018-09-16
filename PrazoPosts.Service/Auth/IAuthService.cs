using PrazoPosts.Dto;

namespace PrazoPosts.Service.Auth
{
    public interface IAuthService
    {
        TokenDTO Authenticate(AuthDTO authData);
    }
}