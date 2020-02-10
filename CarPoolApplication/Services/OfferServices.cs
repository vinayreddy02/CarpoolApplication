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
                return Offers.FindAll(offer => string.Equals(offer.DriverID, userID) && offer.status.Equals(OfferStatus.open));
            }
            catch
            {
                return null;
            }
        }   
        public bool AddViapoint(string viapoint)
        {
            try
            {
                viapoints.Add(viapoint);
                return true;
            }
            catch
            {
                return false;
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
                int fromIndex = -1, toIndex = -1;
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
                            if (offer.status.Equals(OfferStatus.open) && (offer.AvailableSeats > numberOfSeats) && (string.Equals(offer.DateTime.Date.ToString(), dateTime.Date.ToString())))
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
                            request.Price = numberOfPoints * offer.CostPerPoint;
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
        public bool EndRide(Booking booking)
        {
            try
            {
                booking.Status = Status.compleated;
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
        public bool CloseOffer(Offer offer)
        {
            try
            {
                offer.status = OfferStatus.close;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
