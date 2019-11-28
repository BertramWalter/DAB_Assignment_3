using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment_3.Models
{
    public class User
    {
        public User()
        {
            FollowId = new List<string>();
            BlockId = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")] 
        public string Name { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Gender")] 
        public string Gender { get; set; }

        [BsonElement("FollowId")]
        public List<string> FollowId { get; set; }

        [BsonElement("BlockId")]
        public List<string> BlockId { get; set; }
    }
}
