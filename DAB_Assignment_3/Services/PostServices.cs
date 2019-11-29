using DAB_Assignment_3.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.Services
{
    public class PostServices
    {
        //private IMongoCollection<Post> _textPosts;
        //private IMongoCollection<Post> _dataPosts;
        private IMongoCollection<User> _users;
        private IMongoCollection<Circle> _circles;
        private IMongoCollection<Post> _posts;

        public PostServices()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            //_dataPosts = database.GetCollection<Post>("DataPosts");
            //_textPosts = database.GetCollection<Post>("TextPosts");
            _posts = database.GetCollection<Post>("Post");

            _users = database.GetCollection<User>("Users");
            _circles = database.GetCollection<Circle>("Circles");
        }


        public void CreatePost()
        {
            User user = null;
            do
            {
                Console.Write("Write owner ID: ");
                string owner_id = Console.ReadLine();

                //Check if userId exists in Database
                user = _users.Find(x => x.Id == owner_id).FirstOrDefault();
                if (user == null)
                {
                    Console.Write("Invalid id, try again. ");
                }

            } while (user == null);

            Console.WriteLine("You have two options: Textpost or datapost.");
            ConsoleKeyInfo key;
            do
            {
                Console.WriteLine("Press 'T' for Textpost, 'D' for Datapost");
                key = Console.ReadKey(true);

            } while (key.Key != ConsoleKey.D && key.Key != ConsoleKey.T);

            if(key.Key == ConsoleKey.D)
            {
                CreateDataPost(user);
            }
            else if(key.Key == ConsoleKey.T)
            {
                CreateTextPost(user);
            }
        }


        private void CreateDataPost(User user)
        {

            DataPost post = new DataPost();
            post.AuthorId = user.Id;
            post.AuthorName = user.Name;

            Console.WriteLine($"\nHello {post.AuthorName}! You are making a datapost.");

            Console.WriteLine("Do you want your post to be public or not?");
            ConsoleKeyInfo key;

            do
            {
                Console.WriteLine("PRESS 'Y' for YES or 'N' for NO");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    post.IsPublic = true;
                    Console.WriteLine("Post status: Public\n");
                }
                else if (key.Key == ConsoleKey.N)
                {
                    post.IsPublic = false;
                    Console.WriteLine("Post status: Private\n");
                }
            } while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

            //Write data to post//
            Console.WriteLine("Write data, exit with 'Enter': ");
            post.Data = Console.ReadLine();
            Console.WriteLine();
            // ===================== //

            if (post.IsPublic == true)
            {
                foreach (var id in user.BlockId)
                {
                    post.BlockedAllowedUserId.Add(id);
                }

                Console.WriteLine("Public post added");
            }
            else
            {
                Console.WriteLine("Which circle(s) do you want to post to?\nHere is your list of circles: ");
                foreach (var id in user.CircleId)
                {
                    var c = _circles.Find<Circle>(c => c.CircleId == id).FirstOrDefault();
                    Console.WriteLine($"Circle id: {c.CircleId}, Cicle name: {c.Name}");
                }

                Console.WriteLine("\nWrite the id of the circles, you want to include, one at a time.");

                do
                {
                    Console.Write("Write id: ");
                    string circleIdToInclude = Console.ReadLine();

                    if (user.CircleName.Contains(circleIdToInclude))
                    {
                        post.BlockedAllowedUserId.Add(circleIdToInclude);
                    }
                    else
                    {
                        Console.WriteLine("Id does not exist");
                    }

                    do
                    {
                        Console.WriteLine("Pess 'A' to add another circle");
                        Console.WriteLine("Pess 'C' to Continue");

                        key = Console.ReadKey(true);

                    } while (key.Key != ConsoleKey.A && key.Key != ConsoleKey.C);

                } while (key.Key == ConsoleKey.A);

                Console.WriteLine("Private post added to circles");
            }

            user.UserPostsId.Add(post.PostId);
            _posts.InsertOne(post);
        }

        public void CreateTextPost(User user)
        {
            
            TextPost post = new TextPost();
            post.AuthorId = user.Id;
            post.AuthorName = user.Name;

            Console.WriteLine($"\nHello {post.AuthorName}! You are making a textpost.");

            Console.WriteLine("Do you want your post to be public or not?\n");
            ConsoleKeyInfo key;

            do
            {
                Console.WriteLine("PRESS 'Y' for YES or 'N' for NO");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    post.IsPublic = true;
                    Console.WriteLine("Post status: Public\n");
                }
                else if (key.Key == ConsoleKey.N)
                {
                    post.IsPublic = false;
                    Console.WriteLine("Post status: Private\n");
                }
            }while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

            //Write text to post//
            Console.WriteLine("Write post, exit with 'Enter': ");
            post.Text = Console.ReadLine();
            Console.WriteLine();
            // ===================== //

            if (post.IsPublic == true)
            {
                foreach (var id in user.BlockId)
                {
                    post.BlockedAllowedUserId.Add(id);
                }

                Console.WriteLine("Public post added");
            }
            else
            {
                Console.WriteLine("Which circle(s) do you want to post to?\nHere is your list of circles: ");
                foreach (var id in user.CircleId)
                {
                    var c = _circles.Find<Circle>(c => c.CircleId == id).FirstOrDefault();
                    Console.WriteLine($"Circle id: {c.CircleId}, Cicle name: {c.Name}");
                }
                
                Console.WriteLine("\nWrite the id of the circles, you want to include, one at a time.");

                do
                {
                    Console.Write("Write id: ");
                    string circleIdToInclude = Console.ReadLine();

                    if (user.CircleId.Contains(circleIdToInclude))
                    {
                        var c = _circles.Find<Circle>(c => c.CircleId == circleIdToInclude).FirstOrDefault();
                        post.BlockedAllowedUserId = c.UserIds;
                    }
                    else
                    {
                        Console.WriteLine("Id does not exist");
                    }

                    do
                    {
                        Console.WriteLine("Pess 'A' to add another circle");
                        Console.WriteLine("Pess 'C' to Continue");

                        key = Console.ReadKey(true);

                    } while (key.Key != ConsoleKey.A && key.Key != ConsoleKey.C);

                } while (key.Key == ConsoleKey.A);

                Console.WriteLine("Private post added to circles");
            }

            user.UserPostsId.Add(post.PostId);
            _posts.InsertOne(post);
        }
    }
}
