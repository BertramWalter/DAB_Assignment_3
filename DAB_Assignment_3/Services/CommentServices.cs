using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using MongoDB.Driver;
using DAB_Assignment_3.Models;

namespace DAB_Assignment_3.Services
{
    public class CommentServices
    {
        private IMongoCollection<Comment> _comments;
        private IMongoCollection<User> _users;
        private IMongoCollection<Circle> _circle;
        private IMongoCollection<Post> _post;

        public CommentServices()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _comments = database.GetCollection<Comment>("Comments");
            _users = database.GetCollection<User>("Users");
            _post = database.GetCollection<Post>("Posts");
            _circle = database.GetCollection<Circle>("Circles");
        }

        public void CreateComment( string post_id, string datetime ,string comment)
        {
            //if no posts available
            var post = _post.Find(x => x.PostId == post_id ).FirstOrDefault();
            if (post == null)
            {
                Console.WriteLine("Invalid post");
            }

             //   user = _users;


             DateTime Date = datetime;
            //comment.DateTime = DateTime.Parse(Date);

            Console.WriteLine("Type your comment: ");
            string commentstring = Console.ReadLine();

            //comment.AuthorId = authorid;
            //comment.CommentString = commentstring;
            
            
            
            //comment.DateTime = Console.WriteLine("{Date}");

            //Comment postcomment = new Comment(postid, authorid, authorname, commentstring, DateTime);

            //_comments.InsertOne(postcomment);

        }


    }
}
