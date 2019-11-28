using System;
using System.Collections.Generic;
using System.Text;
using DAB_Assignment_3.Models;
using MongoDB.Bson.Serialization.IdGenerators;
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

        //Find all users
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        //Find by UserId (Won't be good - dont know the Id's?)
        public User Get(string userid) =>
            _users.Find<User>(user => user.Id == userid).FirstOrDefault();

        //Get feed from er specific user
        public List<Post> GetFeed(string userid)
        {
            var user = _users.Find(findUser => findUser.Id == userid).FirstOrDefault();
            
            //Find user from whom, you want to see feed
            if (user == null)
            { 
                throw new System.ArgumentException("User ID non-existent");
            }

            var userFeed = _posts.Find(post => 
                    //Checking if post is private or public, if the use is blocked
                    //or not - and if the user follows the author of post
                    (post.IsPublic == false &&
                    post.BlockedAllowedUserId.Contains(userid) &&
                    user.FollowId.Contains(post.AuthorId)) ||
                    
                    //If the post is public - we're checking if the user
                    //is in the "blockedAllowedUserId list (if the user is, post wont show in feed)
                    (post.IsPublic &&
                    !post.BlockedAllowedUserId.Contains(userid) &&
                    user.FollowId.Contains(post.AuthorId)
                    ))
                .SortByDescending(post => post.PostId).Limit(5).ToList();
            return userFeed;
        }

        //public List<Post> GetWall(string userid, string guestId)
        //{

        //}
    }
}
