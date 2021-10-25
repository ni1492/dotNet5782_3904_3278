﻿using System;

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
            public override string ToString()//צורת הדפסה נכונה
            {
                return ("Customer Id: " + Id + "\nCustomer Name: " + Name + "\nPhone Number: " + Phone +
                    "\nLocation: " + "\n-Longitude: " + Longitude + "\n-Lattitude: " + Lattitude + "\n");
            }
        };
    }
}
