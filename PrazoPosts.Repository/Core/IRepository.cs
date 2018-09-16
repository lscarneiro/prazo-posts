using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace PrazoPosts.Repository.Core
{
    public interface IRepository<T>
    {
        string CollectionName { get; }
        IEnumerable<T> GetAll(FilterDefinition<T> filter = null);
        T GetByFilter(FilterDefinition<T> filter);
        void Insert(T model);
        T GetById(string _id);
    }
}
