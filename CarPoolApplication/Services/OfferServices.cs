using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using System.Linq;

namespace CarPoolApplication.Services
{
    class OfferServices
    {
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
             CarID=offer.CarID,
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
        public void AddViapoint(string viapoint)
        {
            viapoints.Add(viapoint);
        }
        public List<Offer> GetListOfAvilableOffers(string frompoint,string topoint)
        {
            int fromIndex=0, toIndex=0;
            List<Offer> AvailableOffers = new List<Offer>();
            foreach(var offer in Offers)
            {
                for(int index=0; index<offer.viaPoints.Count; index++)
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
        public void ApprovalOfBooking(BookingRequest request,Offer offer)
        {
            request.Status = Status.confirm;
            offer.AvailableSeats -= 1;
            int fromIndex = 0, toIndex = 0;
           
                  for (int index = 0; index < offer.viaPoints.Count; index++)
                  {
                    if (string.Equals(request.FromPoint, offer.viaPoints[index]))
                     {
                    fromIndex = index;
                      }
                else if (string.Equals(request.ToPoint, offer.viaPoints[index]))
                     {
                    toIndex = index;
                     }
                   }
            int numberOfPoints = toIndex - fromIndex;
            request.Price = numberOfPoints * offer.CostPerPoint;
        }
    }
}
