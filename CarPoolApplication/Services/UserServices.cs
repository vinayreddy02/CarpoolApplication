using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using System.Linq;
using CarPoolApplication.Interfaces;

namespace CarPoolApplication.Services
{
    class UserServices: IUserService
    {
        private List<User> Users = new List<User>();
         
        public List<User> GetAll()
        {
            try
            {
                return Users;
            }
            catch
            {
                return null;
            }
        }
        public bool AddUser(User user)
        {
            try
            {
                Users.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        public User GetUser( string userId)
        {
            try
            {
                return Users.FirstOrDefault(user => string.Equals(user.ID, userId));
            }
            catch
            {
                return null;
            }
        }
        public bool IsValidUser(string ID,string password)
        {
           
                return Users.Any(user => string.Equals(user.ID, ID) && string.Equals(user.PassWord, password));
           
        }
    }
}
