using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get => Id; set => Id = value; }
            public string Model { get => Model; set => Model = value; }
            public WeightCategories MaxWeight { get => MaxWeight; set => MaxWeight = value; }
            public DroneStatuses Status { get => Status; set => Status = value; }
            public double Battery { get => Battery; set => Battery = value; }
            public override string ToString()
            {
                return ("Drone Id: " + Id + "\nDrone Model: " + Model + "\nMaximum Weight: " + MaxWeight +
                    "\nDrone status: " + Status + "\nDrone battery: " + Battery + "\n");
            }

        };
    }
}
