using DroneDeliveryService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DroneDeliveryService.Utils
{
    public class Loader
    {
        public static DeliveryElements loadFromFile(string fileName)
        {
            DeliveryElements elements = new DeliveryElements();

            // Here we use System.IO to read the lines coming from the input file
            // We also use Linq in order to conver an IEnumerable into a list
            List<string> lines = File.ReadLines(fileName).ToList();
            Console.WriteLine(String.Join(Environment.NewLine, lines));
            var drones = lines[0].Split(',');
            var count = 0;

            Drone readDrone = new Drone();
            foreach(var drone in drones)
            {
                count++;
                if(count % 2 > 0)
                {
                    readDrone = new Drone();
                    readDrone.Name = drone;
                }
                else
                {
                    readDrone.Capacity = decimal.Parse(drone);
                    elements.Drones.Add(readDrone);
                }
            }

            // We use Linq here to skip the first item on the list and then we convert it into an Array.
            var locations = lines.Skip(1).ToArray();

            foreach (var location in locations)
            {
                Location readLocation = new Location();
                var splitLocation = location.Split(',');
                readLocation.Name = splitLocation[0];
                readLocation.Weight = decimal.Parse(splitLocation[1]);
                elements.Locations.Add(readLocation);
            }

            return elements;
        }        
    }
}
