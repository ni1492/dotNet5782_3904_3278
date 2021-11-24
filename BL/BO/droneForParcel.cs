﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class droneForParcel
    {
        public int id { get; set; }
        public double battery { get; set; }
        public location currentLocation { get; set; }
        public override string ToString()//custom print function for drone 
        {
            return ("Drone Id: " + id + "\nDrone Battery:" + battery + "\nCurrent Location:" + currentLocation + "\n");
        }
    }
}
