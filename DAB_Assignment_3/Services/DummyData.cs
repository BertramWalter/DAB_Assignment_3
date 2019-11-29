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

                users.InsertOne(u);
            }

            #endregion

            #region Circle

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

            
        }
    }
}
