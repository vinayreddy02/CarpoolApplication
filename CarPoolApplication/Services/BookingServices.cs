using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    class BookingServices : IService<Booking>
    {
        private List<Booking> bookingrequests = new List<Booking>();

        public List<Booking> GetAll()
        {
            return bookingrequests;
        }
        public bool Add(Booking bookingRequest)
        {
            try
            {
                bookingrequests.Add(bookingRequest);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<Booking> GetRequests(string offerID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(Status.pending)));
            }
            catch
            {
                return null;
            }
        }
        public List<Booking> GetAllbookings(string userID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.PassengerID, userID));
            }
            catch
            {
                return null;
            }
        }
        public List<Booking> GetRides(string offerID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID));
            }
            catch
            {
                return null;
            }
        }
        public List<Booking> GetAllRides(string offerID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(Status.confirm)));
            }
            catch
            {
                return null;
            }
        }
    }
}
