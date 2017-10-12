using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    class Car : Vehicle
    {
        public Car()
        {
            vehicleType = "Car";
            regNr = " ";
            dateTime = " ";
            parkingSpot = ' ';
            vehicleSize = '1';
        }
    }
}
