using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class Offer
    {
        public string ID { get; set; }
        public string DriverID { get; set; } 
        public string FromPoint { get; set; }
        public string ToPoint { get; set; }
        public string CarID { get; set; }
        public int AvailableSeats { get; set; }
        public int CostPerPoint { get; set; }
        public List<string> viaPoints = new List<string>();
    }
}
