using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BasicGarageSystem
{
    // The different types of vehicles that can be used
    public enum v_Vehicle
    {
        Bus,
        Car,
        MC,
        Truck
    }
    public class ParkingSpot
    {
        public int X { get; set; }
        public int Y { get; set; }
        // it looks too ugly if the numbers start at 0, so send them with +1
        public override string ToString()
        {
            return $"[{X + 1},{Y + 1}]";
        }
    }

    class GarageController
    {

        List<Vehicle> m_Vehicles; // maybe make a new list class later? 
        List<string> m_Receipts;
        //Vehicle[] m_Vehicles; // array-version
        bool[,] m_ParkingSpaces; // Making it 2-dimensional instead, would be a really strange garage if it had like 100 adjecent parking spaces
        public int NumberOfParkingSpaces { get; set; }
        public double ParkingFee { get; set; }
        public int MaximumHours { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        private Random randomNumber;

        // Constructor
        // takes argument: int x - used to create the size of the garage (default is 5)
        // takes argument: int y - used to create the size of the garage (default is 5)
        // takes argument: double parkingFee - used to set the parking fee for the garage
        // takes argument: int maximumHours - maximum number of hours allowed to park in the garage
        public GarageController(int x = 10, int y = 25, double parkingFee = 50, int maximumHours = 24)
        {
            m_Vehicles = new List<Vehicle>();
            m_Receipts = new List<string>();
            //m_Vehicles = new Vehicle[num]; // the array-version
            m_ParkingSpaces = new bool[x + 1, y + 1];
            NumberOfParkingSpaces = x * y;
            SizeX = x;
            SizeY = y;
            ParkingFee = parkingFee;
            MaximumHours = maximumHours;
            randomNumber = new Random();
        }

        // Prints out all vehicles in the list of vehicles
        // takes: no arguments
        // returns a list of strings with the basic information about the vehicles
        public List<string> PrintAll()
        {
            var vehicleStrings = new List<string>();

            foreach (var vehicle in m_Vehicles.Where(x => x != null))
            {
                // Maybe this method was only supposed to print basic information for all the vehicles?
                vehicleStrings.Add(vehicle.BasicInfo());
            }

            return vehicleStrings;
        }

        // Prints out a single vehicle that matches the given registration number
        // takes argument: string regNr - the registration number for the vehicle
        // returns a string with all the info of the mathcing vehicle
        public string PrintVehicle(string regNr)
        {
            Vehicle vehicle;
            return ((vehicle = GetVehicle(regNr)) == null) ? null : m_Vehicles.Where(v => v != null && v.regNr == regNr).First().ToString();
        }

        // Finds a specific vehicle with the given registration number 
        // takes argument: string reNr - the registration number
        // returns a string with basic info of the matching vehicle
        public string FindVehicle(string regNr)
        {
            Vehicle vehicle;
            return ((vehicle = GetVehicle(regNr)) == null) ? null : m_Vehicles.Where(v => v != null && v.regNr == regNr).First().BasicInfo();
        }

        // Finds all vehicles of the given type
        // takes argument: v_Vehicle type - the type of the vehicle
        // returns a string with the return message of the method
        public List<string> FindVehiclesByType(v_Vehicle type)
        {
            var results = new List<string>();

            foreach (var vehicle in m_Vehicles.Where(v => v.vehicleType == type))
            {
                results.Add(vehicle.BasicInfo());
            }
            return results;
        }

        // Adds a new Vehicle to the list of Vehicles
        // Creates a new Vehicle instance and assigns it a free parking space if available
        // takes no arguments
        // returns a string with the return message of the method
        public string ParkNewVehicle()
        {
            // Checks if the garage is full, so we can exit earlier if the garage is full
            if (NumberOfParkingSpaces == m_Vehicles.Count)
            {
                return $"The garage is full.";
            }

            // Get the current date
            string dateNow = GetDate();

            // the new vehicle to be added
            Vehicle vehicle = GenerateRandomVehicle();

            // Redundant since this is being checked in the method that generates a random vehicle, but I'll leave it in here anyway
            if (GetVehicle(vehicle.regNr) != null)
            {
                return $"A vehicle with the registration number: {vehicle.regNr} is already parked in the garage!";
            }

            // so we can exit earlier if there's not enough space in the garage
            if (GetNumberOfFreeSpaces() < vehicle.vehicleSize)
            {
                return $"The garage is full!!.";
            }

            ParkingSpot index = FindFreeParkingSpace(vehicle.vehicleSize); // index used to find empty parking space  

            // checks if we found a free parking space for the type of vehicle
            if (index.X == -1 && index.Y == -1)
            {
                return $"Not enough empty spaces in the garage for a vehicle of type: { vehicle.vehicleType.ToString()}.";
            }

            // sets the found parking space(s) to occupied/true
            for (int i = 0; i < vehicle.vehicleSize; i++)
            {
                m_ParkingSpaces[index.X, index.Y + i] = !m_ParkingSpaces[index.X, index.Y + i];
            }
            // assigns the vehicle its parking space
            vehicle.parkingSpot = index;

            // add the new vehicle to the list
            m_Vehicles.Add(vehicle);

            return $"The vehicle of the type: {vehicle.vehicleType.ToString()} " +
                $"with the registration number: {vehicle.regNr} \nwas parked in the garage at " +
                $"parking space(s): {index.ToString()}" + (vehicle.vehicleSize > 1 ? $" to [{index.X + 1},{index.Y + vehicle.vehicleSize}]." : "");
        }


        // !Mainly used for manual testing with different vehicle types!
        // Adds a new Vehicle to the list of Vehicles - 
        // Creates a new Vehicle instance and assigns it a free parking space if available
        // takes argument v_Vehicle - type of vehicle to be created
        // returns a string with the return message of the method
        public string ParkVehicle(v_Vehicle type)
        {
            // Checks if the garage is full, so we can exit earlier if the garage is full
            if (NumberOfParkingSpaces == m_Vehicles.Count)
            {
                return $"The garage is full.";
            }

            // Get the current date
            string dateNow = GetDate();

            // the new vehicle to be added
            Vehicle vehicle; // = GenerateRandomVehicle();

            switch (type)
            {
                case v_Vehicle.Bus:
                    vehicle = new Bus() { regNr = getRandomRegNr(), dateTime = dateNow };
                    break;
                case v_Vehicle.Car:
                    vehicle = new Car() { regNr = getRandomRegNr(), dateTime = dateNow };
                    break;
                case v_Vehicle.MC:
                    vehicle = new MC() { regNr = getRandomRegNr(), dateTime = dateNow };
                    break;
                case v_Vehicle.Truck:
                    vehicle = new Truck() { regNr = getRandomRegNr(), dateTime = dateNow };
                    break;
                default:
                    vehicle = null;
                    break;
            }

            // so we can exit earlier if there's not enough space in the garage
            if (GetNumberOfFreeSpaces() < vehicle.vehicleSize)
            {
                return $"The garage is full!!.";
            }

            ParkingSpot index = FindFreeParkingSpace(vehicle.vehicleSize); // index used to find empty parking space  
            // checks if we found a free parking space for the type of vehicle
            if (index.X == -1 && index.Y == -1)
            {
                return $"Not enough empty spaces in the garage for a vehicle of type: { vehicle.vehicleType.ToString()}.";
            }

            // sets the found parking space(s) to occupied/true
            for (int i = 0; i < vehicle.vehicleSize; i++)
            {
                m_ParkingSpaces[index.X, index.Y + i] = !m_ParkingSpaces[index.X, index.Y + i];
            }
            // assigns the vehicle its parking space
            vehicle.parkingSpot = index;

            // add the new vehicle to the list
            m_Vehicles.Add(vehicle);

            return $"The vehicle of the type: {vehicle.vehicleType.ToString()} " +
                $"with the registration number: {vehicle.regNr} \nwas parked in the garage at " +
                $"parking space(s): {index.ToString()}" + (vehicle.vehicleSize > 1 ? $" to [{index.X + 1},{index.Y + vehicle.vehicleSize}]." : "");
        }

        // Removes a Vehicle from the list of Vehicles
        //
        // takes argument: string regNr - the registration number for the vehicle
        // returns a string with the return message of the method
        public string VehicleCheckout(string regNr)
        {
            var vehicle = GetVehicle(regNr);

            if (vehicle == null)
            {
                return $"No vehicle found with the reigstration number: {regNr}.";
            }
            ParkingSpot index = vehicle.parkingSpot;

            // Free the occupied parking spaces
            for (int i = 0; i < vehicle.vehicleSize; i++)
            {
                m_ParkingSpaces[index.X, index.Y + i] = !m_ParkingSpaces[index.X, index.Y + i];
            }

            m_Vehicles = m_Vehicles.Where(v => v.regNr != regNr).ToList(); // remove the vehicle from the list;

            string receipt = $"The vehicle with the registration number: {regNr} " +
                $"left the garage, freeing up the parking space(s) {index.ToString()}" + (vehicle.vehicleSize > 1 ? $" to [{index.X + 1},{index.Y + vehicle.vehicleSize}]." : "." +
                $"\nTotal hours parked: {GetTotalHours(vehicle)} and total price: {GetTotalPrice(vehicle)}.");

            m_Receipts.Add(receipt);

            return receipt;
        }

        // Views how long a vehicle has been parked, and what the total price of the paarking fee is
        //
        // takes argument: string regNr - the registration number
        // returns a string with the return message of the method
        public string ViewParkedVehicle(string regNr)
        {
            var vehicle = GetVehicle(regNr);
            int totalHours = GetTotalHours(vehicle);
            double totalPrice = GetTotalPrice(vehicle);

            if (vehicle == null)
            {
                return $"No vehicle found with the reigstration number: {regNr}.";
            }

            return ($"Vehicle with registration number: {regNr} is parked at {vehicle.parkingSpot.ToString()} " +
                (vehicle.vehicleSize > 1 ? $" to [{vehicle.parkingSpot.X + 1},{vehicle.parkingSpot.Y + vehicle.vehicleSize}]" : "") +
                $" and has parked for a total of {totalHours} hours (rounded up). Total price: {totalPrice}.");
        }

        // Help-method tyo get the total price for a parked vehicle
        private double GetTotalPrice(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return -1;
            }

            // Total price, just at placeholder atm
            return GetTotalHours(vehicle) * vehicle.vehicleSize * ParkingFee;
        }

        // Help-method to get the total hours parked for a parked vehicle
        private int GetTotalHours(Vehicle vehicle)
        {

            if (vehicle == null)
            {
                return -1;
            }
            var parkedDate = DateTime.ParseExact(vehicle.dateTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InstalledUICulture);

            // Total hours parked rounded up, so can never park (or pay) for less than one hour
            return (int)(DateTime.Now - parkedDate).TotalHours + 1;
        }


        // Test method that views all the vehicles and removes them from the garage
        public void RemoveAllVehicles()
        {
            // I'm really starting to like LINQ!
            foreach (var regNr in m_Vehicles.Select(v => v.regNr))
            {
                Console.WriteLine($"\n{ViewParkedVehicle(regNr)}");
                Console.WriteLine(VehicleCheckout(regNr));
            }
        }

        // Help-method to get the number of free spaces
        // takes no arguments
        // returns an int with the number of free spaces
        private int GetNumberOfFreeSpaces()
        {
            return NumberOfParkingSpaces - m_Vehicles.Select(v => v.vehicleSize).Sum();
        }

        // Help-method to check if there are any free parking spacesa
        // takes no arguments
        // returns a PakringSpot with the free parking spot if one is found, otherwise it will have X = -1 and Y = -1
        private ParkingSpot FindFreeParkingSpace(int vehicleSize)
        {
            bool found = false;
            ParkingSpot ParkingSpot = new ParkingSpot() { X = -1, Y = -1 };
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY - (vehicleSize - 1); j++)
                {
                    if (!m_ParkingSpaces[i, j]) // Checks if the parking space is taken
                    {
                        // Checks if the vehicle will actually fit in the free parking space
                        for (int k = 0; k < vehicleSize; k++)
                        {
                            if (m_ParkingSpaces[i, j + k])
                            {
                                found = false; // if it can't fit; then we haven't found a free parking space
                                break;
                            }
                            found = true; // if it can fit; we have found a free parking space
                        }
                    }
                    if (found)
                    {
                        ParkingSpot.X = i;
                        ParkingSpot.Y = j;
                        return ParkingSpot;
                    }
                }
            }

            return ParkingSpot;
        }

        // Help-method to get a string with the current date
        // takes no arguments
        // returns a string with the date formated like: yyyy-MM-dd HH:mm
        private string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }

        // Help-method to get a vehicle that matches the given registration number 
        // takes argument: string regNr - the registration number of the vehicle
        // returns a Vehicle if it exists, otherwise null
        private Vehicle GetVehicle(string regNr)
        {
            var query = m_Vehicles.Where(v => v != null && v.regNr == regNr);

            return (query.Count() > 0 ? query.First() : null);
        }

        // Help-method to generate a random vehicle  
        // takes no arguments
        // returns the generated Vehicle 
        private Vehicle GenerateRandomVehicle()
        {
            string regNr;
            while (GetVehicle(regNr = getRandomRegNr()) != null) ; // Find a regNr that isn't already used by a vehicle in the garage

            v_Vehicle type = GetRandomVehicleType();
            string now = GetDate();

            switch (type)
            {
                case v_Vehicle.Bus:
                    return new Bus() { regNr = regNr, dateTime = now };
                case v_Vehicle.Car:
                    return new Car() { regNr = regNr, dateTime = now, };
                case v_Vehicle.MC:
                    return new MC() { regNr = regNr, dateTime = now, };
                case v_Vehicle.Truck:
                    return new Truck() { regNr = regNr, dateTime = now, };
                default:
                    return null;
            }
        }

        // Help-method to generate a random vehicle type  
        // takes no arguments
        // returns a v_Vehicle with the vehicle type 
        public v_Vehicle GetRandomVehicleType()
        {
            var values = Enum.GetValues(typeof(v_Vehicle));

            return (v_Vehicle)values.GetValue(randomNumber.Next(values.Length));
        }

        // Self-explanatory
        public List<string> GetReceipts()
        {
            return m_Receipts;
        }

        // Help-method to generate a random registration number
        // takes no arguments
        // returns a string with the generated registration number
        public string getRandomRegNr()
        {
            int num;
            string regNum = "";

            for (int i = 0; i < 3; i++)
            {
                num = randomNumber.Next(97, 122);
                regNum += (char)num;
            }

            for (int i = 0; i < 3; i++)
            {
                num = randomNumber.Next(0, 9);
                regNum += num;
            }
            //Console.WriteLine(regNum.ToUpper());

            return regNum.ToUpper();

        }

        // Extra stuff for later

        // Saves the list of vehicles to a file
        // takes no arguments
        // returns: nothing (void)
        public void SaveGarageToFile()
        {
            XElement xml = new XElement("Vehicles", m_Vehicles.Select(v => new XElement("Vehicle", v)));
            Console.WriteLine(xml);

        }
        // Loads a list of vehicles from a file
        // takes no arguments
        // returns: nothing (void)
        public void LoadGarageFromFile()
        {

        }
    }
}
