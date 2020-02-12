using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Interfaces
{
    interface IOfferService
    {
        public List<Offer> GetAll();
        public bool Add(Offer offer);
        public List<Offer> GetOffers(string userID);
        public Offer GetOfferUsingOfferID(string OfferID);
        public List<Offer> GetListOfAvilableOffers(string frompoint, string topoint, List<Location> locations, int numberOfSeats, DateTime dateTime);
        public bool ApprovalOfBooking(Booking request, Offer offer, List<Location> locations);
        public bool EndRide(Offer offer, List<Booking> bookings);
        public bool StartRide(Offer offer, List<Booking> bookings);
        public List<Offer> GetAllOffers(string userID);
        public bool CloseOffer(Offer offer);
    }
}
