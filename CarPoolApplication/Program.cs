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
           while(true)
            { UserServices userServices = new UserServices();
                OfferServices offerServices = new OfferServices();
                UtilServises utilServices = new UtilServises();
                BookingServices bookingServices = new BookingServices();
                List<string> locations = utilServices.locations;
                Console.WriteLine("1.signup\n2.login\n");
                int LoginPageOption = Convert.ToInt32(Console.ReadLine());
                switch (LoginPageOption)
                {
                   
                    case 1:
                        {
                            Console.WriteLine("Enter name\n");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter password\n");
                            String password = Console.ReadLine();
                            Console.WriteLine("Enter phoneNumber\n");
                            decimal PhoneNumber = Convert.ToDecimal(Console.ReadLine());
                            User user = new User()
                            {
                                Name = name,
                                Password = password,
                                PhoneNumber = PhoneNumber
                            };
                            user = userServices.CreateUser(user);
                            Console.WriteLine("account created\nuserId:" + user.ID);
                            goto case 2;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter UserIDn");
                            string userID = Console.ReadLine();
                            Console.WriteLine("Enter password\n");
                            String password = Console.ReadLine();
                            if(userServices.IsValidUser(userID, password))
                            {
                                User user = userServices.GetUser(userID);
                                Console.WriteLine("1.Create offer\n2.Book a car\n3.View all bookings\n4.View all created offers\n5.Log out ");
                                int choice = Convert.ToInt32(Console.ReadLine());
                                switch(choice)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("1.create offer\n2.Approval of reqests\n3.Exit\n");
                                            int option = Convert.ToInt32(Console.ReadLine());
                                            switch (option)
                                            {
                                                case 1:
                                                    {
                                                        Console.WriteLine("Enter car number\n");
                                                        string carNumber = Console.ReadLine();
                                                        Console.WriteLine("Enter car name\n");
                                                        string carName = Console.ReadLine();
                                                        int numberOfSeats = Convert.ToInt32(Console.ReadLine());
                                                        Vehicle carDetails = new Vehicle(carName, carNumber, userID, numberOfSeats);
                                                        Console.WriteLine("Enter Available seats\n");
                                                        int availableSeats = Convert.ToInt32(Console.ReadLine());

                                                        Console.WriteLine("select from point\n");
                                                        for (int index = 0; index < locations.Count; index++)
                                                        {
                                                            Console.WriteLine(index + 1 + "." + locations[index] + "\n");

                                                        }
                                                        Console.WriteLine("Enter from location: ");
                                                        string FromLocation = Console.ReadLine();
                                                        Console.WriteLine("Enter to location: ");
                                                        string ToLocation = Console.ReadLine();
                                                        LocationServices LocationService = new LocationServices();
                                                        List<string> ViaPoints = LocationService.GetViaPoints(FromLocation, ToLocation);
                                                        offerServices.AddViapoint(FromLocation);
                                                        int point;
                                                        do
                                                        {
                                                            int index = 1;
                                                            foreach (var viaPoint in ViaPoints)
                                                            {
                                                                Console.WriteLine(index + "." + viaPoint);
                                                                index++;
                                                            }
                                                            point = Convert.ToInt32(Console.ReadLine());
                                                            offerServices.AddViapoint(ViaPoints[index - 1]);
                                                        } while (point != 0);
                                                        offerServices.AddViapoint(ToLocation);
                                                        Offer offer = new Offer()
                                                        {
                                                            FromPoint = FromLocation,
                                                            ToPoint = ToLocation,
                                                            CarID = carDetails.ID,
                                                            viaPoints = offerServices.viapoints,
                                                            AvailableSeats = availableSeats
                                                        };
                                                        foreach (var location in offerServices.viapoints)
                                                        {
                                                            offerServices.viapoints.Remove(location);
                                                        }
                                                        Offer newoffer = offerServices.CreateOffer(offer, user);
                                                        Console.WriteLine("Offer created\n");
                                                        Console.WriteLine("offerID:" + newoffer.ID);
                                                        
                                                        break;
                                                    }
                                                case 2:
                                                    { 
                                                        
                                                        Console.WriteLine("view all requets and approve\n");
                                                        Console.WriteLine("Enter offerID\n");
                                                        string offerID = Console.ReadLine();
                                                        Offer offer = offerServices.GetOffer(offerID);
                                                        while (offer.AvailableSeats != 0)
                                                        {
                                                            List<BookingRequest> bookingRequests = bookingServices.GetRequests(offerID);
                                                            int index = 1;
                                                            foreach (var request in bookingRequests)
                                                            {
                                                                Console.WriteLine("{0}.From Point:{1}  ToPoint:{2} \n", index, request.FromPoint, request.ToPoint);
                                                                index++;
                                                            }
                                                            int approvedRequest = Convert.ToInt32(Console.ReadLine());
                                                            BookingRequest bookingRequest = bookingRequests[approvedRequest + 1];
                                                            offerServices.ApprovalOfBooking(bookingRequest, offer);
                                                        }
                                                        break;
                                                    }
                                               
                                                default:
                                                    {
                                                        break;
                                                    }

                                            }
                                            break;
                                        }
                                        
                                    case 2:
                                        {
                                            Console.WriteLine("1.book a vehicle\n2.view all bookings\n3.Exit\n");
                                            int option = Convert.ToInt32(Console.ReadLine());
                                            switch (option)
                                            {
                                                case 1:
                                                    {
                                                        Console.WriteLine("select from point\n");
                                                        for (int index = 0; index < locations.Count; index++)
                                                        {
                                                            Console.WriteLine(index + 1 + "." + locations[index] + "\n");
                                                        }
                                                        int point = Convert.ToInt32(Console.ReadLine());
                                                        string fromPoint = locations[point];
                                                        Console.WriteLine("select to point\n");
                                                        for (int index = point; index < locations.Count; index++)
                                                        {
                                                            Console.WriteLine(index + 1 + "." + locations[index] + "\n");
                                                        }
                                                        int point2 = Convert.ToInt32(Console.ReadLine());
                                                        string toPoint = locations[point2];
                                                        List<Offer> availableOffers = offerServices.GetListOfAvilableOffers(fromPoint, toPoint);
                                                        Console.WriteLine("select to offer\n");
                                                        for (int index = 0; index <= availableOffers.Count; index++)
                                                        {
                                                            Console.WriteLine(index + 1 + "." + locations[index] + "\n");
                                                        }
                                                        int selectedOption = Convert.ToInt32(Console.ReadLine());
                                                        Offer selectedOffer = availableOffers[selectedOption];
                                                        BookingRequest bookingRequest = new BookingRequest()
                                                        {
                                                            passengerID = user.ID,
                                                            FromPoint = fromPoint,
                                                            ToPoint = toPoint,
                                                            OfferID = selectedOffer.ID
                                                        };
                                                        bookingServices.createBookingReqest(bookingRequest);
                                                        break;
                                                    }
                                                case 2:
                                                    {

                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                            }
                            break;

                        }
                }
            }
        }
    }
}
