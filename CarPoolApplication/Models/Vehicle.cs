using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class Vehicle
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string UserID { get; set; }
        public int Numberofseats { get; set; }
        public Vehicle(string name,string number,string userid,int numberOfSeats)
        {
            ID = number +""+name;
            Name = name;
            Number = number;
            UserID = userid;
            Numberofseats = numberOfSeats;
        }
    }
}
