using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    class BookingServices:IService<Booking>
    {
        private List<Booking> bookingrequests = new List<Booking>();

        public List<Booking> GetAll()
        {
            return bookingrequests;
        }
        public void Add(Booking bookingRequest )
        {
            bookingrequests.Add(bookingRequest);
        }
        public List<Booking>  GetRequests(string offerID)
        {
            return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID));
        }
        public List<Booking> GetAllbookings(string userID)
        {
            return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.PassengerID,userID)&&int.Equals(bookingrequests.Status,Status.confirm));
        }
    }
}
