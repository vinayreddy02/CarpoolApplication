using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Interfaces
{
    interface IUserService
    {
        public List<User> GetAll();
        public bool Add(User user);
        public User GetUser(string userId);
        public bool IsValidUser(string ID, string password);
    }
}
