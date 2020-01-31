using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class CarDetails
    {
        public string ID;
        public string CarName;
        public string CarNumber;
        public string UserID;
         public CarDetails(string name,string number,string userid)
        {
            ID = number + DateTime.UtcNow.ToString("mmss");
            CarName = name;
            CarNumber = number;
            UserID = userid;
        }
    }
}
