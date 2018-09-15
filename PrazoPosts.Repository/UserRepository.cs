using System;
using MongoDB.Driver;
using PrazoPosts.Model;
using PrazoPosts.Repository.Core;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public override string CollectionName => "Users";
        public UserRepository(IMongoDatabase mongoDb) : base(mongoDb)
        {
        }


        public User GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return _collection.Find(filter).FirstOrDefault();
        }
    }
}
