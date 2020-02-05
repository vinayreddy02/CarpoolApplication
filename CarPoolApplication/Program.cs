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
                LoginOption LoginPageOption =(LoginOption) Convert.ToInt32(Console.ReadLine());
                switch (LoginPageOption)
                {

                    case LoginOption.signup:
                        {
                            Console.WriteLine("Enter name\n");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter password\n");
                            String passWord = Console.ReadLine();
                            Console.WriteLine("Enter phoneNumber\n");
                            decimal phoneNumber = Convert.ToDecimal(Console.ReadLine());
                            User user = new User(name, passWord, phoneNumber);
                            userServices.Add(user);
                            Console.WriteLine("account created\nuserId:" + user.ID);
                            break;
                        }
                    case LoginOption.login:
                        {
                            Console.WriteLine("Enter UserIDn");
                            string userID = Console.ReadLine();
                            Console.WriteLine("Enter password\n");
                            String password = Console.ReadLine();
                            if (userServices.IsValidUser(userID, password))
                            {
                                User user = userServices.GetUser(userID);
                                while (true)
                                {   Main:
                                    Console.WriteLine("1.For Creating offer\n2.Book a car\n3.View all bookings\n4.View all created offers\n5.Log out\n");
                                    Options choice =(Options)Convert.ToInt32(Console.ReadLine());
                                    switch (choice)
                                    {
                                        case Options.forcreatingOffer:
                                            {
                                                while (true)
                                                {
                                                    Console.WriteLine("1.create offer\n2.Approval of reqests\n3.Exit\n");
                                                    OfferOption option =(OfferOption) Convert.ToInt32(Console.ReadLine());
                                                    switch (option)
                                                    {
                                                        case OfferOption.Create:
                                                            {
                                                                Console.WriteLine("Enter car number\n");
                                                                string vehicleNumber = Console.ReadLine();
                                                                Console.WriteLine("Enter car name\n");
                                                                string vehicleName = Console.ReadLine();
                                                                Console.WriteLine("Enter number of seats");
                                                                int numberOfSeats = Convert.ToInt32(Console.ReadLine());
                                                                Vehicle vehicle = new Vehicle(vehicleNumber, vehicleName, userID, numberOfSeats);
                                                                Console.WriteLine("Enter Available seats\n");
                                                                int availableSeats = Convert.ToInt32(Console.ReadLine());
                                                                Console.WriteLine("Enter from location: ");
                                                                string FromLocation = Console.ReadLine();
                                                                Console.WriteLine("Enter to location: ");
                                                                string ToLocation = Console.ReadLine();
                                                                Console.WriteLine("Enter costper point\n");
                                                                int costPerPoint = Convert.ToInt32(Console.ReadLine());
                                                                Console.WriteLine("Enter date and time in (yy/mm/day hr:min am/pm) formate\n");
                                                                DateTime dateTime = DateTime.Parse(Console.ReadLine());
                                                                Offer offer = new Offer(user.ID, FromLocation, ToLocation, vehicle.ID, availableSeats, costPerPoint, dateTime);
                                                                offerServices.Add(offer);
                                                                Console.WriteLine("Offer created\n");
                                                                Console.WriteLine("offerID:" + offer.ID);
                                                                Console.WriteLine("Enter number of via points\n");
                                                                int numberOfViaPoints = Convert.ToInt32(Console.ReadLine());                                                              
                                                                Console.WriteLine("Enter viapoints\n");
                                                                int start = 1;
                                                                locationService.Add(new Location(FromLocation, offer.ID, start));
                                                                while (numberOfViaPoints != 0)
                                                                {
                                                                    string place = Console.ReadLine();                                                          
                                                                    locationService.Add(new Location(place, offer.ID,));
                                                                    numberOfViaPoints--;
                                                                }
                                                                locationService.Add(new Location(FromLocation,offer.ID,start+numberOfViaPoints));
                                                                Console.WriteLine("via points added successfully\n");
                                                                break;
                                                            }                                                       
                                                        case OfferOption.Approve:
                                                            {
                                                                Console.WriteLine("view all requets and approve\n");
                                                                Console.WriteLine("Enter offerID\n");
                                                                string offerID = Console.ReadLine();
                                                                Offer offer = offerServices.GetOffer(offerID);
                                                                Console.WriteLine("approve reqest\n");
                                                                int numberOfSeats = offer.AvailableSeats;
                                                                List<Booking> bookingRequests = bookingServices.GetRequests(offerID);
                                                                if (numberOfSeats != 0)
                                                                {                                                                  
                                                                    int index = 1;
                                                                    foreach (var request in bookingRequests)
                                                                    {
                                                                        Console.WriteLine("{0}.From Point:{1}  ToPoint:{2} \n", index, request.FromPoint, request.ToPoint);
                                                                        index++;
                                                              
                                                                    }
                                                                   
                                                                }
                                                                int approvedRequest = Convert.ToInt32(Console.ReadLine());
                                                                Booking bookingRequest = bookingRequests[approvedRequest - 1];
                                                                offerServices.ApprovalOfBooking(bookingRequest, offer, locations);
                                                                bookingRequests.Remove(bookingRequest);                                                        
                                                                break;
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

                                            }

                                        case Options.vechicleBooking:
                                            {

                                                Console.WriteLine("Enter from location: ");
                                                string FromLocation = Console.ReadLine();
                                                Console.WriteLine("Enter to location: ");
                                                string ToLocation = Console.ReadLine();
                                               
                                                List<Offer> availableOffers = offerServices.GetListOfAvilableOffers(FromLocation, ToLocation, locations);
                                                Console.WriteLine("select  offer\nSNo.costperPoint\n");
                                                for (int index = 0; index < availableOffers.Count; index++)
                                                {
                                                    Console.WriteLine(index + 1 + "." + availableOffers[index].CostPerPoint + "\n");
                                                }
                                                int selectedOption = Convert.ToInt32(Console.ReadLine());
                                                Offer selectedOffer = availableOffers[selectedOption];
                                                Booking bookingRequest = new Booking(user.ID, FromLocation, ToLocation, selectedOffer.ID);                                              
                                                bookingServices.Add(bookingRequest);
                                                break;
                                            }
                                        case Options.viewAllBookings:
                                            {
                                                List<Booking> bookingHistory = bookingServices.GetAllbookings(user.ID);
                                                int index = 1;
                                                foreach (var booking in bookingHistory)
                                                {
                                                    Console.WriteLine("{0}.Frompoint:{1}\tToPoint;{2}\tPrice:{3}\tDriver:{4}", index, booking.FromPoint, booking.ToPoint, booking.Price, booking.OfferID);
                                                    index++;
                                                }
                                                break;
                                            }
                                        case Options.ViewAllOffers:
                                            {
                                                List<Offer> offerhistory = offerServices.GetAllOffers(user.ID);
                                                int index = 1;
                                                foreach (var offer in offerhistory)
                                                {
                                                    Console.WriteLine("{0}.Frompoint:{1}\tToPoint;{2}\tPrice:{3}\t", index, offer.FromPoint, offer.ToPoint, offer.CostPerPoint);
                                                    index++;
                                                }
                                                break;
                                            }
                                        case Options.Logout:
                                            {
                                                goto Login;
                                            }
                                        default:
                                            {
                                                break;
                                            }

                                    }
                                }
                            }
                            break;

                        }

                }
            }
        }
    }
}