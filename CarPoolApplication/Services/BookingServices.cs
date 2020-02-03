using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Services
{
    class BookingServices
    {
        private List<BookingRequest> bookingrequests = new List<BookingRequest>();

        public List<BookingRequest> GetAllRequests()
        {
            return bookingrequests;
        }
        public void createBookingReqest(BookingRequest bookingRequest )
        {
            BookingRequest newBookingRequest = new BookingRequest()
            {
                ID = bookingRequest.passengerID + DateTime.UtcNow.ToString("mmss"),
                passengerID =bookingRequest.passengerID,               
                FromPoint = bookingRequest.FromPoint,
                ToPoint = bookingRequest.ToPoint,
                offerID =bookingRequest.offerID

            };

            bookingrequests.Add(newBookingRequest);
        }
        public List<BookingRequest>  GetRequests(string offerID)
        {
            return bookingrequests.FindAll(bookingrequests => string.Equals(bookingrequests.offerID, offerID));
        }
    }
}
