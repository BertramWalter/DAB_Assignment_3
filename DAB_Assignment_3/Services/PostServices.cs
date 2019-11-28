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

        public void CreateTextPost()
        {
            Console.WriteLine("Create new post:");
            Console.Write("Write owner ID: ");
            string owner_id = Console.ReadLine();
            
            //Check if userId exists in Database
            var user = _users.Find(x => x.Id == owner_id).FirstOrDefault();
            if (user == null)
            {
                Console.Write("Invalid id, try again: ");
            }

            TextPost post = new TextPost();
            post.AuthorId = user.Id;
            post.AuthorName = user.Name;

            Console.WriteLine($"Hello {post.AuthorName}!");

            Console.WriteLine("Do you want your post to be public or not?");
            ConsoleKeyInfo key;
            do
            {

                Console.Write("PRESS 'Y' for YES or 'N' for NO: ");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    post.IsPublic = true;
                    Console.WriteLine("Post status: Public");
                }
                else if (key.Key == ConsoleKey.N)
                {
                    post.IsPublic = false;
                    Console.WriteLine("Post status: Private");
                }
            }while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

            if(post.IsPublic == true)
            {
                foreach (var id in user.BlockId)
                {
                    post.BlockedAllowedUserId.Add(id);
                }              
            }
            else
            {
                Console.WriteLine("Which circle(s) do you want to post to?\nHere is your list of circles: ");
                int i = 0;
                foreach (var id in user.CircleId)
                {
                    var c = _circles.Find<Circle>(id);

                    //Console.WriteLine(name);

                }
                
                Console.WriteLine("\nWrite the name of the circles, you want to include, one at a time.");

                Console.WriteLine("Write name: ");
                string name2 = Console.ReadLine();

                if (user.CircleName.Contains(name2))
                {
                    
                }





                do
                {
                    

                    var circle = _circles.Find(c => c.CircleId == post.CircleId);

                    if (user.CircleName == name

                            _circles
                    

                    key2 =
                    if (user.CircleId.Find(())
                    BlockedAllowedUserId

                    Console.WriteLine("Do you want to add more circles? [Y/S]");

                } while (key2.Key != ConsoleKey.N);
                

                ConsoleKeyInfo key2;
                



            }


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
