using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class User
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string PassWord { get; set; }
        public decimal PhoneNumber { get; set; }
        public User(string name,string passWord,decimal phoneNumber)
        {
            Name = name;
            PassWord = passWord;
            PhoneNumber = phoneNumber;
            ID = name + DateTime.UtcNow.ToString("mmss");

        }
    }
}
