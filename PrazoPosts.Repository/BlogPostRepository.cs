using System;
using MongoDB.Driver;
using PrazoPosts.Model;
using PrazoPosts.Repository.Core;
using PrazoPosts.Repository.Interfaces;

namespace PrazoPosts.Repository
{
    public class BlogPostRepository : BaseRepository<BlogPost>, IBlogPostRepository
    {
        public override string CollectionName => "BlogPosts";

        public BlogPostRepository(IMongoDatabase mongoDb) : base(mongoDb)
        {
        }
    }
}
