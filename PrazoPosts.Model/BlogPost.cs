using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PrazoPosts.Model
{
    public class BlogPost
    {
        [BsonId]
        public ObjectId Id { get; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Content { get; set; }
        public string CoverUrl { get; set; }
    }
}
