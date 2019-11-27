using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment_3.Models
{
    public class Circle
    {
        public Circle()
        {
            UsersInCircle = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CircleId { get; set; }

        [BsonElement("NameOfCircle")]
        public string Name { get; set; }

        [BsonElement("UsersInCircle")]
        public List<string> UsersInCircle { get; set; }

    }
}
