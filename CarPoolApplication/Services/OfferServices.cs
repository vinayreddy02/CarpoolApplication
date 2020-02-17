using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using System.Linq;
using CarPoolApplication.Interfaces;

namespace CarPoolApplication.Services
{
    class OfferServices : IOfferService
    {
        private List<Offer> Offers = new List<Offer>();

        public List<Offer> GetAll()
        {
            try
            {
                return Offers;
            }
            catch
            {
                return null;
            }
        }
        public bool AddOffer(Offer offer)
        {
            try
            {
                Offers.Add(offer);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public List<Offer> GetOffers(string userID)
        {
            try
            {
                return Offers.FindAll(offer => string.Equals(offer.DriverID, userID) && offer.Status.Equals(OfferStatus.open));
            }
            catch
            {
                return null;
            }
        }

        public Offer GetOfferUsingOfferID(string OfferID)
        {
            try
            {
                return Offers.FirstOrDefault(offer => string.Equals(offer.ID, OfferID));
            }
            catch
            {
                return null;
            }
        }


        public List<Offer> GetAvilableOffers(string frompoint, string topoint,List<Location> fromLocations,List<Location> toLocations, int numberOfSeats, DateTime dateTime)
        {
            try
            {
                Offer offer;
                int numberOfPoints;
                List<Offer> AvailableOffers = new List<Offer>();
                
                for (int fromIndex = 0; fromIndex < fromLocations.Count; fromIndex++)
                {
                    for (int toIndex = 0; toIndex < fromLocations.Count; toIndex++)
                    {
                        if (string.Equals(fromLocations[fromIndex].OfferID, toLocations[toIndex].OfferID) && fromLocations[fromIndex].StationNumber < toLocations[toIndex].StationNumber)
                        {
                            offer = GetOfferUsingOfferID(fromLocations[fromIndex].OfferID);
                            if (offer.Status.Equals(OfferStatus.open) && (offer.AvailableSeats > numberOfSeats) && (string.Equals(offer.DateTime.Date.ToString(), dateTime.Date.ToString())))
                            {
                                numberOfPoints = toLocations[toIndex].StationNumber - fromLocations[fromIndex].StationNumber;
                                offer.Price = numberOfPoints * offer.CostPerPoint;
                                AvailableOffers.Add(offer);
                            }
                        }
                    }
                }
                return AvailableOffers;
            }
            catch
            {
                return null;
            }
        }

        public bool ApprovalOfBooking(Booking request, Offer offer, List<Location> locations)
        {
            try
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
                        if (locations[fromIndex].StationNumber < locations[toIndex].StationNumber)
                        {
                            numberOfPoints = locations[toIndex].StationNumber - locations[fromIndex].StationNumber;
                            request.Price = numberOfPoints * offer.CostPerPoint * request.NumberOfseats;
                            request.Status = BookingStatus.confirm;
                            offer.AvailableSeats -= request.NumberOfseats;
                            if (offer.AvailableSeats == 0)
                            {
                                offer.Status = OfferStatus.close;
                            }

                        }
                        fromIndex = -1;
                        toIndex = -1;

                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool EndRide(Offer offer)
        {
            try
            {
                offer.RideStatus = RideStatus.Compleated;

                return true;

            }
            catch
            {
                return false;
            }

        }
        public List<Offer> GetAllOffers(string userID)
        {
            try
            {
                return Offers.FindAll(Offer => string.Equals(Offer.DriverID, userID));
            }
            catch
            {
                return null;
            }

        }
        public bool StartRide(Offer offer)
        {
            try
            {
                offer.RideStatus = RideStatus.running;
                if (offer.Status.Equals(OfferStatus.open))
                {
                    offer.Status = OfferStatus.close;
                }
               

                return true;

            }
            catch
            {
                return false;
            }
        }
        public bool CloseOffer(Offer offer)
        {
            try
            {
                offer.Status = OfferStatus.close;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CancelRide(Offer offer)
        {
            try
            {
                offer.RideStatus = RideStatus.cancel;
                offer.Status = OfferStatus.close;
          
                return true;

            }
            catch
            {
                return false;
            }
        }

    }
}
