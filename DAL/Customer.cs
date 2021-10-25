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
                return ("Customer Id: " + Id + "\nCustomer Name: " + Name + "\nPhone Number: " + Phone + "\nLocation: " + 
                    "\n-Longitude: " + DALObject.DALObject.ConvertLattitude(Longitude) +
                    "\n-Lattitude: " + DALObject.DALObject.ConvertLattitude(Lattitude) + "\n");
            }
        };
    }
}
