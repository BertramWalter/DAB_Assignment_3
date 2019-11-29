﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using DAB_Assignment_3.Models;
using Microsoft.VisualBasic;
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

            #region Users

            for (int i = 0; i < 10; i++)
            {
                var gender = rand.Next(1) == 1 ? "M" : "F";
                var user = new User("Name_" + i.ToString(), i + 10, gender);
                usersList.Add(user);
            }

            foreach (var u in usersList)
            {
                foreach (var un in usersList.Where(un => un != u))
                {
                    if (rand.Next(2) == 2)
                        u.BlockId.Add(un.Id);
                    else
                        u.FollowId.Add(un.Id);
                }

                users.InsertOne(u);
            }

            #endregion

            #region Circle
            int timeToAdd = 0;
            foreach (var u in usersList)
            {
                var c1 = new Circle(u.Name+"_C1",u.Id);
                var c2 = new Circle(u.Name + "_C2", u.Id);
                var c3 = new Circle(u.Name + "_C3", u.Id);
                foreach (var un in usersList.Where(un => un != u))
                {
                    if (rand.Next(2) == 2)
                        c1.UserIds.Add(un.Id);
                    if (rand.Next(2) == 2)
                        c2.UserIds.Add(un.Id);
                    if (rand.Next(2) == 2)
                        c3.UserIds.Add(un.Id);
                }
                circles.InsertOne(c1);
                circles.InsertOne(c2);
                circles.InsertOne(c3);
            }
            #endregion

            #region Post
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

                //Overwrite DateTime
                textPost.DateTime.AddHours(timeToAdd);
                dataPost.DateTime.AddHours(timeToAdd);
                timeToAdd++;

                posts.InsertOne(textPost);
                posts.InsertOne(dataPost);
            }

            #endregion

            InsertCommentDummyData(database);
        }

        public static void InsertCommentDummyData(IMongoDatabase database /*Post post*/)
        {
            var comments = database.GetCollection<Comment>("Comments");
            //var posts = database.GetCollection<Post>("Posts");
            //post1 = post.PostId;

            //var comment11 = new Comment(postid:$"{post.PostId}",authorid: "a11", authorname:"Peter",commentstring:"Halvvejs", new DateTime(year: 2019, month: 1, day: 21, hour: 11, minute: 1, second: 1));
            var comment1 = new Comment(postid:"1", authorid: "a1", authorname: "Per Thorsen", commentstring: "Hvor er du?", new DateTime(year: 2019, month: 11, day: 29, hour: 19, minute: 3, second: 43));
            var comment2 = new Comment(postid: "1", authorid: "a2", authorname: "Poul E", commentstring: "WebAPI load", new DateTime(year: 2019, month: 11, day: 29, hour: 11, minute: 23, second: 23));
            var comment3 = new Comment(postid: "3", authorid: "a3", authorname: "Henrik", commentstring: "MongoDb virker ikke", new DateTime(year: 2019, month: 11, day: 29, hour: 11, minute: 23, second: 28));
            var comment4 = new Comment(postid: "3", authorid: "a3", authorname: "Henrik", commentstring: "Nu virker det!", new DateTime(year: 2019, month: 11, day: 29, hour: 11, minute: 53, second: 16));
            var comment5 = new Comment(postid: "4", authorid: "a4", authorname: "Søren H", commentstring: "Giver det mening??", new DateTime(year: 2019, month: 11, day: 29, hour: 7, minute: 23, second: 23));
            var comment6 = new Comment(postid: "5", authorid: "a5", authorname: "Lars M", commentstring: "Nu er jeg vaagen", new DateTime(year: 2019, month: 11, day: 29, hour: 10, minute: 23, second: 23));
            var comment7 = new Comment(postid: "5", authorid: "a6", authorname: "Henrik O", commentstring: "Du kan bare komme forbi kontoret", new DateTime(year: 2019, month: 11, day: 30, hour: 8, minute: 23, second: 23));
            var comment8 = new Comment(postid: "6", authorid: "a7", authorname: "Torben", commentstring: "Jeg tager papir med", new DateTime(year: 2019, month: 11, day: 18, hour: 4, minute: 40, second: 33));
            var comment9 = new Comment(postid: "7", authorid: "a8", authorname: "Kim", commentstring: "Kravsspec og review", new DateTime(year: 2019, month: 11, day: 29, hour: 10, minute: 20, second: 0));
            var comment10 = new Comment(postid: "7", authorid: "a9", authorname: "Frankster", commentstring: "ehhhmm", new DateTime(year:2019,month:11,day:30,hour:10,minute:21,second:1));
            comments.InsertOne(comment1);
            comments.InsertOne(comment2);
            comments.InsertOne(comment3);
            comments.InsertOne(comment4);
            comments.InsertOne(comment5);
            comments.InsertOne(comment6);
            comments.InsertOne(comment7);
            comments.InsertOne(comment8);
            comments.InsertOne(comment9);
            comments.InsertOne(comment10);
            //comments.InsertOne(comment11);

            //comments.InsertMany(comment1,comment2, comment3,comment4,comment5,comment6,comment7,comment8,comment9,comment10]);
            /*comments.InsertMany(
                [ < comment1 1 > , < comment2 2 >, ... ], 
            {
                writeConcern: < comments >,
                ordered: < boolean >
            }
            )*/
        }

    }
}
