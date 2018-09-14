using System;
using System.Collections.Generic;

namespace PrazoPosts.Repository.Core
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Insert(T model);
    }
}
