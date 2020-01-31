using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using System.Linq;

namespace CarPoolApplication.Services
{
    class UserServices
    {
        private List<User> Users = new List<User>();
         
        public List<User> GetUsers()
        {
            return Users;
        }
        public User CreateUser(User user)
        {
            User Newuser = new User()
            {
                Name = user.Name,
                ID = user.Name + DateTime.UtcNow.ToString("mmss"),
                Password=user.Password,
                PhoneNumber=user.PhoneNumber

            };
            Users.Add(Newuser);
            return Newuser;
        }
        public User GetUser( string userId)
        {
            return Users.FirstOrDefault(user => string.Equals(user.ID, userId));
        }
        public bool IsValidUser(string ID,string password)
        {
            return Users.Any(user => string.Equals(user.ID, ID) && string.Equals(user.Password, password));
        }
    }
}
