
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    public enum Status { pending = 1, confirm = 2 ,compleated=3}
    public enum OfferStatus { open=1,close=2};
    public enum LoginOption { signup = 1, login = 2 }
    public enum Options { createOffer= 1, vechicleBooking = 2, ViewAllOffers = 4, viewAllBookings = 3, Logout = 5 }
    public enum OfferOption {  Approve = 1,EndRide=2,CloseOffer=3,ViewAllRides=4, Exit = 5 }
}
