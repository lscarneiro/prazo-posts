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
        void Update(string _id, T model);
        void Delete(string _id);
        T GetById(string _id);
        T GetByUserIdAndId(string UserId, string _id);
    }
}
