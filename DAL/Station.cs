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
                Sexagesimal Location = new Sexagesimal(Longitude, Lattitude);
                return ("Station Id: " + Id + "\nStation Name: " + Name + "\nLocation: " + "(" + Longitude + "," + Lattitude + ") \n" + Location +
                    "\nNumber of charge slots: " + ChargeSlots + "\n");
            }

        };
    }
}
