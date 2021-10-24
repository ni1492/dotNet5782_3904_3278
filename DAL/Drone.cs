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
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return ("Drone Id: " + Id + "\nDrone Model: " + Model + "\nMaximum Weight: " + MaxWeight +
                    "\nDrone status: " + Status + "\nDrone battery: " + Battery + "\n");
            }

        };
    }
}
