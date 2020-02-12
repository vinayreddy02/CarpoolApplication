using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class Vehicle
    {
        public string ID { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public string UserID { get; set; }
        public int Numberofseats { get; set; }
        public Vehicle(string name,string number,string userid,int numberOfSeats)
        {
            ID = number +""+name;
            CarName = name;
            CarNumber = number;
            UserID = userid;
            Numberofseats = numberOfSeats;
        }
    }
}
