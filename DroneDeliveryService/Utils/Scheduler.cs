using DroneDeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    List<Location> locationsPicked = LoadDrones(locationsLeft, drone.Capacity);

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

        // This method makes runs of adjacent elements to find the closest sum of elements to the target.
        // Returns the list of Locations which weights sum get closer to the target value.
        static List<Location> LoadDrones(List<Location> elements, decimal target)
        {
            Solution bestSolution = new Solution { StartIndex = 0, EndIndex = -1, Sum = 0 };
            decimal bestError = Math.Abs(target);
            Solution currentSolution = new Solution { StartIndex = 0, Sum = 0 };

            for (int i = 0; i < elements.Count; i++)
            {
                currentSolution.EndIndex = i;
                currentSolution.Sum += elements[i].Weight;
                while (elements[currentSolution.StartIndex].Weight <= currentSolution.Sum - target)
                {
                    currentSolution.Sum -= elements[currentSolution.StartIndex].Weight;
                    ++currentSolution.StartIndex;
                }
                decimal currentError = Math.Abs(currentSolution.Sum - target);
                if (currentError < bestError || currentError == bestError && currentSolution.Length < bestSolution.Length)
                {
                    bestError = currentError;
                    bestSolution.Sum = currentSolution.Sum;
                    bestSolution.StartIndex = currentSolution.StartIndex;
                    bestSolution.EndIndex = currentSolution.EndIndex;
                }
            }
            return elements.GetRange(bestSolution.StartIndex, bestSolution.Length);
        }
    }
}
