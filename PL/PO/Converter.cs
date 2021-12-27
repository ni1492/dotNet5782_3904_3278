﻿using System;
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
        public static PO.Parcel ParcelPO(BO.parcelForList parcel)
        {
            return new()
            {
                PID = parcel.id,
                SenderName = parcel.sender,
                TargetName = parcel.receiver,
                PWeight = (WeightCategories)parcel.weight,
                Priority = (Priorities)parcel.priority,
                PStatus = (ParcelStatus)parcel.status
                //Drone_ID = parcel.drone.id,
                //Creation = parcel.creation,
                //Match = parcel.match,
                //Pickup = parcel.pickup,
                //Delivery = parcel.delivery
            };
        }
        public static PO.Customer CustomerPO(BO.customerForList customer)
        {
            return new()
            {
                CID = customer.id,
                CName = customer.name,
                Phone = customer.phone,
                //CLongitude = customer.location.convertLo(customer.location.Longitude),
                //CLatitude = customer.location.convertLa(customer.location.Latitude),
                Delivered = customer.deliveredPar,
                NDelivered = customer.notDeliveredPar,
                Accepted = customer.acceptedPar,
                NAccepted = customer.notAcceptedPar
                //Delivered = customer.fromCus.FindAll(p => p.status == BO.ParcelStatus.Delivered).Count(),
                //NDelivered = customer.fromCus.FindAll(p => p.status != BO.ParcelStatus.Delivered).Count(),
                //Accepted = customer.toCus.FindAll(p => p.status == BO.ParcelStatus.Delivered).Count(),
                //NAccepted = customer.toCus.FindAll(p => p.status != BO.ParcelStatus.Delivered).Count()
            };

        }
        public static PO.BaseStation StationPO(BO.baseStationForList station)
        {
            return new()
            {
                BSId = station.id,
                Name = station.name,
                //SLongitude = station.location.convertLo(station.location.Longitude),
                //SLatitude = station.location.convertLa(station.location.Latitude),
                Available = station.availableSlots,
                Used = station.usedSlots
                //  Used = station.dronesInCharging.Count()
            };
        }
        public static PO.ParcelSingle SingleParcelPO(BO.parcel parcel)
        {
            return new()
            {
            PSID=parcel.id,
                PSSender =CustomerForParcelPO( parcel.sender),
                PSTarget = CustomerForParcelPO(parcel.receiver),
                PSWeight=(WeightCategories)parcel.weight,
                PSPriority=(Priorities)parcel.priority,
                PSDrone_ID = parcel.drone.id,
                PSCreation = parcel.creation,
                PSMatch = parcel.match,
                PSPickup = parcel.pickup,
                PSDelivery = parcel.delivery
            };

        }
        public static PO.CustomerSingle SingleCustomerPO(BO.customer customer)
        {
            List<ParcelAtCustomer> from = new List<ParcelAtCustomer>();
            List<ParcelAtCustomer> to = new List<ParcelAtCustomer>();

            foreach (var item in customer.fromCus)
            {
                from.Add(ParcelAtCustomerPO(item));
            }
            foreach (var item in customer.toCus)
            {
                to.Add(ParcelAtCustomerPO(item));
            }
            return new()
            {
            CusID=customer.id,
            CusName = customer.name,
            CPhone= customer.phone,
            CLongitude=customer.location.convertLo(customer.location.Longitude),
            CLatitude= customer.location.convertLa(customer.location.Latitude),
            FromC= from,
            ToC= to
            };

        }
        public static PO.BaseStationSingle SingleStationPO(BO.baseStation station)
        {
            List<DroneInCharging> charging = new List<DroneInCharging>();
            foreach (var item in station.dronesInCharging)
            {
                charging.Add(DroneInChargingPO(item));
            }
            return new()
            {
                BaseSId= station.id,
                BSName= station.name,
                BSLongitude= station.location.convertLo(station.location.Longitude),
                BSLatitude = station.location.convertLa(station.location.Latitude),
                ChargingSlots = station.chargingSlots,
                InCharging=charging
            };

        }
        public static PO.CustomerForParcel CustomerForParcelPO(BO.customerForParcel customer)
        {
            return new()
            {
             CPID=customer.id,
            CPName=customer.name
            };

        }
        public static PO.DroneForParcel DroneForParcelPO(BO.droneForParcel drone)
        {
            return new()
            {
                DPId=drone.id,
            DPBattery= drone.battery,
            DPLongitude= drone.currentLocation.convertLo(drone.currentLocation.Longitude),
            DPLatitude= drone.currentLocation.convertLa(drone.currentLocation.Latitude)
            };

        }
        public static PO.DroneInCharging DroneInChargingPO(BO.droneInCharging drone)
        {
            return new()
            {
                DCId = drone.id,
                DCBattery = drone.battery
            };

        }
        public static PO.ParcelAtCustomer ParcelAtCustomerPO(BO.parcelAtCustomer parcel)
        {
            return new()
            {
                PCID = parcel.id,
            PCWeight= (WeightCategories)parcel.weight,
            PCPriority =(Priorities) parcel.priority,
            PCStatus= (ParcelStatus)parcel.status,
            OtherC= CustomerForParcelPO(parcel.otherCus)
            };

        }
        public static PO.ParcelInDelivery ParcelInDeliveryPO(BO.parcelInDelivery parcel)
        {
            string s;
            if (parcel.status)
                s = "delivery";
            else
                s = "waiting";
            return new()
            {
                PDID = parcel.id,
            PDSenderName = parcel.sender.name,
            PDTargetName = parcel.receiver.name,
                PDWeight=(WeightCategories)parcel.weight,
                PDPriority = (Priorities)parcel.priority,
                PDStatus =s,
                PickLongitude = parcel.pickUp.convertLo(parcel.pickUp.Longitude),
                PickLatitude= parcel.pickUp.convertLa(parcel.pickUp.Latitude),
                DesLongitude
                DesLatitude
            };

        }
    }
}