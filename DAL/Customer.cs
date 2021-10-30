using System;

namespace IDAL
{
    namespace DO
    {
        public struct Customer
        {
            public int Id { get; set; }
            public string Name { get ; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public override string ToString()//custom print function for customer struct
            {
                Sexagesimal Location = new Sexagesimal(Longitude, Lattitude);
                return ("Customer Id: " + Id + "\nCustomer Name: " + Name + "\nPhone Number: " + Phone + "\nLocation: " + "(" + Longitude + "," + Lattitude + ") \n" + Location + "\n");
            }
        };
    }
}
