using System;
using PrazoPosts.Model;
using PrazoPosts.Repository.Core;

namespace PrazoPosts.Repository.Interfaces
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        long PostCountByAuthor(string authorId);
    }
}
