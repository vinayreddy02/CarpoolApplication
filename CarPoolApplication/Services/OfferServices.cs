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
        public bool Add(Offer offer)
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
            try {
                return Offers.FirstOrDefault(offer => string.Equals(offer.ID, OfferID));
            }
            catch
            {
                return null;
            }
        }

        public List<Offer> GetListOfAvilableOffers(string frompoint, string topoint, List<Location> locations,int numberOfSeats,DateTime dateTime)
        {
            try
            {
                Offer offer;
                int fromIndex = -1, toIndex = -1, numberOfPoints;
                List<Offer> AvailableOffers = new List<Offer>();

                for (int index = 0; index < locations.Count; index++)
                {
                    if (string.Equals(locations[index].Place, frompoint))
                    {
                        fromIndex = index;
                    }
                    else if (string.Equals(locations[index].Place, topoint))
                    {
                        toIndex = index;
                    }
                    if ((fromIndex != -1) && (toIndex != -1))
                    {
                        if ((locations[fromIndex].StationNumber < locations[toIndex].StationNumber) && string.Equals(locations[fromIndex].OfferID, locations[toIndex].OfferID))
                        {
                            offer = GetOfferUsingOfferID(locations[fromIndex].OfferID);
                            if (offer.Status.Equals(OfferStatus.open) && (offer.AvailableSeats > numberOfSeats) && (string.Equals(offer.DateTime.Date.ToString(), dateTime.Date.ToString())))
                            {
                                numberOfPoints = locations[toIndex].StationNumber - locations[fromIndex].StationNumber;
                                offer.Price = numberOfPoints * offer.CostPerPoint;
                                AvailableOffers.Add(offer);
                            }
                        }
                        fromIndex = -1;
                        toIndex = -1;
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
                        if ((locations[fromIndex].StationNumber < locations[toIndex].StationNumber) && string.Equals(locations[fromIndex].OfferID, locations[toIndex].OfferID))
                        {
                            numberOfPoints = locations[toIndex].StationNumber - locations[fromIndex].StationNumber;
                            request.Price = numberOfPoints * offer.CostPerPoint*request.NumberOfseats;
                            request.Status = Status.confirm;
                            offer.AvailableSeats -= request.NumberOfseats;

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
        public bool EndRide(Offer offer, List<Booking> bookings)
        {
            try
            {
                
                foreach (var booking in bookings)
                {
                    booking.Status = Status.compleated;
                }
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
        public bool StartRide(Offer offer,List<Booking> bookings)
        {
            try
            {
                offer.RideStatus = RideStatus.running;
                foreach(var booking in bookings)
                {
                    booking.Status = Status.running;
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
    }
}
