using System;
using MongoDB.Driver;
using PrazoPosts.Model;
using PrazoPosts.Repository.Core;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Repository
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public override string CollectionName => "Authors";

        public AuthorRepository(IMongoDatabase mongoDb) : base(mongoDb)
        {
        }

    }
}
