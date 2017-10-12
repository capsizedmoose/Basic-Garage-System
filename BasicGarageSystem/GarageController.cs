using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    public enum v_Vehicle
    {
        Bus,
        Car,
        MC,
        Truck
        
        
    }


    class GarageController
    {
        List<Vehicle> m_Vehicles; // maybe make a new list class later? 
        //Vehicle[] m_Vehicles; // will go with an array for now, since the garage doen't have an infinite amount of parking spaces
        bool[] m_ParkingSpaces;
        public int NumberOfParkingSpaces { get; set; }

        // Constructor
        // takes argument: int numberOfParkingSpaces - the total number of parking spaces in the garage (default is 100)
        public GarageController(int num = 100)
        {
            m_Vehicles = new List<Vehicle>();
            //m_Vehicles = new Vehicle[num];
            m_ParkingSpaces = new bool[num + 1];
            NumberOfParkingSpaces = num;
        }

        // Prints out all vehicles in the list of vehicles
        // takes: no arguments
        // returns a string with the return message of the method
        public string PrintAll()
        {
            string text = "";

            foreach (var vehicle in m_Vehicles.Where(x => x != null))
            {
                // Maybe this method was only supposed to print basic information for all the vehicles?
                text += vehicle.ToString();
            }

            return text;
        }

        // Prints out a single vehicle that matches the given registration number
        // takes argument: string regNr - the registration number for the vehicle
        // returns a string with the return message of the method
        public string PrintVehicle(string regNr)
        {
            var vehicle = m_Vehicles.Where(v => v.regNr == regNr);

            return vehicle.ToString();
        }

        // Help method to get the number of free spaces
        // takes no arguments
        // returns and int with the number of free spaces
        private int GetNumberOfFreeSpaces()
        {
            return NumberOfParkingSpaces - m_Vehicles.Select(v => v.vehicleSize).Sum();
        }

        // Adds a new Vehicle to the list of Vehicles
        // Creates a new Vehicle instance and assigns it the given registrattion number and vehicle type
        // takes argument: string vehicleType - the type of the vehicle (maybe an enum instead? where type = number of vehicles, like Car = 4)
        // takes argument: string regNr - the registration number for the vehicle
        // returns a string with the return message of the method
        public string ParkVehicle(v_Vehicle vehicleType, string regNr)
        {
            // Checks if the garage is full, so we can exit earlier if the garage is full
            if (NumberOfParkingSpaces == m_Vehicles.Count)
            {
                return $"The garage is full.";
            }

            // Get the current date
            string dateNow = GetDate();

            Vehicle vehicle; // the new behicle to be added

            // Will be changed or removed with the new method to generate random vehicles
            switch (vehicleType)
            {
                //
                case v_Vehicle.MC:
                    vehicle = new MC()
                    {
                        dateTime = dateNow,
                        regNr = regNr
                    };
                    break;
                //
                case v_Vehicle.Car:
                    vehicle = new Car()
                    {
                        dateTime = dateNow,
                        regNr = regNr
                    };
                    break;
                //
                case v_Vehicle.Bus:
                    vehicle = new Bus()
                    {
                        dateTime = dateNow,
                        regNr = regNr
                    };
                    break;
                //
                case v_Vehicle.Truck:
                    vehicle = new Truck()
                    {
                        dateTime = dateNow,
                        regNr = regNr
                    };
                    break;
                //
                default:
                    vehicle = new Vehicle();
                    break;
            }

            // so we can exit earlier if there's not enough space in the garage
            if (GetNumberOfFreeSpaces() < vehicle.vehicleSize)
            {
                return $"The garage is full!!.";
            }

            int index = 0; // index used to find empty parking space
            bool foundSpace = false; //

            while (index < (NumberOfParkingSpaces - (vehicle.vehicleSize - 1)))
            {
                if (!m_ParkingSpaces[index]) // Checks if the parking space is taken
                {
                    // Checks if the vehicle will actually fit in the free parking space
                    for (int i = 0; i < vehicle.vehicleSize; i++)
                    {
                        if (m_ParkingSpaces[index + i])
                        {
                            foundSpace = false; // if it can't fit; then we haven't found a free parking space
                            break;
                        }
                        foundSpace = true; // if it can fit; we have found a free parking space
                    }
                }
                if (foundSpace)
                {
                    break; // to escape the while-loop when we first find a free parking space
                }

                index++;
            }

            // checks if we exited the while-loop without finding a free parking space
            if (!foundSpace)
            {
                return $"Not enough empty spaces in the garage for a vehicle of type: { vehicle.vehicleType.ToString()}.";
            }

            // sets the found parking space(s) to occupied/true
            for (int i = 0; i < vehicle.vehicleSize; i++)
            {
                m_ParkingSpaces[index + i] = true;
            }
            // assigns the vehicle its parking space
            vehicle.parkingSpot = index;

            // add the new vehicle tpo the list
            m_Vehicles.Add(vehicle);

            return $"The vehicle with the registration number {regNr} was parked in the garage at parking space(s): {index} to {index + vehicle.parkingSpot}.";
        }

        // Removes a Vehicle from the list of Vehicles
        //
        // takes argument: string regNr - the registration number for the vehicle
        // returns a string with the return message of the method
        public string CheckOutVehicle(string regNr)
        {
            var vehicle = GetVehicle(regNr);

            if (vehicle == null)
            {
                return $"No vehicle with the reigstration number: {regNr}.";
            }

            // Free the occupied parking spaces
            for (int i = 0; i < vehicle.vehicleSize; i++)
            {
                m_ParkingSpaces[vehicle.parkingSpot + i] = !m_ParkingSpaces[vehicle.parkingSpot + i];
            }

            m_Vehicles = m_Vehicles.Where(v => v.regNr != regNr).ToList(); // remove to vehicle from the list;

            return $"The vehicle with the registration number: {regNr} left the garage, freeing up the parking spaces {vehicle.parkingSpot} to {vehicle.parkingSpot + vehicle.vehicleSize}.";
        }

        // Views how long a vehicle has been parked, and what the total price of the paarking fee is
        //
        // takes argument: string regNr - the registration number
        // returns a string with the return message of the method
        public string ViewParkedVehicle(string regNr)
        {
            var vehicle = GetVehicle(regNr);

            if (vehicle == null)
            {
                return $"No vehicle with the reigstration number: {regNr}.";
            }


            var parkedDate = DateTime.ParseExact(vehicle.dateTime, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InstalledUICulture);
            int totalHours = (int)(DateTime.Now - parkedDate).TotalHours + 1;
            double totalPrice = totalHours * vehicle.vehicleSize * 50;

            return ($"Registration number: {regNr} parked for a total of {totalHours} hours. Total price: {totalPrice}.");
        }
        // Help-method to check if there are any free parking spacesa string with the current date
        // takes no arguments
        // returns in int with the number of free parking spaces
        private int FindFreeParkingSpace()
        {
            int index = 0;
            while (index < NumberOfParkingSpaces && !m_ParkingSpaces[index++]) ;

            return (index == NumberOfParkingSpaces) ? -1 : index;
        }

        // Help-method to get a string with the current date
        // takes no arguments
        // returns a string with the date
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

        // Extra stuff for later

        // Saves the list of vehicles to a file
        // takes no arguments
        // returns: nothing (void)
        public void SaveGarageToFile()
        {

        }
        // Loads a list of vehicles from a file
        // takes no arguments
        // returns: nothing (void)
        public void LoadGarageFromFile()
        {

        }

        public string getRandomRegNr()
        {
            Random randomNumber = new Random();
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
            Console.WriteLine(regNum.ToUpper());

            return regNum.ToUpper();

        }
    }
}
