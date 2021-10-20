using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get => Id; set => Id = value; }
            public string Name { get => Name; set => Name = value; }
            public string Phone { get => Phone; set => Phone = value; }
            public double Longitude { get => Longitude; set => Longitude = value; }
            public double Lattitude { get => Lattitude; set => Lattitude = value; }
            public override string ToString()
            {
                return ("Customer Id: " + Id + "\nCustomer Name: " + Name + "\nPhone Number: " + Phone +
                    "\nLocation: " + "\nLongitude: " + Longitude + "\nLattitude: " + Lattitude + "\n");
            }
        };
    }
}
