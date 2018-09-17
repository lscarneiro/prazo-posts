using System;
using PrazoPosts.Dto;

namespace PrazoPosts.Service.Users
{
    public interface IUserService
    {
        TokenDTO RegisterUser(UserDTO userData);
        UserDTO GetUser(string _id);
    }
}
