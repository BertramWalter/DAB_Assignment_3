using DAB_Assignment_3.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.Services
{
    public class PostServices
    {
        private IMongoCollection<Post> _textPosts;
        private IMongoCollection<Post> _dataPosts;
        private IMongoCollection<User> _users;
        private IMongoCollection<Circle> _circles;

        public PostServices()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _dataPosts = database.GetCollection<Post>("DataPosts");
            _textPosts = database.GetCollection<Post>("TextPosts");
            _users = database.GetCollection<User>("Users");
            _circles = database.GetCollection<Circle>("Circles");
        }

        public void CreateTextPost(string owner_id, string content, string circles)
        {
            Console.WriteLine("Create new post:");
            Console.WriteLine()
            //TextPost post = new TextPost();
            //Check if user exists in database
            var user = _users.Find(x => x.UserId == owner_id).FirstOrDefault();
            if (user == null)
            {
                Console.WriteLine("Invalid id");
            }

            post.AuthorName = user.Name;
            post.AuthorId = user.UserId;



        //Check if circle exist
        //var circle = _circle.Find(c => c.Id == post.CircleId);
        //    if (post.CircleId != "" && circle == null)
        //    {
        //        return BadRequest("Invalid circleId");
        //    }


        //    _posts.InsertOne(post);
        //    return Ok(post);


        }


    }
}
