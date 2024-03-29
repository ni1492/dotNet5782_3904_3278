﻿using System;

 namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime? Requested { get ; set ; }
            public int DroneId { get; set; }
            public DateTime? Scheduled { get; set; }
            public DateTime? PickedUp { get; set; }
            public DateTime? Delivered { get; set; }
            public override string ToString()//custom print function for parcel struct
            {
                return ("Parcel Id: " + Id + "\nSender Id: " + SenderId + "\nCustomer Id: " + TargetId + "\nWeight of the parcel: " + Weight +
                    "\nPriority: " + Priority + "\nDrone Id: " + DroneId + "\nRequested Date: " + Requested + "\nScheduled Date: " + Scheduled +
                    "\nPickedUp Date: " + PickedUp + "\nDelivered Date: " + Delivered + "\n");
            }

        };
    }
