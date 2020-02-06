
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    public enum Status { pending = 1, confirm = 2 ,compleated=3}
    public enum OfferStatus { open=1,close=2};
    public enum LoginOption { signup = 1, login = 2 }
    public enum Options { forcreatingOffer = 1, vechicleBooking = 2, ViewAllOffers = 4, viewAllBookings = 3, Logout = 5 }
    public enum OfferOption { Create = 1, Approve = 2,EndRide=3,CloseOffer=4, Exit = 5 }
}
