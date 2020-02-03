using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class BookingRequest
    {
        public string ID { get; set; }
        public string passengerID { get; set; }
        public string FromPoint { get; set; }
        public string ToPoint { get; set; }
        public string OfferID { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}
