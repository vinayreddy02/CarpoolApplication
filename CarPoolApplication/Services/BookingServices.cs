using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Interfaces;

namespace CarPoolApplication.Services
{
    class BookingServices : IBookingService
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
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.pending)));
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
        public List<Booking> GetAllRidesToStart(string offerID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.confirm)));
            }
            catch
            {
                return null;
            }
        }
        public List<Booking> GetAllRidesCancel(string offerID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.confirm)) || (bookingrequests.Status.Equals(BookingStatus.pending)));
            }
            catch
            {
                return null;
            }
        }
        public List<Booking> GetAllRidesToEnd(string offerID)
        {
            try
            {
                return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.running)));
            }
            catch
            {
                return null;
            }
        }
    }
}

