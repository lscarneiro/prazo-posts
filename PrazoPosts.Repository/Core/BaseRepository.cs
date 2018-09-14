using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PrazoPosts.Repository.Core
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        protected IMongoDatabase _mongoDb;
        protected IMongoCollection<T> _collection;

        protected abstract string CollectionName { get; }

        protected BaseRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb ?? throw new ArgumentNullException(nameof(mongoDb));
            _collection = _mongoDb.GetCollection<T>(CollectionName);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _collection.Find(doc => true).ToList();
        }

        public virtual void Insert(T model)
        {
            _collection.InsertOne(model);
        }

        public virtual void Delete(string _id)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", _id));
        }
    }
}
