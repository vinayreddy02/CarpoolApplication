using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using System.Linq;

namespace CarPoolApplication.Services
{
    class OfferServices
    {
        List<string> locations = utilServices.locations;
        private List<Offer> Offers = new List<Offer>();
        public List<Offer> GetOffers()
        {
            return Offers;
        }
        public Offer CreateOffer(Offer offer,User user)
        {
            Offer newOffer = new Offer()
            { ID = user.ID + DateTime.UtcNow.ToString("mmss"),
             DriverID=user.ID,
             DriverName=user.Name,
             DriverPhoneNumber=user.PhoneNumber,
             CarName=offer.CarName,
             CarNumber=offer.CarNumber,
             FromPoint=offer.FromPoint,
             ToPoint=offer.ToPoint,
             AvailableSeats=offer.AvailableSeats,


            };
            return newOffer;
        }
        public Offer GetOffer(string offerID)
        {
            return Offers.FirstOrDefault(offer => string.Equals(offer.ID, offerID));
        }
        public List<Offer> AvailableOffers(string fromPoint,string topoint)
        {
           
        }
    }
}
