using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using System.Linq;

namespace CarPoolApplication.Services
{
    class OfferServices : IService<Offer>
    {
        private List<Offer> Offers = new List<Offer>();
        public List<string> viapoints = new List<string>();
        public List<Offer> GetAll()
        {
            return Offers;
        }
        public void Add(Offer offer)
        {
            Offers.Add(offer);
        }
        public Offer GetOffer(string offerID)
        {
            return Offers.FirstOrDefault(offer => string.Equals(offer.ID, offerID));
        }
        public void AddViapoint(string viapoint)
        {
            viapoints.Add(viapoint);
        }
        public List<Offer> GetListOfAvilableOffers(string frompoint, string topoint, List<Location> locations)
        {
            Offer offer;
            int fromIndex = 0, toIndex = 0;
            List<Offer> AvailableOffers = new List<Offer>();

            for (int index = 0; index < locations.Count; index++)
            {
                if (string.Equals(locations[index].Place,frompoint))
                {
                    fromIndex = index;
                }
                else if (string.Equals(locations[index].Place,topoint))
                {
                    toIndex = index;
                }
                if ((fromIndex != 0) && (toIndex != 0))
                {
                    if ((fromIndex < toIndex) && string.Equals(locations[fromIndex].OfferID, locations[toIndex].OfferID))
                    {
                      
                          offer = GetOffer(locations[fromIndex].OfferID);
                        if (offer.status.Equals(OfferStatus.open))
                        {
                            AvailableOffers.Add(offer);
                        }
                    }
                    fromIndex = 0;
                    toIndex = 0;
                }
            }
            return AvailableOffers;
        }
        public void ApprovalOfBooking(Booking request, Offer offer, List<Location> locations)
        {
           
           
            int numberOfPoints;
            int fromIndex = 0, toIndex = 0;
            for (int index = 0; index < locations.Count; index++)
            {
                if (string.Equals(request.FromPoint, locations[index]))
                {
                    fromIndex = index;
                }
                else if (string.Equals(request.ToPoint, locations[index]))
                {
                    toIndex = index;
                }
                if (fromIndex != 0 && toIndex != 0)
                {
                    if ((fromIndex < toIndex) && string.Equals(locations[fromIndex].OfferID, locations[toIndex].OfferID))
                    {
                        numberOfPoints = toIndex - fromIndex;
                        request.Price = numberOfPoints * offer.CostPerPoint;
                        request.Status = Status.confirm;
                        offer.AvailableSeats -= 1;
                    }
                }
            }
        }
        public void EndRide(Booking booking)
        {
            booking.Status = Status.compleated;

        }
        public List<Offer> GetAllOffers(string userID)
        {
            return Offers.FindAll(Offer => string.Equals(Offer.DriverID, userID));
        }
        public void CloseOffer(Offer offer)
        {
            offer.status = OfferStatus.close;
        }
    }
}
