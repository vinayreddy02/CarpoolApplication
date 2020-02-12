
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    public enum Status { pending = 1, confirm ,running,compleated}
   public enum OfferStatus { open=1,close}
    public enum RideStatus {pending=1, running,Compleated}
    public enum LoginOption { signup = 1, login}
    public enum Options { createOffer= 1, vechicleBooking, ViewAllOffers, ViewAllBookings, Logout }
    public enum OfferOption {  Approve = 1,startRide,EndRide,CloseOffer, Exit }
}
