using System;
using System.Collections.Generic;
using System.Linq;
using DAB_Assignment_3.Models;
using DAB_Assignment_3.Services;
using MongoDB.Driver;

namespace DAB_Assignment_3
{
    class Program
    {
        static MongoClient client = new MongoClient("mongodb://localhost:27017");
        static void Main(string[] args)
        {
            var database = client.GetDatabase("SocialNetworkDb");
            var circleServices = new CircleServices();
            var commentServices = new CommentServices();
            var postServices = new PostServices();
            var userServices = new UserServices();
            var _users = database.GetCollection<User>("Users");

            while (true)
            {
                DisplayMainChoices();
                var input = UserInput();

                switch (input)
                {
                    case "1":
                        UserMenu(userServices);
                        break;
                }

            }




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
        }

        private static void UserMenu(UserServices userServices)
        {
            List<User> users = userServices.Get();
            bool userMenu = true;

            while (userMenu)
            {
                ListAllUsers(users);

                Console.WriteLine("Input user id or 'esc' to go back");

                var userId = UserInput();
                if (userId == "esc")
                    return;

                bool idExists = users.Any(u => u.Id == userId);

                if (!idExists)
                    Console.WriteLine("Invalid input try again");
                else
                {
                    Console.WriteLine("Choices:");
                    Console.WriteLine("1: Get user feed");
                    Console.WriteLine("2: Get Wall as guest");
                    Console.WriteLine("3: Create a post");
                    var selection = UserInput();
                    switch (selection)
                    {
                        case "1":
                            userServices.GetFeed(userId);
                            break;
                        case "2":
                            userServices.
                    }
                }
            }
        }

        private static void UserSelectedMenu()
        {

        }

        private static void ListAllUsers(List<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.Name} Id: {user.Id}");
            }
        }

        private static void SelectUser(string userId)
        {
            
        }

        private static void DisplayMainChoices()
        {
            Console.WriteLine("Here are your choices");
            Console.WriteLine("1: List all Users");
            Console.WriteLine("2: Select user by id");
        }

        private static string UserInput()
        {
            return Console.ReadLine();
        }
    }

}
