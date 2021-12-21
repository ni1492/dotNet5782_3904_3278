using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PL.PO
{
    public static class Converter
    {
        public static PO.Drone DronePO(BO.droneForList drone)
        {
            return new()
            {
                DId = drone.id,
                Model = drone.model,
                Battery = drone.battery,
                Weight = (WeightCategories)drone.weight,
                Status = (DroneStatuses)drone.status,
                DLongitude = drone.currentLocation.convertLo(drone.currentLocation.Longitude),
                DLatitude = drone.currentLocation.convertLa(drone.currentLocation.Latitude),
                Parcel = drone.parcelID
            };

        }
        public static PO.Parcel ParcelPO(BO.parcel parcel)
        {
            return new()
            {
                PID = parcel.id,
                SID = parcel.sender.id,
                TID = parcel.receiver.id,
                PWeight = (WeightCategories)parcel.weight,
                Priority = (Priorities)parcel.priority,
                Drone_ID = parcel.drone.id,
                Creation = parcel.creation,
                Match = parcel.match,
                Pickup = parcel.pickup,
                Delivery = parcel.delivery
            };
        }
        public static PO.Customer CustomerPO(BO.customer customer)
        {
            return new()
            {
                CID = customer.id,
                CName = customer.name,
                Phone = customer.phone,
                CLongitude = customer.location.convertLo(customer.location.Longitude),
                CLatitude = customer.location.convertLa(customer.location.Latitude),
                Delivered = customer.fromCus.FindAll(p => p.status == BO.ParcelStatus.Delivered).Count(),
                NDelivered = customer.fromCus.FindAll(p => p.status != BO.ParcelStatus.Delivered).Count(),
                Accepted = customer.toCus.FindAll(p => p.status == BO.ParcelStatus.Delivered).Count(),
                NAccepted = customer.toCus.FindAll(p => p.status != BO.ParcelStatus.Delivered).Count()
            };

        }
        public static PO.BaseStation StationPO(BO.baseStation station)
        {
            return new()
            {
                BSId = station.id,
                Name = station.name,
                SLongitude = station.location.convertLo(station.location.Longitude),
                SLatitude = station.location.convertLa(station.location.Latitude),
                Available = station.chargingSlots,
                Used = station.dronesInCharging.Count()
            };
        }
    }
}
