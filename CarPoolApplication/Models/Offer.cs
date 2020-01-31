using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class Offer
    {
        public string ID;
        public string DriverID;    
        public string FromPoint;
        public string ToPoint;
        public string carID;
        public int AvailableSeats;
        public List<string> viaPoints = new List<string>();
    }
}
