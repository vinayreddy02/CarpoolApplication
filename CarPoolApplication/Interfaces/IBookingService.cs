using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Interfaces
{
    interface IBookingService
    {
        public List<Booking> GetRequests(string offerID);
        public List<Booking> GetAllbookings(string userID);
        public List<Booking> GetAllRidesToStart(string offerID);
        public bool StartRides(string offerID);
        public bool EndRides(string offerID);
        public bool CancelRides(string offerID);
        public List<Booking> GetAll();
        public bool Add(Booking bookingRequest);
    }
}
