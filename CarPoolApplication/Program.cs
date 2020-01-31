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
                List<string> locations = utilServices.locations;
                Console.WriteLine("1.signup\n2.login\n");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
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
                                Console.WriteLine("1.create offer\n2.Book a car\n3.view all bookings\n4.view all created offers\n5.log out ");
                                int choice = Convert.ToInt32(Console.ReadLine());
                                switch(choice)
                                {
                                    case 1:
                                        {
                                            Console.WriteLine("Enter car number\n");
                                            string carNumber = Console.ReadLine();
                                            Console.WriteLine("Enter car name\n");
                                            string carName = Console.ReadLine();
                                            CarDetails carDetails = new CarDetails(carName, carNumber, userID); 
                                            Console.WriteLine("Enter Available seats\n");
                                            int availableSeats = Convert.ToInt32(Console.ReadLine());

                                            Console.WriteLine("select from point\n");
                                            for(int index = 0;index <locations.Count;index++)
                                            {
                                                Console.WriteLine(index+1+"."+locations[index]+"\n");

                                            }
                                            int point =Convert.ToInt32(Console.ReadLine());
                                              string fromPoint = locations[point];
                                            Console.WriteLine("select to point\n");
                                            for (int index = point; index < locations.Count; index++)
                                            {
                                                Console.WriteLine(index + 1 + "." + locations[index] + "\n");

                                            }
                                            int point2 = Convert.ToInt32(Console.ReadLine());
                                            string toPoint = locations[point2];
                                            //   Console.WriteLine("Add via points\n");
                                            // Console.WriteLine("Number of via points\n");
                                            //int NumberOfviapoints = Convert.ToInt32(Console.ReadLine());
                                            User user = userServices.GetUser(userID);
                                            Offer offer = new Offer()
                                            { 
                                              FromPoint=fromPoint,
                                              ToPoint=toPoint,
                                              carID=carDetails.ID,
                                              AvailableSeats=availableSeats
                                            };
                                            offer = offerServices.CreateOffer(offer,user);
                                            Console.WriteLine("Offer created\n");
                                            break;
                                        }
                                    case 2:
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

                                            break;
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
