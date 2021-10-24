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
            public override string ToString()
            {
                return ("Station Id: " + Id + "\nStation Name: " + Name +  "\n-Location: " + "\n-Longitude: " + Longitude + 
                    "\nLattitude: " + Lattitude + "\nNumber of charge slots: " + ChargeSlots + "\n");
            }

        };
    }
}
