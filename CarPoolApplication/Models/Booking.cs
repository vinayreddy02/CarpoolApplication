using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class Booking
    {
        public string ID { get; set; }
        public string PassengerID { get; set; }
        public string FromPoint { get; set; }
        public string ToPoint { get; set; }
        public string OfferID { get; set; }
        public decimal Price { get; set; }
        public int NumberOfseats { get; set; }
        public Status Status { get; set; }
        public DateTime DateTime { get; set; }
        public Booking(string userID,string fromPoint,string toPoint,string offerId,int numberOfSeats,DateTime dateTime)
        {
            PassengerID = userID;
            FromPoint = fromPoint;
            ToPoint = toPoint;
            OfferID = offerId;
            NumberOfseats = numberOfSeats;
            Status = Status.pending;
            DateTime = dateTime;
            ID = userID + DateTime.UtcNow.ToString("mmss");
        }
    }
}
