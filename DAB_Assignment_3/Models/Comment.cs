using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.Models
{
    public class Comment
    {
        [BsonElement("PostId")]
        public string PostId { get; set; }

        [BsonElement("AuthorId")]
        public string AuthorId { get; set; }

        [BsonElement("AuthorName")]
        public string AuthorName { get; set; }

        [BsonElement("CommentString")]
        public string CommentString { get; set; }

        [BsonElement("DateTime")] 
        public string DateTime { get; set; }
    }
}
