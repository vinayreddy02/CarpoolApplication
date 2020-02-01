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
        public List<string> viapoints = new List<string>();
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
        public void AddviaPoint(string viapoint)
        {
            viapoints.Add(viapoint);
        }
        public List<Offer> GetListOfAvilableOffers(string frompoint,string topoint)
        {
            int fromIndex, toIndex;
            List<Offer> AvailableOffers = new List<Offer>();
            foreach(var offer in Offers)
            {
                for(int index=0; index<offer.viaPoints.count; index++)
                {
                    if(string.Equals(frompoint, offer.viaPoints[index]))
                    {
                        fromIndex = index;
                    }
                    else if(string.Equals(topoint, offer.viaPoints[index]))
                    {
                        toIndex = index;
                    }
                }
                if (fromIndex < toIndex)
                {
                    AvailableOffers.Add(offer);
                }
            }
            return AvailableOffers;
        }
    }
}
