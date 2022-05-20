using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public User(UserType userType, string name, string surname, string username, string password)
        {
            this.Name = name;
            this.Surname = surname;
            this.UserType = userType;
            this.Username = username;
            this.Password = password;
        }

    }
}
