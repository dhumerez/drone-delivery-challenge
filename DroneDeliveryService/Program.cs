using DroneDeliveryService.Utils;
using System;
using System.IO;

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

            foreach(var instance in list)
            {
                //We use the System library to print the output
                Console.WriteLine(instance.DroneSubject.Name);
                foreach(var trip in instance.Trips)
                {
                    foreach(var location in trip.Locations)
                    {
                        Console.Write(location.Name + " ");
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
    }
}
