using DAB_Assignment_3.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.Services
{
    public class PostServices
    {
        private IMongoCollection<Post> _posts;
        private IMongoCollection<Users> _users;
        private IMongoCollection<Circle> _circle;

        public PostServices()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _posts = database.GetCollection<Post>("Posts");
        }

        public void CreatePost(Post post)
        {
           

        }


    }
}
