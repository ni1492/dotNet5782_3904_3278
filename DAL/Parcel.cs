using System;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get => Id; set => Id = value; }
            public int SenderId { get => SenderId; set => SenderId = value; }
            public int TargetId { get => TargetId; set => TargetId = value; }
            public WeightCategories Weight { get => Weight; set => Weight = value; }
            public Priorities Priority { get => Priority; set => Priority = value; }
            public DateTime Requested { get => Requested; set => Requested = value; }
            public int DroneId { get => DroneId; set => DroneId = value = 0; }
            public DateTime Scheduled { get => Scheduled; set => Scheduled = value; }
            public DateTime PickedUp { get => PickedUp; set => PickedUp = value; }
            public DateTime Delivered { get => Delivered; set => Delivered = value; }
            public override string ToString()
            {
                return ("Parcel Id: " + Id + "\nSender Id: " + SenderId + "\nCustomer Id: " + TargetId + "\nWeight of the parcel: " + Weight +
                    "\nPriority: " + Priority + "\nDrone Id: " + DroneId + "\nRequested Date: " + Requested + "\nScheduled Date: " + Scheduled +
                    "\nPickedUp Date:" + PickedUp + "\nDelivered Date: " + Delivered + "\n");
            }

        };
    }
}
