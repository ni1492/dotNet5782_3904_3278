using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get => Id; set => Id = value; }
            public int Name { get => Name; set => Name = value; }
            public double Longitude { get => Longitude; set => Longitude = value; }
            public double Lattitude { get => Lattitude; set => Lattitude = value; }
            public int ChargeSlots { get => ChargeSlots; set => ChargeSlots = value; }
            public override string ToString()
            {
                return ("Station Id: " + Id + "\nStation Name: " + Name + 
                    "\nLocation: " + "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + "\nNumber of charge slots: " + ChargeSlots + "\n");
            }

        };
    }
}
