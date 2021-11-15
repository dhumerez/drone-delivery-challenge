using System.Collections.Generic;

namespace DroneDeliveryService.Models
{
    public class DeliveryElements
    {
        public DeliveryElements()
        {
            Drones = new List<Drone>();

            Locations = new List<Location>();
        }

        public List<Drone> Drones { get; set; }

        public List<Location> Locations { get; set; }
    }
}
