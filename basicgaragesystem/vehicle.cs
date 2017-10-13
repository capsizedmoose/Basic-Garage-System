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
        public ParkingSpot parkingSpot { get; set; }
        public int vehicleSize { get; set; }


        public override string ToString()
        {
            return regNr + " :" + vehicleType + " : " + dateTime + " : " + parkingSpot + " : " + vehicleSize;
        }

        public string BasicInfo() {
            return regNr + ":" + vehicleType;
        }


    }
}
