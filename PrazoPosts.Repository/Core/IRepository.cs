using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace PrazoPosts.Repository.Core
{
    public interface IRepository<T>
    {
        string CollectionName { get; }
        IList<T> GetAll(FilterDefinition<T> filter = null);
        void Insert(T model);
        T GetById(string _id);
    }
}
