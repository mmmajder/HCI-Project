using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class UserRepo
    {
        private static List<User> Users = new List<User>();
        public UserRepo()
        {
            Users.Add(new User(UserType.Client, "Petar", "Petrovic", "p", "p"));
            Users.Add(new User(UserType.Client, "Ana", "Anic", "a", "a"));
            Users.Add(new User(UserType.Client, "Marko", "Markovic", "m", "m"));
            Users.Add(new User(UserType.Manager, "Jovan", "Jovanovic", "j", "j"));
        }

        public static void AddUser(User user)
        {
            Users.Add(user);
        }

        public static List<User> GetUsers()
        {
            return Users;
        }

        public static User Login(string username, string password)
        {
            foreach (User user in Users)
            {
                if (user.Username==username && user.Password==password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
