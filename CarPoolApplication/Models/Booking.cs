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
        public Status Status { get; set; }
        public Booking(string userID,string fromPoint,string toPoint,string offerId)
        {
            PassengerID = userID;
            FromPoint = fromPoint;
            ToPoint = toPoint;
            OfferID = offerId;
            Status = Status.pending;
            ID = userID + DateTime.UtcNow.ToString("mmss");
        }
    }
}
