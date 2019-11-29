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
                var input = UserInput("Input Selection");

                switch (input)
                {
                    case "1":
                        ListAllUsers(userServices);
                        break;
                    case "2":
                        UserMenu(userServices);
                        break;
                    case "3":
                        postServices.CreatePost();
                        break;
                    case "4":
                        commentServices.CreateComment(UserInput("Input post id: "), DateTime.Now.ToString(),
                            UserInput("Input comment: "));
                        break;
                    case "5":
                        userServices.CreateUser();
                        break;
                    default:
                        Console.WriteLine("Wrong input");
                        break;
                }
            }
        }

        private static void UserMenu(UserServices userServices)
        {
            List<User> users = userServices.Get();

            while (true)
            {
                var userId = UserInput("Input user id or 'esc' to go back");
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
                    var selection = UserInput("Inout Selection");
                    switch (selection)
                    {
                        case "1":
                            userServices.GetFeed(userId);
                            break;
                        case "2":
                            userServices.GetWall(userId,UserInput("Input guest id:"));
                            break;
                        default:
                            Console.WriteLine("Wrong selection try again");
                            break;
                    }
                }
            }
        }


        private static void ListAllUsers(UserServices userServices)
        {
            List<User> users = userServices.Get();
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
            Console.WriteLine("3: Create Post");
            Console.WriteLine("4: Create Comment");
            Console.WriteLine("5: Create user");
        }

        private static string UserInput(string outputToUser)
        {
            Console.WriteLine(outputToUser);
            return Console.ReadLine();
        }
    }

}
