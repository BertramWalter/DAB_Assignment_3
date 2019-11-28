﻿using System;
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
        }

        //Create a user
        public void CreateUser()
        {
            Console.Write("Enter new username: ");
            string name = Console.ReadLine();
            Console.Write("\nEnter users age: ");
            int age = Console.Read();
            Console.Write("\nEnter gender: ");
            string gender = Console.ReadLine();

            User user = new User(name, age, gender);

            _users.InsertOne(user);

            Console.WriteLine($"{gender}-user {name} (age: {age}) has been successfully created");
        }

        public void BlockUser(string userid, string blockUserId)
        {
            var userWhoBlocks = _users.Find(findUser => findUser.Id == userid).FirstOrDefault();

            var userToBlock = _users.Find(findUser => findUser.Id == blockUserId).FirstOrDefault();

            if (userWhoBlocks == null || userToBlock == null)
            {
                Console.WriteLine("User or userToBlockId non-existent");
                return;
            }

            userWhoBlocks.BlockId.Add(userToBlock.Id);
        }


        public void UnblockUser(string userid, string blockedUserId)
        {
            var userWhoBlocks = _users.Find(user => 
                    user.Id == userid &&
                    user.BlockId.Contains(blockedUserId)).FirstOrDefault();

            if (userWhoBlocks == null)
            {
                Console.WriteLine($"UserID {userid} doesn't exist or doesn't block ID: {blockedUserId}");
                return;
            }

            userWhoBlocks.BlockId.Remove(blockedUserId);
            
            Console.WriteLine($"User {userWhoBlocks.Name} has unblocked user with ID: {blockedUserId}");
        }

        public void Follow(string userid, string userToFollow)
        {
            var user = _users.Find(user =>
                user.Id == userid).FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine("User doesn't exist");
                return;
            }

            user.FollowId.Add(userToFollow);
        }

        public void UnFollow(string userid, string userToUnfollow)
        {
            var user = _users.Find(user =>
                user.Id == userid &&
                user.FollowId.Contains(userToUnfollow)).FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine($"UserID {userid} doesn't exist or doesn't follow ID: {userToUnfollow}");
                return;
            }

            user.FollowId.Remove(userToUnfollow);

            Console.WriteLine($"User {user.Name} has unfollowed user with ID: {userToUnfollow}");
        }
        //Find all users
        public List<User> Get() =>
            _users.Find(user => true).ToList();

        //Find by UserId (Won't be good - dont know the Id's?)
        public User Get(string userid) =>
            _users.Find<User>(user => user.Id == userid).FirstOrDefault();

        //Get feed from er specific user
        public void GetFeed(string userid)
        {
            var user = _users.Find(findUser => findUser.Id == userid).FirstOrDefault();
            
            //Find user from whom, you want to see feed
            if (user == null)
            { 
               Console.WriteLine("User doesn't exist"); //throw new System.ArgumentException("User ID non-existent");
               return;
            }

            ////----------------------Arbejder med dette-----------------------//
            //////TEST HER
            //List<string> following = user.FollowId;
            ////Can I search posts from the "following" list?
            ////Get all id from the followed users - now see posts from these?
            ////---------------------------------------------------------------//


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


            foreach (var f in userFeed)
            {
                Console.WriteLine($"{f}");
            }
        }

        public void GetWall(string userid, string guestId)
        {
            var user = _users.Find(findUser => findUser.Id == userid).FirstOrDefault();
            var guest = _users.Find(findGuest => findGuest.Id == guestId).FirstOrDefault();

            if (user == null ||guest == null)
            {
                Console.WriteLine("User or guest doesnt exist");
                return;
            }

            //Checking if guest is blocked by user
            if (user.BlockId.Contains(guestId))
            {
                Console.WriteLine("Guest is blocked by user!");
                return;
            }

            var wall = _posts.Find(postsOnWall => 
                postsOnWall.AuthorId == user.Id &&
                user.FollowId.Contains(guestId))
                .SortByDescending(post => post.PostId).Limit(5).ToList();


            foreach (var wp in wall)
            {
                Console.WriteLine($"{wp}");
            }
        }


    }
}
