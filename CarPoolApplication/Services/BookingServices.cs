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
       
        public bool StartRides(string offerID)
        {
            try
            {
                List<Booking> bookings = bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.confirm)));
                foreach (var booking in bookings)
                {
                    booking.Status = BookingStatus.running;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EndRides(string offerID)

        {
            try
            {
                List<Booking> bookings = bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.running)));
                foreach (var booking in bookings)
                {
                    booking.Status = BookingStatus.compleated;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CancelRides(string offerID)
        {
            try
            {
                List<Booking> bookings = bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) && (bookingrequests.Status.Equals(BookingStatus.confirm)) || (bookingrequests.Status.Equals(BookingStatus.pending)));
                foreach (var booking in bookings)
                {
                    booking.Status = BookingStatus.cancel;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

