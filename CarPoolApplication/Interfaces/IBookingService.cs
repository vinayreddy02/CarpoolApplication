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
        public List<Booking> GetRides(string offerID);
        public List<Booking> GetAllRidesToStart(string offerID);
        public List<Booking> GetAllRidesToEnd(string offerID);
        public List<Booking> GetAll();
        public bool Add(Booking bookingRequest);
    }
}
