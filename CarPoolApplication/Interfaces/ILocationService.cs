using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CarPoolApplication.Interfaces
{
    interface ILocationService
    {
        public List<Location> GetAllLocations(string offerID);
    
        public bool AddLocation(Location point);
        public List<Location> GetLocations(string place);
        public List<Location> GetAll();
    }
}
