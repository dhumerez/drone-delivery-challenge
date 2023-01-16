using DroneDeliveryService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DroneDeliveryService.Utils
{
    public class Scheduler
    {
        public static IDictionary<Drone, List<Trip>> Generate(DeliveryElements deliveryElements)
        {
            List<Location> locationsLeft = new List<Location>();
            IDictionary<Drone, List<Trip>> droneTrips = new Dictionary<Drone, List<Trip>>();

            locationsLeft.AddRange(deliveryElements.Locations);
            foreach (var drone in deliveryElements.Drones)
            {
                droneTrips.Add(drone, new List<Trip>());
            }
            while (locationsLeft.Count > 0)
            {
                foreach (var drone in deliveryElements.Drones)
                {
                    var tripsForDrone = droneTrips[drone];
                    var locationsPicked = PickLocationsForCapacity(drone.Capacity, locationsLeft);

                    if (locationsPicked.Count > 0)
                    {
                        locationsLeft = locationsLeft.Except(locationsPicked).ToList();
                        tripsForDrone.Add(new Trip() { Locations = locationsPicked });
                    }

                    droneTrips[drone] = tripsForDrone;
                }
            }
            return droneTrips;
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

                if (locations[i].Weight < droneMaxWeight && locations.Count > 1)
                {
                    var itemToRemove = locations.SingleOrDefault(r => r == locations[i]);
                    if (itemToRemove != null)
                        locations.Remove(itemToRemove);

                    List<Location> found = PickLocationsForCapacity(remainingCapacity, locations);
                    bool isEmpty= IsEmpty(found);

                    if (isEmpty)
                    {
                        return locationsPicked;
                    }
                    if (found.Count > 0)
                    {
                        locationsPicked.AddRange(found);
                        return locationsPicked;
                    }

                }
                else if (locations[i].Weight <= droneMaxWeight)
                {
                    return locationsPicked;
                }
                locationsPicked = null;
                return locationsPicked;
            }
            return locationsPicked;
        }

        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }
    }
}
