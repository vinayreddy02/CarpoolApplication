using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Interfaces
{
    interface ILocationService
    {
        public List<Location> GetAllLocations(string offerID);
    
        public bool Add(Location point);
        public List<Location> GetAll();
    }
}
