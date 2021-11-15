using System.Collections.Generic;

namespace DroneDeliveryService.Models
{
    public class Schedule
    {
        public Drone DroneSubject { get; set; }

        public List<Trip> Trips { get; set; }
    }
}
