using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get ; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }
            public override string ToString()//custom print function for station struct
            {
                return ("Station Id: " + Id + "\nStation Name: " + Name +  "\nLocation: " + "\n-Longitude: " + Longitude + 
                    "\n-Lattitude: " + Lattitude + "\nNumber of charge slots: " + ChargeSlots + "\n");
            }

        };
    }
}
