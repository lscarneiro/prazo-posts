using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PrazoPosts.Model
{
    public class Author
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
