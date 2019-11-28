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
            //var circleServices = new CircleServices();
            //var commentServices = new CommentServices();
            //var postServices = new PostServices();
            //var usersServices = new UserServices();

            //List<Circle> circleList = circleServices.Get();
            //List<Comment> commentList = circleServices.Get();
            //List<Post> postList = circleServices.Get();
            //List<Users> usersList = circleServices.Get();

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

            ConsoleKeyInfo key;
            do
            {
                Console.Write("PRESS 'Y' for YES or 'N' for NO: ");
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("YYY");
                }
                else if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("NNN");
                }
            } while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);



        }
    }
}
