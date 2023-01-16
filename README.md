# Drone-delivery-challenge by Diego Humerez
## Algorithm

First of all the data input file must be parsed into project objects to handle the input values (Drones, Locations).
Once we have the objects, we can proceed to process the data and provide the expected solution. The data process will be done with a method that makes runs of adjacent elements to find the closest sum of adjacent elements (Location weights) to the target value (Drone capacity). The reason for selecting this type of algorithm is because of the efficiency it provides, faster than recursivity.
When the process is completed for all the drones and no locations are left, it's time to print the results in the console with the required format.

## Walk Through Solution

- Parse input file to objects(Drones, Locations). For this purpose we create a Loader class that will take care of the parsing.
- Generate the list of trips (List of Locations) for each drone. In order to achieve this a Scheduler class is created, the main method (Load Drones) makes runs of adjacent elements to find the closest sum of adjacent elements (Location weights) to the target value (Drone capacity). The reason for selecting this type of algorithm is because of the efficiency it provides, faster than recursivity. The method "Load Drones" is called for each drone while the locations count is bigger than 0, if all the drones have been used and the locations are more than 0, another trip is added to the drones until there is no locations left.  
- Print the results in the required format.
- You will find the Program.cs where the Loader and Scheduler classes are used, both of them are in the Utils folder. The Models folder contains all the object Classes used in this project.


## Technical Dependencies and Libraries

- Framework : .NET Core 3.1
- IDE: Visual Studio 2022

### Libraries
- System.Linq
- System.IO
- System.Collections.Generic
