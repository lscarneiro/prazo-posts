using System;
using MongoDB.Driver;
using PrazoPosts.Model;
using PrazoPosts.Repository.Core;

namespace PrazoPosts.Repository
{
    public class PostRepository : BaseRepository<BlogPost>
    {
        public override string CollectionName => "BlogPosts";

        public PostRepository(IMongoDatabase mongoDb) : base(mongoDb)
        {
        }
    }
}
