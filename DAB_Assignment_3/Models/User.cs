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
            UserCircles = new List<string>();
            Following = new List<string>();
            FollowedBy = new List<string>();
            BlockedUsers = new List<string>();
            BlocksUsers = new List<string>();
            UserCircles.Add("");
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("Name")] 
        public string Name { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Gender")] 
        public string Gender { get; set; }

        [BsonElement("UserCircles")] 
        public List<string> UserCircles { get; set; }

        [BsonElement("Following")]
        public List<string> Following { get; set; }

        [BsonElement("FollowedBy")]
        public List<string> FollowedBy { get; set; }

        [BsonElement("BlocksUsers")]
        public List<string> BlocksUsers { get; set; }

        [BsonElement("BlockedByUsers")]
        public List<string> BlockedUsers  { get; set; }
    }
}
