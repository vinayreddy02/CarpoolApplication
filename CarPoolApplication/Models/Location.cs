using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    class Location
    {
        public string Place { get; set; }
        public string OfferID { get; set; }
        public string ID { get; set; }
        public int StationNumber { get; set; }
        public Location(string place,string offerID,int position )
        {
            Place = place;
            OfferID = offerID;
            ID = DateTime.UtcNow.ToString("yyyyMMddss");
            StationNumber = position;
        }
    }
}
