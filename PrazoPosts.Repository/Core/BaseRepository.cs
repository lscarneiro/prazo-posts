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

        public abstract string CollectionName { get; }

        protected BaseRepository(IMongoDatabase mongoDb)
        {
            _mongoDb = mongoDb ?? throw new ArgumentNullException(nameof(mongoDb));
            _collection = _mongoDb.GetCollection<T>(CollectionName);
        }

        public virtual IEnumerable<T> GetAll(FilterDefinition<T> filter = null)
        {
            return filter == null ? _collection.Find(doc => true).ToEnumerable() : _collection.Find(filter).ToEnumerable();
        }

        public virtual void Insert(T model)
        {
            _collection.InsertOne(model);
        }
        public void Update(string _id, T model)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(_id));
            _collection.ReplaceOne(filter, model);
        }

        public virtual void Delete(string _id)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", ObjectId.Parse(_id)));
        }

        public T GetById(string _id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(_id));
            return _collection.Find(filter).FirstOrDefault();
        }
        public T GetByFilter(FilterDefinition<T> filter)
        {
            return _collection.Find(filter).FirstOrDefault();
        }
    }
}
