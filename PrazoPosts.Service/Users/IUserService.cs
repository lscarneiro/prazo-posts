using System;
using PrazoPosts.Dto;

namespace PrazoPosts.Service.Users
{
    public interface IUserService
    {
        void RegisterUser(UserDTO userData);
        UserDTO GetUser(string _id);
    }
}
