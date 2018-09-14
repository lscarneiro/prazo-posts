using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PrazoPosts.Model
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; }
        public string Name { get; set; }
        public string Login { get; set; }
    }
}
