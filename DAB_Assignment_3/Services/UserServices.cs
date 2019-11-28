using System;
using System.Collections.Generic;
using System.Text;
using DAB_Assignment_3.Models;
using MongoDB.Driver;

namespace DAB_Assignment_3.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Post> _posts;
        private readonly IMongoCollection<Circle> _circle;

        public UserServices()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _users = database.GetCollection<User>("Users");
            _posts = database.GetCollection<Post>("Posts");
            _circle = database.GetCollection<Circle>("Circles");
        }

        //Create a user
        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        //Find all?????????????????
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        //Find by UserId (Won't be good - dont know the Id's?)
        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        //public List<Post> GetFeed()
        //{
        //    //Find user from who you want to see feed
        //    var user = _users.Find(user)

        //    var posts = _posts.Find(post =>
        //        post.AuthorId == User.)
        //    return posts;//_posts.Find(post => true).ToList();
        //}
    }
}
