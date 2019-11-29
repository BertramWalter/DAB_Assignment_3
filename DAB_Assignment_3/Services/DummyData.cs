using System;
using System.Collections.Generic;
using System.Text;
using DAB_Assignment_3.Models;
using MongoDB.Driver;

namespace DAB_Assignment_3.Services
{
    public static class DummyData
    {
        public static void InsertDummyData(IMongoDatabase database)
        {
            var users = database.GetCollection<User>("Users");
            var posts = database.GetCollection<Post>("Posts");
            var comments = database.GetCollection<Comment>("Comments");
            var circles = database.GetCollection<Circle>("Circles");

            var rand = new Random();
            var usersList = new List<User>();

            for (int i = 0; i < 10; i++)
            {
                string gender;
                gender = rand.Next(1) == 1 ? "M" : "F";
                var user = new User("Name_"+i.ToString(),i+10,gender);
                users.InsertOne(user);
            }

            //Dummy Posts
            foreach (var user in usersList)
            {
                TextPost textPost = new TextPost();
                DataPost dataPost = new DataPost();

                textPost.AuthorId = user.Id;
                dataPost.AuthorId = user.Id;

                textPost.AuthorName = user.Name;
                dataPost.AuthorName = user.Name;

                bool randPublicPost = rand.Next(1) == 1 ? true : false;

                textPost.IsPublic = randPublicPost;
                dataPost.IsPublic = randPublicPost;

                string publicPost = randPublicPost == true ? "public" : "private";

                textPost.Text = $"My name is {textPost.AuthorName} and my ID is {textPost.AuthorId}. This is a {publicPost} TEXTPOST.";
                dataPost.Data = $"Audio: 'My name is {dataPost.AuthorName} and my ID is {dataPost.AuthorId}. This is a {publicPost} DATAPOST.'";

                if (randPublicPost == true)
                {
                    foreach (var id in user.BlockId)
                    {
                        textPost.BlockedAllowedUserId.Add(id);
                        dataPost.BlockedAllowedUserId.Add(id);
                    }
                }
                else
                {
                    for (int i = 0; i < user.CircleId.Count; i++)
                    {
                        var c = circles.Find<Circle>(c => c.CircleId == user.CircleId[i]).FirstOrDefault();
                        textPost.BlockedAllowedUserId = c.UserIds;
                        dataPost.BlockedAllowedUserId = c.UserIds;
                    }
                }

                user.UserPostsId.Add(textPost.PostId);
                user.UserPostsId.Add(dataPost.PostId);

                posts.InsertOne(textPost);
                posts.InsertOne(dataPost);
            }
        }
    }
}
