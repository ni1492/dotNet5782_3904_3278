using System;

namespace DO
    {
        public struct DroneCharge
        {
            public int DroneId { get; set; }
            public int StationId { get; set; }
            public DateTime chargeTime { get; set; }

            public override string ToString()//custom print function for DroneCharge struct
            {
                return ("Drone Id: " + DroneId + "\nStation Id: " + StationId + "\n");
            }

        };
    }
