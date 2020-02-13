using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication.Interfaces
{
    interface IOfferService
    {
        public List<Offer> GetAll();
        public bool Add(Offer offer);
        public List<Offer> GetOffers(string userID);
        public Offer GetOfferUsingOfferID(string OfferID);
        public List<Offer> GetAvilableOffers(string frompoint, string topoint, LocationServices locationService, int numberOfSeats, DateTime dateTime);
        public bool ApprovalOfBooking(Booking request, Offer offer,LocationServices locationService);
        public bool EndRide(Offer offer, List<Booking> bookings);
        public bool StartRide(Offer offer, List<Booking> bookings);
        public List<Offer> GetAllOffers(string userID);
        public bool CloseOffer(Offer offer);
    }
}
