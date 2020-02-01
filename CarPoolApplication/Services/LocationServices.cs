using System;
using System.Collections.Generic;
using System.Text;
using system.linq;

namespace CarPoolApplication.Services
{
    public class LocationServices
    {
        UtilServises utilServices = new UtilServises();
        public List<string> Locations = new List<string>();
        public List<string> ViaPoints = new List<string>();
        public List<string> GetViaPoints(string fromLocation, string toLocation)
        {
            List<string> ViaPoints = new List<string>();
            int FromIndex = utilServices.locations.FindIndex(fromLocation);
            int ToIndex = utilServices.locations.FindIndex(toLocation);
            for(int index=FromIndex;index<=ToIndex;index++)
            {
                ViaPoints.Add(utilServices.locations[index]);
            }
            return ViaPoints;
        }
    }
}
