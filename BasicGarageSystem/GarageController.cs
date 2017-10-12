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

        // Default Constructor
        public GarageController()
        {
            m_Vehicles = new List<Vehicle>();
        }

        // Prints out a all vehicles in the list of vehicles
        public void PrintAll()
        {
            foreach (var vehicle in m_Vehicles)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        // Prints out a singe vheicle that matches the given registration number
        // string regNr = the registration number for the vehicle
        public void PrintVehicle(string regNr)
        {
            var vehicle = m_Vehicles.Where(v => v.regNr == regNr);

            Console.WriteLine(vehicle.ToString());
        }

        // Adds a new Vehicle to the list of Vehicles
        // Creates a new Vehicle instance and assigns it the given registrattion number and vehicle type
        // argument: string type - the type of the vehicle (maybe an enum instead?)
        // argument: string regNr - the registration number for the vehicle
        public void ParkVehicle(string type, string regNr)
        {
            //var vehicle = new Vehicle() { }
        }

        // Removes a Vehicle from the list of Vehicles
        //
        // argument: string regNr - the registration number for the vehicle
        public void CheckOutVehicle(string regNr)
        {

        }

        // Extra stuff for later
        public void SaveGarageToFile()
        {

        }
        public void LoadGarageFromFile()
        {

        }
    }
}
