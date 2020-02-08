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
        public Offer GetOffer(string userID)
        {
            return Offers.FirstOrDefault(offer => string.Equals(offer.DriverID, userID));
        }
        public void AddViapoint(string viapoint)
        {
            viapoints.Add(viapoint);
        }

        public Offer GetOfferUsingOfferID(string OfferID)
        {
            return Offers.FirstOrDefault(offer => string.Equals(offer.ID, OfferID));
        }

        public List<Offer> GetListOfAvilableOffers(string frompoint, string topoint, List<Location> locations,int numberOfSeats,DateTime dateTime)
        {
            Offer offer;
            int fromIndex = -1, toIndex = -1;
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
                if ((fromIndex != -1) && (toIndex != -1))
                {
                    if ((locations[fromIndex].StationNumber < locations[toIndex].StationNumber) && string.Equals(locations[fromIndex].OfferID, locations[toIndex].OfferID))
                    {
                        offer = GetOfferUsingOfferID(locations[fromIndex].OfferID);
                        if (offer.status.Equals(OfferStatus.open)&&(offer.AvailableSeats>numberOfSeats)&&(string.Equals(offer.DateTime.ToString(),dateTime.ToString())))
                        {
                            AvailableOffers.Add(offer);
                        }
                    }
                    fromIndex = -1;
                    toIndex = -1;
                }
            }
            return AvailableOffers;
        }
        public void ApprovalOfBooking(Booking request, Offer offer, List<Location> locations)
        {
           
            int numberOfPoints;
            int fromIndex = -1, toIndex = -1;
            for (int index = 0; index < locations.Count; index++)
            {
                if (string.Equals(request.FromPoint, locations[index].Place))
                {
                    fromIndex = index;
                }
                else if (string.Equals(request.ToPoint, locations[index].Place))
                {
                    toIndex = index;
                }
                if (fromIndex != -1 && toIndex != -1)
                {
                    if ((locations[fromIndex].StationNumber<locations[toIndex].StationNumber) && string.Equals(locations[fromIndex].OfferID, locations[toIndex].OfferID))
                    {
                        numberOfPoints = locations[toIndex].StationNumber-locations[fromIndex].StationNumber;
                        request.Price = numberOfPoints * offer.CostPerPoint;
                        request.Status =Status.confirm;
                        offer.AvailableSeats -= request.NumberOfseats;
                    }
                    fromIndex = -1;
                    toIndex = -1;

                }
            }

        }
        public void EndRide(Booking booking)
        {
            booking.Status =Status.compleated;

        }
        public List<Offer> GetAllOffers(string userID)
        {
            return Offers.FindAll(Offer => string.Equals(Offer.DriverID, userID));
        }
        public void CloseOffer(Offer offer)
        {
            offer.status +=1;
        }
    }
}
