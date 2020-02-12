using System;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using System.Collections.Generic;

namespace CarPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            UserServices userServices = new UserServices();
            OfferServices offerServices = new OfferServices();
            LocationServices locationService = new LocationServices();
            BookingServices bookingServices = new BookingServices();
            List<Location> locations = locationService.GetAll();

            while (true)
            {
            Login:
                Console.WriteLine("1.signup\n2.login\n");
                try
                {
                    LoginOption LoginPageOption = (LoginOption)Convert.ToInt32(Console.ReadLine());
                    switch (LoginPageOption)
                    {
                        case LoginOption.signup:
                            {  name:
                                Console.WriteLine("Enter name");
                                string name = Console.ReadLine();
                                if (string.IsNullOrEmpty(name))
                                {
                                    Console.WriteLine("invalid input\n");
                                    goto name;
                                }

                                passWord:
                                Console.WriteLine("Enter password");
                                String passWord = Console.ReadLine();
                                if (string.IsNullOrEmpty(passWord))
                                {
                                    Console.WriteLine("invalid input\n");
                                    goto passWord;
                                }
                                phoneNumber:
                                Console.WriteLine("Enter phoneNumber");
                                string phoneNumber = Console.ReadLine();
                                if (string.IsNullOrEmpty(phoneNumber))
                                {
                                    Console.WriteLine("invalid input\n");
                                    goto phoneNumber;
                                }
                                User user = new User(name, passWord, phoneNumber);
                                if (userServices.Add(user))
                                {
                                    Console.WriteLine("account created\nuserId:" + user.ID);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("account creation failed\n ");
                                    break;
                                }
                            }
                        case LoginOption.login:
                            {
                                Console.WriteLine("Enter UserID");
                                string userID = Console.ReadLine();
                                Console.WriteLine("Enter password");
                                String password = Console.ReadLine();
                                if (userServices.IsValidUser(userID, password))
                                {
                                    User user = userServices.GetUser(userID);
                                    
                                    while (true)
                                    {
                                    
                                        try
                                        {
                                            Console.WriteLine("Welcome..choose an option\n");
                                            Console.WriteLine("1.Create Offer\n2.Book a car\n3.View all created offers\n4.View all bookings\n\n5.Log out\n");
                                            Options choice = (Options)Convert.ToInt32(Console.ReadLine());
                                            switch (choice)
                                            {
                                                case Options.createOffer:
                                                    {
                                                    CarNumber:

                                                        Console.WriteLine("Enter car number\n");
                                                        string vehicleNumber = Console.ReadLine();
                                                        if (string.IsNullOrEmpty(vehicleNumber))
                                                        {
                                                            Console.WriteLine("invalid input\n");
                                                            goto CarNumber;
                                                        }
                                                    CarName:
                                                        Console.WriteLine("Enter car name\n");
                                                        string vehicleName = Console.ReadLine();
                                                        if (string.IsNullOrEmpty(vehicleName))
                                                        {
                                                            Console.WriteLine("invalid input\n");
                                                            goto CarName;
                                                        }

                                                    NumberOfSeats:
                                                        Console.WriteLine("Enter number of seats");
                                                        try
                                                        {
                                                            int numberOfSeats = Convert.ToInt32(Console.ReadLine());
                                                            Vehicle vehicle = new Vehicle(vehicleNumber, vehicleName, userID, numberOfSeats);
                                                        FromLocation:


                                                            Console.WriteLine("Enter from location: ");
                                                            string FromLocation = Console.ReadLine();
                                                            if (string.IsNullOrEmpty(FromLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto FromLocation;
                                                            }
                                                        ToLocation:

                                                            Console.WriteLine("Enter to location: ");
                                                            string ToLocation = Console.ReadLine();
                                                            if (string.IsNullOrEmpty(ToLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto ToLocation;
                                                            }
                                                        costPerPoint:
                                                            try
                                                            {
                                                                Console.WriteLine("Enter costper point\n");
                                                                int costPerPoint = Convert.ToInt32(Console.ReadLine());
                                                            Date:

                                                                Console.WriteLine("Enter date and time in (yyyy/mm/day hr:min am/pm) formate\n");

                                                                try
                                                                {
                                                                    DateTime dateTime = DateTime.Parse(Console.ReadLine());

                                                                    if (DateTime.Compare(dateTime, DateTime.Now) < 0)
                                                                    {
                                                                        Console.WriteLine("Please give valid Date and Time\n");
                                                                        goto Date;
                                                                    }

                                                                    Offer offer = new Offer(user.ID, FromLocation, ToLocation, vehicle.ID, numberOfSeats, costPerPoint, dateTime);
                                                                    offerServices.Add(offer);
                                                                numberOfViaPoints:
                                                                    try
                                                                    {
                                                                        Console.WriteLine("Enter number of via points\n");
                                                                        int numberOfViaPoints = Convert.ToInt32(Console.ReadLine());
                                                                        Console.WriteLine("Enter viapoints\n");
                                                                        int start = 1;
                                                                        if (!locationService.Add(new Location(FromLocation, offer.ID, start)))
                                                                        {
                                                                            Console.WriteLine("from location not saved\n");
                                                                        }
                                                                        if (!locationService.Add(new Location(ToLocation, offer.ID, numberOfViaPoints + 2)))
                                                                        {
                                                                            Console.WriteLine("To location not saved\n");
                                                                        }
                                                                        while (numberOfViaPoints != 0)
                                                                        {
                                                                        viapoint:

                                                                            string place = Console.ReadLine();
                                                                            if (string.IsNullOrEmpty(place))
                                                                            {
                                                                                Console.WriteLine("invalid input\n");
                                                                                goto viapoint;
                                                                            }

                                                                            if (!locationService.IsPlaceExists(place, offer.ID))
                                                                            {
                                                                                if (locationService.Add(new Location(place, offer.ID, ++start)))
                                                                                {
                                                                                    numberOfViaPoints--;
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.WriteLine(place + "not saved");
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine("you can not add a place which is already added\n");
                                                                            }
                                                                        }


                                                                        Console.WriteLine("via points added successfully\n");
                                                                        Console.WriteLine("Offer created\n");
                                                                        Console.WriteLine("offerID:" + offer.ID);
                                                                        break;
                                                                    }
                                                                    catch
                                                                    {
                                                                        Console.WriteLine("invalid input\n");
                                                                        goto numberOfViaPoints;
                                                                    }
                                                                }
                                                                catch
                                                                {
                                                                    Console.WriteLine("invalid input\n");
                                                                    goto Date;
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto costPerPoint;
                                                            }


                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("invalid input\n");
                                                            goto NumberOfSeats;
                                                        }

                                                        }
                           case Options.vechicleBooking:
                                                    {
                                                        try
                                                        {
                                                            fromLocation:
                                                            Console.WriteLine("Enter from location: ");
                                                            string fromLocation = Console.ReadLine();
                                                            if (string.IsNullOrEmpty(fromLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto fromLocation;
                                                            }
                                                            toLocation:
                                                            Console.WriteLine("Enter to location: ");
                                                            string toLocation = Console.ReadLine();
                                                            if (string.IsNullOrEmpty(toLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto toLocation;
                                                            }
                                                        numberOfSeats:
                                                            try
                                                            {
                                                                Console.WriteLine("Enter number of seats to book\n");
                                                                int numberOfSeats = Convert.ToInt32(Console.ReadLine());
                                                            Date:
                                                                try
                                                                {
                                                                    Console.WriteLine("Enter date  in yyyy/mm/day  formate\n");
                                                                    DateTime dateTime = DateTime.Parse(Console.ReadLine());
                                                                    if (DateTime.Compare(dateTime, DateTime.Now) < 0)
                                                                    {
                                                                        Console.WriteLine("Please give valid Date and Time\n");
                                                                        goto Date;
                                                                    }
                                                                    List<Offer> availableOffers = offerServices.GetListOfAvilableOffers(fromLocation, toLocation, locations, numberOfSeats, dateTime);
                                                                    if (availableOffers.Count > 0)
                                                                    {
                                                                       select:
                                                                        Console.WriteLine("select  offer\n");
                                                                        Console.WriteLine("select 0 to exit\n");
                                                                        int index = 1;
                                                                        foreach(var offer in availableOffers)
                                                                        {
                                                                            Console.WriteLine("{0}.Price:{1}\tCar:{2}\tTime:{3}\t", index, offer.Price,offer.CarID, offer.DateTime);
                                                                                index++;

                                                                        }
                                                                       
                                                                        try
                                                                        {
                                                                            int selectedOption = Convert.ToInt32(Console.ReadLine());
                                                                            if (selectedOption != 0)
                                                                            {
                                                                                Offer selectedOffer = availableOffers[selectedOption - 1];
                                                                                Booking bookingRequest = new Booking(user.ID, fromLocation, toLocation, selectedOffer.ID, numberOfSeats, dateTime);
                                                                                if (bookingServices.Add(bookingRequest))
                                                                                {
                                                                                    Console.WriteLine("Booking reqest sent :)\n");
                                                                                    break;
                                                                                }
                                                                                else
                                                                                {
                                                                                    Console.WriteLine("Booking request failed\n");
                                                                                    break;
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                break;
                                                                            }
                                                                        }
                                                                        catch
                                                                        {
                                                                            Console.WriteLine("Invalid inpt\n");
                                                                            goto select;
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("There are no available offers\n");
                                                                        break;
                                                                    }
                                                                }
                                                                catch
                                                                {
                                                                    Console.WriteLine("Invalid inpt\n");
                                                                    goto Date;
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                Console.WriteLine("Invalid inpt\n");
                                                                goto numberOfSeats;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Invalid inpt\n");
                                                            break;
                                                        }
                                                    }
                                                case Options.ViewAllBookings:
                                                    {
                                                        List<Booking> bookingHistory = bookingServices.GetAllbookings(user.ID);
                                                        if (bookingHistory.Count > 0)
                                                        {
                                                            int index = 1;
                                                            foreach (var booking in bookingHistory)
                                                            {
                                                                Console.WriteLine("{0}.Frompoint:{1}\tToPoint:{2}\tPrice:{3}\tDriver:{4}\tstatus:{5}\tDateTime:{6}", index, booking.FromPoint, booking.ToPoint, booking.Price, booking.OfferID, booking.Status, booking.DateTime);
                                                                index++;
                                                            }
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("There are no bookings\n");
                                                            break;
                                                        }
                                                    }
                                                case Options.ViewAllOffers:
                                                    {
                                                        try
                                                        {
                                                            bool entry = true;
                                                            while (entry)
                                                            {


                                                                Console.WriteLine("view all offers\n");
                                                                List<Offer> offers = offerServices.GetAllOffers(user.ID);

                                                                if (offers != null)
                                                                {
                                                                    Console.WriteLine("select offer\n0.exit");
                                                                    int index = 1;
                                                                    foreach (var offer in offers)
                                                                    {
                                                                        Console.WriteLine("{0}.frompoint:{1}\ttoPoint:{2}\tstatus:{3}\tridestatus:{4}",index,offer.FromPoint,offer.ToPoint,offer.Status,offer.RideStatus);
                                                                        index++;
                                                                    }
                                                                    int offerNumber = Convert.ToInt32(Console.ReadLine());

                                                                    if (offerNumber != 0)
                                                                    {
                                                                        Offer selectedOffer = offers[offerNumber - 1];
                                                                       
                                                                        try
                                                                        {
                                                                            Console.WriteLine("1.Approval of reqests\n2.start ride\n3.End ride\n4.Close offer\n\n5.Exit\n");

                                                                            OfferOption option = (OfferOption)Convert.ToInt32(Console.ReadLine());
                                                                            switch (option)
                                                                            {
                                                                                case OfferOption.Approve:
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            int numberOfSeats = selectedOffer.AvailableSeats;
                                                                                            if (numberOfSeats > 0)
                                                                                            {
                                                                                                Console.WriteLine("approve reqest\n");

                                                                                                List<Booking> bookingRequests = bookingServices.GetRequests(selectedOffer.ID);
                                                                                                if (bookingRequests.Count > 0)
                                                                                                {

                                                                                                    int index2 = 1;
                                                                                                    Console.WriteLine("0. Exit");
                                                                                                    foreach (var request in bookingRequests)
                                                                                                    {
                                                                                                        if (request.NumberOfseats < selectedOffer.AvailableSeats)
                                                                                                        {
                                                                                                            Console.WriteLine("{0}. From Point:{1}\tToPoint:{2} \n", index2, request.FromPoint, request.ToPoint);
                                                                                                            index2++;
                                                                                                        }

                                                                                                    }
                                                                                                    int approvedRequest = Convert.ToInt32(Console.ReadLine());
                                                                                                    if (approvedRequest > 0 && index2 != 1)
                                                                                                    {
                                                                                                        Booking bookingRequest = bookingRequests[approvedRequest - 1];
                                                                                                        if (offerServices.ApprovalOfBooking(bookingRequest, selectedOffer, locations))
                                                                                                        {
                                                                                                            Console.WriteLine("Booking Approved\n");
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            Console.WriteLine("Booking not Approved\n");
                                                                                                        }
                                                                                                        break;
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        break;
                                                                                                    }
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Console.WriteLine("There are no requests\n");
                                                                                                    break;
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("There are no available seats\n");
                                                                                                break;
                                                                                            }

                                                                                        }
                                                                                        catch
                                                                                        {
                                                                                            Console.WriteLine("Invalid Input\n");
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                case OfferOption.startRide:
                                                                                    {
                                                                                        if (selectedOffer.Status.Equals(OfferStatus.open))
                                                                                        {
                                                                                            if (DateTime.Compare(selectedOffer.DateTime, DateTime.Now) < 0)
                                                                                            {
                                                                                                List<Booking> bookings = bookingServices.GetAllRidesToStart(selectedOffer.ID);
                                                                                                if (bookings.Count != 0)
                                                                                                {
                                                                                                    offerServices.StartRide(selectedOffer, bookings);
                                                                                                    Console.WriteLine("ride started\n");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Console.WriteLine("There are no bookings \n");
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("still there is some time to start ride");
                                                                                            }
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                case OfferOption.EndRide:
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            List<Booking> bookings = bookingServices.GetAllRidesToEnd(selectedOffer.ID);
                                                                                            if (bookings.Count != 0)
                                                                                            {

                                                                                                if (offerServices.EndRide(selectedOffer, bookings))
                                                                                                {

                                                                                                    Console.WriteLine("Ride ended\nThank You:)\n");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Console.WriteLine("Sorry:(\nRide not ended\n");
                                                                                                }
                                                                                                break;

                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("There are no bookings \n");
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                        catch
                                                                                        {
                                                                                            Console.WriteLine("Invalid Input\n");
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                case OfferOption.CloseOffer:
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (offerServices.CloseOffer(selectedOffer))
                                                                                            {
                                                                                                Console.WriteLine("Offer Closed\n");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("Offer closing failed\n");
                                                                                            }

                                                                                            break;
                                                                                        }
                                                                                        catch
                                                                                        {
                                                                                            Console.WriteLine("Invalid Input\n");
                                                                                            break;
                                                                                        }
                                                                                    }

                                                                                case OfferOption.Exit:
                                                                                    {
                                                                                        entry = false;
                                                                                        break;
                                                                                    }
                                                                                default:
                                                                                    {
                                                                                        break;
                                                                                    }
                                                                            }
                                                                        }

                                                                        catch
                                                                        {
                                                                            Console.WriteLine("ivalid input\n");
                                                                            break;
                                                                        }

                                                                    }
                                                                    else
                                                                    {
                                                                        break;
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("There are no offers\n");
                                                                    break;
                                                                }
                                                            }
                                                            
                                                        }
                                                        catch(Exception ex)
                                                        {
                                                            Console.WriteLine(ex);
                                                        }
                                                        break;
                                                    }
                                                case Options.Logout:
                                                    {
                                                        Console.WriteLine("Thankyou:)\n");
                                                        goto Login;
                                                    }
                                                default:
                                                    {
                                                        break;
                                                    }
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("invalid input\n");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("invalid userId & password\n");
                                }
                                break;
                            }
                    }
                }
                catch
                {
                    Console.WriteLine("invalid input\n");

                }
            }
        }
    }
}