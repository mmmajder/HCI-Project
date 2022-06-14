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
        private static User logged = null;

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
                    logged = user;
                    return user;
                }
            }
            return null;
        }

        public static User getLogged()
        {
            return logged;
        }

        public static User getUser(string username)
        {
            foreach (User u in Users)
                if (u.Username.Equals(username))
                    return u;

            return null;
        }
    }
}
