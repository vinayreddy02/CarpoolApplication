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
        public int Price { get; set; }
        public OfferStatus Status { get; set; }
        public RideStatus RideStatus { get; set; }
        public DateTime DateTime { get; set; }
      public Offer(string driverID,string fromPoint,string toPoint,string carID,int availableSeats,int costPerPoint,DateTime dateTime)
        {
            DriverID = driverID;
            FromPoint = fromPoint;
            ToPoint = toPoint;
            CarID = carID;
            AvailableSeats = availableSeats;
            CostPerPoint = costPerPoint;
            DateTime = dateTime;
            Status = OfferStatus.open;
            RideStatus = RideStatus.pending;
            ID = driverID + DateTime.UtcNow.ToString("mmss");
        }
    }
}
