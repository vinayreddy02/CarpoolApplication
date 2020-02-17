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
        public bool AddOffer(Offer offer);
        public List<Offer> GetOffers(string userID);
        public Offer GetOfferUsingOfferID(string OfferID);
        public List<Offer> GetAvilableOffers(string frompoint, string topoint,List<Location> fromLocations,List<Location> toLocations, int numberOfSeats, DateTime dateTime);
        public bool ApprovalOfBooking(Booking request, Offer offer,List<Location> locations);
        public bool EndRide(Offer offer);
        public bool StartRide(Offer offer);
        public bool CancelRide(Offer offer);
        public List<Offer> GetAllOffers(string userID);
        public bool CloseOffer(Offer offer);
    }
}
