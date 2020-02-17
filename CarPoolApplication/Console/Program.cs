using System;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using System.Collections.Generic;
using CarPoolApplication.Interfaces;

namespace CarPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserServices();
            IOfferService offerService = new OfferServices();
            ILocationService locationService = new LocationServices();
            IBookingService bookingService = new BookingServices();
           
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
                                string name = Console.ReadLine().ToUpper();
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
                                if (userService.AddUser(user))
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
                                string userID = Console.ReadLine().ToUpper();
                                Console.WriteLine("Enter password");
                                String password = Console.ReadLine();
                                if (userService.IsValidUser(userID, password))
                                {
                                    User user = userService.GetUser(userID);
                                    
                                    while (true)
                                    {
                                    
                                        try
                                        {
                                            Console.WriteLine("Welcome..choose an option\n");
                                            Console.WriteLine("1.Create Offer\n2.Book a car\n3.View all created offers\n4.View all bookings\n5.Log out\n");
                                            Options choice = (Options)Convert.ToInt32(Console.ReadLine());
                                            switch (choice)
                                            {
                                                case Options.createOffer:
                                                    {
                                                        List<string> places = new List<string>();
                                                    CarNumber:

                                                        Console.WriteLine("Enter car number\n");
                                                        string vehicleNumber = Console.ReadLine().ToUpper();
                                                        if (string.IsNullOrEmpty(vehicleNumber))
                                                        {
                                                            Console.WriteLine("invalid input\n");
                                                            goto CarNumber;
                                                        }
                                                    CarName:
                                                        Console.WriteLine("Enter car name\n");
                                                        string vehicleName = Console.ReadLine().ToUpper();
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
                                                            string fromLocation = Console.ReadLine().ToUpper();
                                                            if (string.IsNullOrEmpty(fromLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto FromLocation;
                                                            }
                                                           
                                                        ToLocation:

                                                            Console.WriteLine("Enter to location: ");
                                                            string toLocation = Console.ReadLine().ToUpper();

                                                                if (string.IsNullOrEmpty(toLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto ToLocation;
                                                            }
                                                                else if(toLocation.Equals(fromLocation))
                                                            {
                                                                Console.WriteLine("you can not have same place as from location and to location");
                                                                goto ToLocation;
                                                            }
                                                            
                                                        numberOfViaPoints:
                                                            try
                                                            {
                                                            
                                                                Console.WriteLine("Enter number of via points\n");
                                                                int numberOfViaPoints = Convert.ToInt32(Console.ReadLine());
                                                                Console.WriteLine("Enter viapoints\n");
                                                                int start = 1;
                                                                int lastStation = numberOfViaPoints + 2;
                                                                while (numberOfViaPoints != 0)
                                                                {
                                                                viapoint:

                                                                    string place = Console.ReadLine().ToUpper();
                                                                    if (string.IsNullOrEmpty(place))
                                                                    {
                                                                        Console.WriteLine("invalid input\n");
                                                                        goto viapoint;
                                                                    }

                                                                    if (!places.Contains(place)&&place!=fromLocation&&place!=toLocation)
                                                                    {
                                                                        places.Add(place);
                                                                        numberOfViaPoints--;

                                                                    }
                                                                    else
                                                                    {
                                                                        Console.WriteLine("you can not add a place which is already added\n");
                                                                    }
                                                                }
                                                            costPerPoint:
                                                                try
                                                                {
                                                                    Console.WriteLine("Enter costper point\n");
                                                                    int costPerPoint = Convert.ToInt32(Console.ReadLine());
                                                                Date:

                                                                    Console.WriteLine("Enter date and time\n");

                                                                    try
                                                                    {
                                                                        DateTime dateTime = DateTime.Parse(Console.ReadLine());

                                                                        if (DateTime.Compare(dateTime, DateTime.Now) < 0)
                                                                        {
                                                                            Console.WriteLine("Please give valid Date and Time\n");
                                                                            goto Date;
                                                                        }

                                                                        Offer offer = new Offer(user.ID, fromLocation, toLocation, vehicle.ID, numberOfSeats, costPerPoint, dateTime);
                                                                   
                                                                            if (!locationService.AddLocation(new Location(fromLocation, offer.ID, start)))
                                                                            {
                                                                                Console.WriteLine("from location not saved\n");
                                                                            }
                                                                            if (!locationService.AddLocation(new Location(toLocation, offer.ID, lastStation)))
                                                                            {
                                                                                Console.WriteLine("To location not saved\n");
                                                                            }
                                                                            foreach(var place in places)
                                                                        {
                                                                            if (!locationService.AddLocation(new Location(place, offer.ID, ++start)))
                                                                            {
                                                                                Console.WriteLine(" location not saved\n");
                                                                            }
                                                                        }

                                                                        places.Clear();
                                                                        if (offerService.AddOffer(offer))
                                                                        {
                                                                            Console.WriteLine("Offer created\n");
                                                                            Console.WriteLine("offerID:" + offer.ID);
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("Offer creation failed\n");
                                                                        }
                                                                            break;
                                                                        
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
                                                                goto numberOfViaPoints;
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
                                                            string fromLocation = Console.ReadLine().ToUpper();
                                                            if (string.IsNullOrEmpty(fromLocation))
                                                            {
                                                                Console.WriteLine("invalid input\n");
                                                                goto fromLocation;
                                                            }
                                                            toLocation:
                                                            Console.WriteLine("Enter to location: ");
                                                            string toLocation = Console.ReadLine().ToUpper();
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
                                                                    Console.WriteLine("Enter date and time\n");
                                                                    DateTime dateTime = DateTime.Parse(Console.ReadLine());
                                                                    if (DateTime.Compare(dateTime, DateTime.Now) < 0)
                                                                    {
                                                                        Console.WriteLine("Please give valid Date and Time\n");
                                                                        goto Date;
                                                                    }
                                                                    else
                                                                    {
                                                                        List<Location> fromLocations = locationService.GetLocations(fromLocation);
                                                                        List<Location> toLocations = locationService.GetLocations(toLocation);
                                                                        List<Offer> availableOffers = offerService.GetAvilableOffers(fromLocation, toLocation, fromLocations, toLocations, numberOfSeats, dateTime);
                                                                        if (availableOffers.Count > 0)
                                                                        {
                                                                        select:
                                                                            Console.WriteLine("choose an aption\n");
                                                                            Console.WriteLine("select 0 to exit");
                                                                            int index = 1;
                                                                            foreach (var offer in availableOffers)
                                                                            {
                                                                                Console.WriteLine("{0}.StartLocation:{1}\tToLocation:{2}\tPricePerSeat:{3}\tCar:{4}\tTime:{5}\n", index, offer.FromPoint, offer.ToPoint, offer.Price, offer.CarID, offer.DateTime);
                                                                                index++;

                                                                            }

                                                                            try
                                                                            {
                                                                                int selectedOption = Convert.ToInt32(Console.ReadLine());
                                                                                if (selectedOption != 0)
                                                                                {
                                                                                    Offer selectedOffer = availableOffers[selectedOption - 1];
                                                                                    Booking bookingRequest = new Booking(user.ID, fromLocation, toLocation, selectedOffer.ID, numberOfSeats, dateTime);
                                                                                    if (bookingService.AddRequest(bookingRequest))
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
                                                        List<Booking> bookingHistory = bookingService.GetAllbookings(user.ID);
                                                        if (bookingHistory.Count > 0)
                                                        {
                                                            int index = 1;
                                                            foreach (var booking in bookingHistory)
                                                            {
                                                                Console.WriteLine("{0}.Frompoint:{1}\tToPoint:{2}\tPrice:{3}\tDriver:{4}\tstatus:{5}\tDateTime:{6}\tNo.of seats:{7}\n", index, booking.FromPoint, booking.ToPoint, booking.Price, booking.OfferID, booking.Status, booking.DateTime,booking.NumberOfseats);
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
                                                                List<Offer> offers = offerService.GetAllOffers(user.ID);

                                                                if (offers != null)
                                                                {
                                                                    Console.WriteLine("choose an option\n0.exit");
                                                                    int index = 1;
                                                                    foreach (var offer in offers)
                                                                    {
                                                                        Console.WriteLine("{0}.frompoint:{1}\ttoPoint:{2}\tstatus:{3}\tridestatus:{4}", index, offer.FromPoint, offer.ToPoint, offer.Status, offer.RideStatus);
                                                                        index++;
                                                                    }
                                                                    int offerNumber = Convert.ToInt32(Console.ReadLine());

                                                                    if (offerNumber != 0)
                                                                    {
                                                                        Offer selectedOffer = offers[offerNumber - 1];

                                                                        try
                                                                        {
                                                                            if (selectedOffer.RideStatus.Equals(RideStatus.NotSarted))
                                                                            {
                                                                                Console.WriteLine("1.Approve  reqests\n2.start ride\n4.cancel ride\n5.Close offer\n6.Exit\n");
                                                                            }
                                                                           else if (selectedOffer.RideStatus.Equals(RideStatus.running))
                                                                            {
                                                                                Console.WriteLine("3.End ride\n6.Exit\n");
                                                                            }
                                                                           else if (selectedOffer.RideStatus.Equals(RideStatus.Compleated) || selectedOffer.RideStatus.Equals(RideStatus.cancel))
                                                                            {
                                                                                Console.WriteLine("6.exit\n");
                                                                            }
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

                                                                                                List<Booking> bookingRequests = bookingService.GetRequests(selectedOffer.ID);
                                                                                                if (bookingRequests.Count > 0)
                                                                                                {

                                                                                                    int index2 = 1;
                                                                                                    Console.WriteLine("0. Exit");
                                                                                                    foreach (var request in bookingRequests)
                                                                                                    {
                                                                                                        if (request.NumberOfseats <= selectedOffer.AvailableSeats)
                                                                                                        {
                                                                                                            Console.WriteLine("{0}. From Point:{1}\tToPoint:{2} \n", index2, request.FromPoint, request.ToPoint);
                                                                                                            index2++;
                                                                                                        }

                                                                                                    }
                                                                                                    int approvedRequest = Convert.ToInt32(Console.ReadLine());
                                                                                                    if (approvedRequest > 0 && index2 != 1)
                                                                                                    {
                                                                                                        Booking bookingRequest = bookingRequests[approvedRequest - 1];
                                                                                                        List<Location> locations = locationService.GetAllLocations(selectedOffer.ID);
                                                                                                        if (offerService.ApprovalOfBooking(bookingRequest, selectedOffer, locations))
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
                                                                                        if (selectedOffer.RideStatus.Equals(RideStatus.NotSarted))
                                                                                        {
                                                                                            if (DateTime.Compare(selectedOffer.DateTime, DateTime.Now) < 0)
                                                                                            {
                                                                                                List<Booking> bookings = bookingService.GetAllRidesToStart(selectedOffer.ID);
                                                                                                if (bookings.Count != 0)
                                                                                                {
                                                                                                    if(offerService.StartRide(selectedOffer)&&bookingService.StartRides(selectedOffer.ID))
                                                                                                        {
                                                                                                        Console.WriteLine("ride started\n");
                                                                                                    }
                                                                                                    {
                                                                                                        Console.WriteLine("ride starting failed\n");
                                                                                                    }
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
                                                                                            if (selectedOffer.RideStatus.Equals(RideStatus.running))
                                                                                            {
                                                                                                
                                                                                                if (offerService.EndRide(selectedOffer)&& bookingService.EndRides(selectedOffer.ID))
                                                                                                {
                                                                                                    Console.WriteLine("Ride ended\nThank You:)\n");
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Console.WriteLine("Sorry:(\nRide not ended\n");
                                                                                                }
                                                                                                break;
                                                                                            }
                                                                                            else if(selectedOffer.RideStatus.Equals(RideStatus.Compleated))
                                                                                            {
                                                                                                Console.WriteLine("Ride already ended \n");
                                                                                                break;
                                                                                            }
                                                                                            else if(selectedOffer.RideStatus.Equals(RideStatus.cancel))
                                                                                                {
                                                                                                Console.WriteLine("Ride canceled\n");
                                                                                                break;
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("Ride not started yet\n");
                                                                                                break;
                                                                                            }

                                                                                        }
                                                                                        catch
                                                                                        {
                                                                                            Console.WriteLine("Invalid Input\n");
                                                                                            break;
                                                                                        }
                                                                                    }
                                                                                case OfferOption.cancel:
                                                                                    {
                                                                                        if (selectedOffer.RideStatus.Equals(RideStatus.NotSarted))
                                                                                        {
                                                                                           ;

                                                                                            if (offerService.CancelRide(selectedOffer) && bookingService.CancelRides(selectedOffer.ID))
                                                                                            {
                                                                                                Console.WriteLine("Ride canceled\n");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("sorry :( not canceled\n");
                                                                                            }
                                                                                        }
                                                                                        else if (selectedOffer.RideStatus.Equals(RideStatus.cancel))
                                                                                        {
                                                                                            Console.WriteLine("Ride  alredy canceled\n");
                                                                                        }
                                                                                        else { 
                                                                                            Console.WriteLine(" you can not cancel now\n");
                                                                                        }
                                                                                        break;
                                                                                    }
                                                                                case OfferOption.CloseOffer:
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (offerService.CloseOffer(selectedOffer))
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
                                                        catch
                                                        {
                                                            Console.WriteLine("invalid input\n");
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