using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    class Truck: Vehicle
    {
        public Truck()
        {
            vehicleType = "Truck";
            regNr = " ";
            dateTime = " ";
            parkingSpot = ' ';
            vehicleSize = '1';
        }
    }
}
