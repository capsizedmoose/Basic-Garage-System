using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    class Vehicle
    {
        public v_Vehicle vehicleType { get; set; }
        public string regNr { get; set; }
        public string dateTime { get; set; }
        public int parkingSpot { get; set; }
        public int vehicleSize { get; set; }


        public override string ToString()
        {
            return vehicleType + " : " + regNr + " : " + dateTime + " : " + parkingSpot + " : " + vehicleSize + "\n";
        }

    }
}
