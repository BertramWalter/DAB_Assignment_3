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

            for (int i = 0; i < 10; i++)
            {
                string gender;
                if (rand.Next(1) == 1)
                    gender = "M";
                else
                    gender = "F";
                var user = new User("Name_"+i.ToString(),i+10,gender);
            }
        }

        public static void InsertCommentDummyData(IMongoDatabase database)
        {
            var comments = database.GetCollection<Comment>("Comments");
            var comment1 = new Comment(postid:"1",authorid: "a1", authorname:"Per Thorsen",commentstring:"Hvor er du?",DateTime.Now );
            var comment2 = new Comment(postid: "1", authorid: "a2", authorname: "Poul E", commentstring: "WebAPI load", DateTime.Now);
            var comment3 = new Comment(postid: "3", authorid: "a3", authorname: "Henrik", commentstring: "MongoDb virker ikke", DateTime.Now);
            var comment4 = new Comment(postid: "3", authorid: "a3", authorname: "Henrik", commentstring: "Nu virker det!", DateTime.Now);
            var comment5 = new Comment(postid: "4", authorid: "a4", authorname: "Søren H", commentstring: "Giver det mening??", DateTime.Now);
            var comment6 = new Comment(postid: "5", authorid: "a5", authorname: "Lars M", commentstring: "Nu er jeg vaagen", DateTime.Now);
            var comment7 = new Comment(postid: "5", authorid: "a6", authorname: "Henrik O", commentstring: "Du kan bare komme forbi kontoret", DateTime.Now);
            var comment8 = new Comment(postid: "6", authorid: "a7", authorname: "Torben", commentstring: "Jeg tager papir med", DateTime.Now);
            var comment9 = new Comment(postid: "7", authorid: "a8", authorname: "Kim", commentstring: "Kravsspec og review", DateTime.Now);
            var comment10 = new Comment(postid: "7", authorid: "a9", authorname: "Frankster", commentstring: "ehhhmm", DateTime.Now);
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
