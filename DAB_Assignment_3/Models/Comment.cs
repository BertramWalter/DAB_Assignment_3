using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.Models
{
    public class Comment
    {
        public Comment(string postid, string authorid, string authorname, string commentstring, DateTime DateTime)
        {
            PostId = postid;
            AuthorId = authorid;
            AuthorName = authorname;
            CommentString = commentstring;
            DateTime = new DateTime();

        }

        [BsonElement("PostId")]
        public string PostId { get; set; }

        [BsonElement("AuthorId")]
        public string AuthorId { get; set; }

        [BsonElement("AuthorName")]
        public string AuthorName { get; set; }

        [BsonElement("CommentString")]
        public string CommentString { get; set; }

        [BsonElement("DateTime")] 
        public DateTime DateTime { get; set; }
    }
}
