using DroneDeliveryService.Utils;
using System;
using System.IO;
using System.Linq;

namespace DroneDeliveryService
{
    class Program
    {             
        static void Main(string[] args)
        {
            //We will use System.IO in order to read the input file
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string file = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(_filePath).FullName).FullName).FullName).FullName;
            file += @"\input.txt";
            var objects = Loader.loadFromFile(file);
            var list = Scheduler.Generate(objects);

            int tripNumber = 1;
            int droneNumber = 1;
            foreach(var instance in list)
            {
                //We use the System library to print the output
                Console.WriteLine("[Drone #" + droneNumber + " " + instance.Key.Name + "] Capacity: " + instance.Key.Capacity);
                droneNumber++;
                foreach(var trip in instance.Value)
                {
                    Console.WriteLine("Trip " + "#" + tripNumber );

                    string allAddresses = string.Join(", ", trip.Locations.Select(s => s.Name));
                    Console.Write(allAddresses);
                    decimal total = trip.Locations.Sum(item => item.Weight);
                    Console.Write(" [Total: " + total + "]");
                    tripNumber++;
                    Console.WriteLine();
                }
                tripNumber = 1;
            }
            Console.ReadLine();
        }
    }
}
