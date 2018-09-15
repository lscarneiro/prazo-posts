using PrazoPosts.Model;
using PrazoPosts.Repository.Core;

namespace PrazoPosts.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}