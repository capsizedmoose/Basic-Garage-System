﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicGarageSystem
{
    class Vehicle
    {
        public string vehicleType { get; set; }
        public string regNr { get; set; }
        public string dateTime { get; set; }
        public int parkingSpot { get; set; }
        public int vehicleSize { get; set; }


        public override string ToString()
        {
            return base.ToString();
        }

    }
}
