using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PrazoPosts.Model
{
    public class BlogPost
    {
        [BsonId]
        public ObjectId Id { get; }
        public string UserId { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CoverUrl { get; set; }
    }
}
