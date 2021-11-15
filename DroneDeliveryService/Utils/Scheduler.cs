using DroneDeliveryService.Models;
using System.Collections.Generic;
using System.Linq;

namespace DroneDeliveryService.Utils
{
    public class Scheduler
    {
        public static List<Schedule> Generate(DeliveryElements deliveryElements)
        {
            List<Schedule> schedules = new List<Schedule>();
            List<Location> locationsLeft = new List<Location>();
            locationsLeft.AddRange(deliveryElements.Locations);
            while (deliveryElements.Drones.Count > 0 && locationsLeft.Count > 0)
            {
                foreach(var drone in deliveryElements.Drones)
                {
                    var tripsForDrone = GetScheduledTripsDrone(drone, schedules);
                    var locationsPicked = PickLocationsForCapacity(drone.Capacity, locationsLeft);

                    if(locationsPicked.Count > 0)
                    {
                        locationsLeft = locationsLeft.Except(locationsPicked).ToList();
                        tripsForDrone.Add(new Trip() { Locations =  locationsPicked});
                    }

                    schedules.Add(new Schedule() { DroneSubject = drone, Trips = tripsForDrone });
                }
            }
            return schedules;
        }
        private static decimal CalculateLoadForLocations(List<Location> locations)
        {
            var sum = locations.Aggregate(0m,
                    (accum, loc) => accum + loc.Weight);
            return sum;
        }

        private static List<Location> PickLocationsForCapacity(decimal droneMaxWeight, List<Location> locations)
        {
            List<Location> locationsPicked = new List<Location>();
            decimal remainingCapacity;

            for (int i = 0; i < locations.Count; i++)
            {
                locationsPicked.Add(locations[i]);
                var currentLoad = CalculateLoadForLocations(locationsPicked);
                remainingCapacity = droneMaxWeight - currentLoad;

                if (locations[i].Weight < remainingCapacity && locations.Count > 1)
                {
                    locations.RemoveRange(0, i+1);
                    List<Location> found = PickLocationsForCapacity(remainingCapacity - locations[i].Weight, locations);
                    if(found.Count > 0)
                    {
                        locationsPicked.Add(locations[i]);
                        locationsPicked.AddRange(found);
                        return locationsPicked;
                    }
                }else if (locations[i].Weight <= remainingCapacity)
                {
                    locationsPicked.Add(locations[i]);
                    return locationsPicked;
                }

                return locationsPicked;
            }
            return locationsPicked;
        }

        private static List<Trip> GetScheduledTripsDrone(Drone drone, List<Schedule> schedules)
        {
            List<Trip> trips = new List<Trip>();
            foreach(var schedule in schedules)
            {
                if(schedule.DroneSubject == drone)
                {
                    trips.AddRange(schedule.Trips);
                }
            }
            return trips;
        }
    }
}
