using DAB_Assignment_3.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.DummyData
{
    public static class StaticDummyDataClass
    {
        public static void InsertDummyUsers()
        {
            User user = new User(name, age, gender);
            _users.InsertOne(user);
        }

    }
}
