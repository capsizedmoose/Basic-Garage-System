using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    class GarageController
    {
        List<object> m_Vehicles; // will be a list of vehcles when that class is ready

        public GarageController()
        {
            m_Vehicles = new List<object>();
        }

        public void PrintAll()
        {
            foreach (var vehicle in m_Vehicles)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        public void PrintVehicle(string regNum)
        {
            //var vehicle = m_Vehicles.Where(v => v.RegNumber == RegNumber)

        }
    }
}
