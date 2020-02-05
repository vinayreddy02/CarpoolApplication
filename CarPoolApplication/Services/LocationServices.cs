using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPoolApplication.Models;
using CarPoolApplication.Services;

namespace CarPoolApplication.Services
{
   class LocationServices:IService<Location>
    { 
        public List<Location> locations = new List<Location>();
        public void Add(Location  point)
        {
            locations.Add(point);
        }
        public  List<Location> GetAll()
        {
            return locations;
        }
        public List<Location> GetAllLocations(string offerID)
        {
            return locations.FindAll(location => string.Equals(location.OfferID, offerID));
        }
       
    }
   
}
