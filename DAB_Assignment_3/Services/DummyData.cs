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
    }
}
