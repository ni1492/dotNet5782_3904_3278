using System;

 namespace DO
    {
        public struct Drone
        {
            public int Id { get ; set ; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get ; set ; }
            public override string ToString()//custom print function for drone struct
            {
                return ("Drone Id: " + Id + "\nDrone Model: " + Model + "\nMaximum Weight: " + MaxWeight + "\n");
            }

        };
    }
