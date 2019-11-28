using System;
using System.Collections.Generic;
using DAB_Assignment_3.Models;
using DAB_Assignment_3.Services;

namespace DAB_Assignment_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var circleServices = new CircleServices();
            var commentServices = new CommentServices();
            var postServices = new PostServices();
            var userServices = new UserServices();

            //List<Circle> circleList// = circleServices.Get();
            //List<Comment> commentList = commentServices.Get();
            //List<Post> postList = postServices.Get();

            //List<User> usersList = userServices.Get();

            //foreach (var c in circleList)
            //{
            //    Console.WriteLine(c);
            //}

            //foreach (var co in commentList)
            //{
            //    Console.WriteLine(co);
            //}

            //foreach (var p in postList)
            //{
            //    Console.WriteLine(p);
            //}

            //foreach (var u in usersList)
            //{
            //    Console.WriteLine(u);
            //}
 

            Console.WriteLine("Welcome to SocialNetwork\n\nUse the following commands:");
            Console.WriteLine("A: Add user\nB: Make post\nC: Make a circle\nD: Comment on post");

            ConsoleKeyInfo k = Console.ReadKey(true);

            switch (k.Key)
            {
                case ConsoleKey.A:
                    userServices.CreateUser();
                    break;
                case ConsoleKey.B:
                    postServices.CreatePost();
                    break;
                default:
                    break;
            }
        }
    }
}
