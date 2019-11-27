using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAB_Assignment_3.Models
{
    public class Post
    {
        public Post()
        {
            CommentsOnPost = new List<string>();
            PostCreationTime = DateTime.Now;
            PostedInCircle = "";
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        [BsonElement("ContentType")] 
        public string ContentType { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }
        //Dictionary?????? Binding Type and content together?

        [BsonElement("PostedInCircle")]
        public string PostedInCircle { get; set; }

        [BsonElement("AuthorOfPost")]
        public string AuthorOfPost { get; set; }

        [BsonElement("PostCreationTime")]
        public DateTime PostCreationTime { get; set; }

        [BsonElement("CommentsOnPost")]
        public List<string> CommentsOnPost { get; set; }
    }
}
