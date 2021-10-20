using System;

namespace IDAL
{
    namespace DO
    {
        public struct DroneCharge
        {
            public int DroneId { get => DroneId; set => DroneId = value; }
            public int StationId { get => StationId; set => StationId = value; }
            public override string ToString()
            {
                return ("Drone Id: " + DroneId + "\nStation Id: " + StationId + "\n");
            }

        };
    }
}
