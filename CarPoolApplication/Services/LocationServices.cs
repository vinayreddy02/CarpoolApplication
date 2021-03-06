﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CarPoolApplication.Interfaces;

namespace CarPoolApplication.Services
{
   class LocationServices: ILocationService
    { 
        public List<Location> locations = new List<Location>();
        public bool AddLocation(Location  point)
        {
            try
            {
                locations.Add(point);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public  List<Location> GetAll()
        {
            try
            {
                return locations;
            }
            catch
            {
                return null;
            }
        }
        public List<Location> GetLocations(string place)
        {
            return locations.FindAll(location => string.Equals(location.Place, place));
        }
        public List<Location> GetAllLocations(string offerID)
        {
            try
            {
                return locations.FindAll(location => string.Equals(location.OfferID, offerID));
            }
            catch
            {
                return null;
            }
        }
    
       
    }
   
}
