using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
            }

            #endregion


        }
    }
}
