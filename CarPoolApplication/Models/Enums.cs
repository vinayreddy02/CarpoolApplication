
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
   public enum Status {pending=1,confirm=2 }
   public enum LoginOption { signup=1,login=2}
    public enum Options { forcreatingOffer=1,vechicleBooking=2,ViewAllOffers=3,viewAllBookings=4,Logout=5}
    public enum OfferOption { Create=1,Approve=2,Exit=3}
}
