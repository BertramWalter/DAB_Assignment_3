using System;
using System.Collections.Generic;
using System.Text;

namespace DAB_Assignment_3.Models
{
    class Users
    {
        public int UsersId { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }

        public List<Users> Followers  { get; set; }

        public List<Users> Following { get; set; }

        public List<Circle> UserCircle { get; set; }
    }
}
