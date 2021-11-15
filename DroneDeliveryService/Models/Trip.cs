using System.Collections.Generic;

namespace DroneDeliveryService.Models
{
    public class Trip
    {
        public Trip()
        {
            Locations = new List<Location>();
        }

        public List<Location> Locations { get; set; }
    }
}
