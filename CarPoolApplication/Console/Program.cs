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
                try { 
                LoginOption LoginPageOption =(LoginOption) Convert.ToInt32(Console.ReadLine());
                    switch (LoginPageOption)
                    {

                        case LoginOption.signup:
                            {
                                Console.WriteLine("Enter name");
                                string name = Console.ReadLine();
                                Console.WriteLine("Enter password");
                                String passWord = Console.ReadLine();
                                Console.WriteLine("Enter phoneNumber");
                                decimal phoneNumber = Convert.ToDecimal(Console.ReadLine());
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
                                    { Main:
                                        try
                                        {
                                            Console.WriteLine("Welcome..choose an option\n");
                                            Console.WriteLine("1.Create Offer\n2.Book a car\n3.View all bookings\n4.View all created offers\n5.Log out\n");
                                            Options choice = (Options)Convert.ToInt32(Console.ReadLine());
                                            switch (choice)
                                            {
                                                case Options.createOffer:
                                                    { 
                                                    
                                                        try
                                                        {
                                                            Console.WriteLine("Enter car number\n");
                                                            string vehicleNumber = Console.ReadLine();
                                                            Console.WriteLine("Enter car name\n");
                                                            string vehicleName = Console.ReadLine();
                                                            Console.WriteLine("Enter number of seats");
                                                            int numberOfSeats = Convert.ToInt32(Console.ReadLine());
                                                            Vehicle vehicle = new Vehicle(vehicleNumber, vehicleName, userID, numberOfSeats);
                                                            Console.WriteLine("vechile added successfully\n");
                                                            Console.WriteLine("Enter Number of Available seats for ride\n");
                                                            int availableSeats = Convert.ToInt32(Console.ReadLine());
                                                            if (availableSeats < numberOfSeats)
                                                            {
                                                                Console.WriteLine("Enter from location: ");
                                                                string FromLocation = Console.ReadLine();
                                                                Console.WriteLine("Enter to location: ");
                                                                string ToLocation = Console.ReadLine();
                                                                Console.WriteLine("Enter costper point\n");
                                                                int costPerPoint = Convert.ToInt32(Console.ReadLine());
                                                                Console.WriteLine("Enter date and time in (yyyy/mm/day hr:min am/pm) formate\n");
                                                                DateTime dateTime = DateTime.Parse(Console.ReadLine());
                                                                Offer offer = new Offer(user.ID, FromLocation, ToLocation, vehicle.ID, availableSeats, costPerPoint, dateTime);
                                                                offerServices.Add(offer);
                                                                Console.WriteLine("Offer created\n");
                                                                Console.WriteLine("offerID:" + offer.ID);
                                                                Console.WriteLine("Enter number of via points\n");
                                                                int numberOfViaPoints = Convert.ToInt32(Console.ReadLine());
                                                                Console.WriteLine("Enter viapoints\n");
                                                                int start = 1;
                                                                if (!locationService.Add(new Location(FromLocation, offer.ID, start)))
                                                                {
                                                                    Console.WriteLine("from location not saved\n");
                                                                }
                                                                while (numberOfViaPoints != 0)
                                                                {
                                                                    string place = Console.ReadLine();
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
                                                                if (!locationService.Add(new Location(ToLocation, offer.ID, numberOfViaPoints + 2)))
                                                                {
                                                                    Console.WriteLine("To location not saved\n");
                                                                }
                                                                Console.WriteLine("via points added successfully\n");
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine(" you can not create an offer for more than total number of seats of vechicle\n");
                                                                break;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("invalid input\n");
                                                            break;
                                                        }
                                                        
                                                    }
                                                    

                                                case Options.vechicleBooking:
                                                    {
                                                        try
                                                        {
                                                            Console.WriteLine("Enter from location: ");
                                                            string fromLocation = Console.ReadLine();
                                                            Console.WriteLine("Enter to location: ");
                                                            string toLocation = Console.ReadLine();
                                                            Console.WriteLine("Enter number of seats to book\n");
                                                            int numberOfSeats = Convert.ToInt32(Console.ReadLine());
                                                            Console.WriteLine("Enter date  in yyyy/mm/day  formate\n");
                                                            DateTime dateTime = DateTime.Parse(Console.ReadLine());
                                                            List<Offer> availableOffers = offerServices.GetListOfAvilableOffers(fromLocation, toLocation, locations,numberOfSeats,dateTime);
                                                            if (availableOffers.Count > 0)
                                                            {
                                                                Console.WriteLine("select 0 to exit\n");
                                                                Console.WriteLine("select  offer\nSNo. costperPoint\t ID\t datetime\n");
                                                                for (int index = 0; index < availableOffers.Count; index++)
                                                                {
                                                                    Console.WriteLine(index + 1 + ". " + availableOffers[index].CostPerPoint +"\t"+ availableOffers[index].ID+ availableOffers[index].DateTime+ "\n");
                                                                }
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
                                                            else
                                                            {
                                                                Console.WriteLine("There are no available offers\n");
                                                                break;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            Console.WriteLine("Invalid inpt\n");
                                                            break;
                                                        }
                                                    }
                                                case Options.viewAllBookings:
                                                    {
                                                        List<Booking> bookingHistory = bookingServices.GetAllbookings(user.ID);
                                                        if (bookingHistory.Count > 0)
                                                        {
                                                            int index = 1;
                                                            foreach (var booking in bookingHistory)
                                                            {
                                                                Console.WriteLine("{0}.Frompoint:{1}\tToPoint;{2}\tPrice:{3}\tDriver:{4}\tstatus:{5}\tDateTime:{6}", index, booking.FromPoint, booking.ToPoint, booking.Price, booking.OfferID, booking.Status,booking.DateTime);
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
                                                        while (true)
                                                        {
                                                            Console.WriteLine("view all offers\n");
                                                            List<Offer> offers = offerServices.GetAllOffers(user.ID);
                                                            if (offers != null)
                                                            {
                                                                Console.WriteLine("select offer\n0.exit\n");
                                                                int index = 1;
                                                                foreach (var offer in offers)
                                                                {
                                                                    Console.WriteLine("{0}.Frompoint:{1}\tToPoint;{2}\tPrice:{3}\tOffer ID:{4}\tstatus:{5}\tDateTime:{6}", index, offer.FromPoint, offer.ToPoint, offer.CostPerPoint, offer.ID, offer.status, offer.DateTime);
                                                                    index++;
                                                                }
                                                                int offerNumber = Convert.ToInt32(Console.ReadLine());
                                                                if (offerNumber != 0)
                                                                {
                                                                    Offer selectedOffer = offers[offerNumber - 1];
                                                                    try
                                                                    {
                                                                        if (selectedOffer.status.Equals(OfferStatus.open))
                                                                        {
                                                                            Console.WriteLine("1.Approval of reqests\n2.End ride\n3.Close offer\n4.View All rides\n5.Exit\n");
                                                                        }
                                                                        else
                                                                        {
                                                                            Console.WriteLine("4.view All Rides\n5.Exit\n");
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

                                                                                            List<Booking> bookingRequests = bookingServices.GetRequests(selectedOffer.ID);
                                                                                            if (bookingRequests.Count > 0)
                                                                                            {

                                                                                                int index2 = 1;
                                                                                                Console.WriteLine("0. Exit\n");
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
                                                                            case OfferOption.EndRide:
                                                                                {
                                                                                    try
                                                                                    {


                                                                                        List<Booking> bookings = bookingServices.GetAllRides(selectedOffer.ID);
                                                                                        if (bookings.Count != 0)
                                                                                        {
                                                                                            int index2 = 1;
                                                                                            Console.WriteLine("select 0 to exit\n");
                                                                                            foreach (var booking in bookings)
                                                                                            {

                                                                                                Console.WriteLine("{0}.Booking Id:{1}\tFrom:{2}\tTo:{3}\tPassenger:{4}", index2, booking.ID, booking.FromPoint, booking.ToPoint, booking.PassengerID);
                                                                                                index2++;
                                                                                            }
                                                                                            int rideNumber = Convert.ToInt32(Console.ReadLine());
                                                                                            if (rideNumber != 0)
                                                                                            {
                                                                                                Booking ride = bookings[rideNumber - 1];
                                                                                                if (offerServices.EndRide(ride))
                                                                                                {
                                                                                                    selectedOffer.AvailableSeats += 1;
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
                                                                                                break;
                                                                                            }
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

                                                                                        List<Booking> bookings = bookingServices.GetAllRides(selectedOffer.ID);
                                                                                        if (bookings.Count == 0)
                                                                                        {
                                                                                            if (offerServices.CloseOffer(selectedOffer))
                                                                                            {
                                                                                                Console.WriteLine("Offer Closed\n");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                Console.WriteLine("Offer closing failed\n");
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Console.WriteLine("There are  some rides to end\n");
                                                                                        }
                                                                                        break;

                                                                                    }
                                                                                    catch
                                                                                    {
                                                                                        Console.WriteLine("Invalid Input\n");
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            case OfferOption.ViewAllRides:
                                                                                {
                                                                                    try
                                                                                    {

                                                                                        List<Booking> AllRides = bookingServices.GetRides(selectedOffer.ID);


                                                                                        if (AllRides.Count > 0)
                                                                                        {
                                                                                            int index2 = 1;
                                                                                            foreach (var booking in AllRides)
                                                                                            {
                                                                                                Console.WriteLine("{0}.Frompoint:{1}\tToPoint:{2}\tPrice:{3}\tID:{4}\tstatus:{5}\tDateTime:{6}", index2, booking.FromPoint, booking.ToPoint, booking.Price, booking.ID, booking.Status, booking.DateTime);
                                                                                                index2++;
                                                                                            }
                                                                                            break;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            Console.WriteLine("There are no bookings\n");
                                                                                            break;
                                                                                        }

                                                                                    }
                                                                                    catch
                                                                                    {
                                                                                        Console.WriteLine("Invalid Input\n");
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            case OfferOption.Exit:
                                                                                {
                                                                                    goto Main;
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