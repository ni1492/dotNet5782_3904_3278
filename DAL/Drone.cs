using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get ; set ; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get ; set ; }
           // public DroneStatuses Status { get; set; }
           // public double Battery { get; set; }
            public override string ToString()//custom print function for drone struct
            {
                return ("Drone Id: " + Id + "\nDrone Model: " + Model + "\nMaximum Weight: " + MaxWeight + "\n");
                //"\nDrone status: " + Status + "\nDrone battery: " + Battery + 
            }

        };
    }
}
