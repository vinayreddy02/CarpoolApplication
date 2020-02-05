using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using System.Linq;

namespace CarPoolApplication.Services
{
    class UserServices:IService<User>
    {
        private List<User> Users = new List<User>();
         
        public List<User> GetAll()
        {
            return Users;
        }
        public void Add(User user)
        {

            Users.Add(user);
           
        }
        public User GetUser( string userId)
        {
            return Users.FirstOrDefault(user => string.Equals(user.ID, userId));
        }
        public bool IsValidUser(string ID,string password)
        {
            return Users.Any(user => string.Equals(user.ID, ID) && string.Equals(user.PassWord, password));
        }
    }
}
