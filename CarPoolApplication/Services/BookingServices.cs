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
            return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID)&&(bookingrequests.Status==Status.pending));
        }
        public List<Booking> GetAllbookings(string userID)
        {
            return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.PassengerID,userID)&&(bookingrequests.Status==Status.compleated));
        }
        public List<Booking> GetAllRides(string offerID)
        {
            return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.OfferID, offerID) &&(bookingrequests.Status==Status.confirm));
        }
    }
}
