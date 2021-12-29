﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void addParcel(parcelInDelivery parcel)
        {
            try
            {
                dl.AddParcel(parcel.sender.id, parcel.receiver.id, (DO.WeightCategories)parcel.weight, (DO.Priorities)parcel.priority, 0);
            }
            catch (Exception ex) //catches if the ID already exists
            {
                throw new BO.exceptions.ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public parcel displayParcel(int id) //display the parcel requested
        {
            try
            {
                if (id == 0)
                    return null;
                foreach (var p in dl.DisplayParcels(parcel => true))
                {
                    if (p.Id == id)
                    {
                        //if(p.DroneId==0)
                        //{
                        //    return (new parcel
                        //    {
                        //        id = p.Id,
                        //        sender = new customerForParcel
                        //        {
                        //            id = p.SenderId,
                        //            name = dl.DisplayCustomers(customer => customer.Id == p.SenderId).First().Name
                        //        },
                        //        receiver = new customerForParcel
                        //        {
                        //            id = p.TargetId,
                        //            name = dl.DisplayCustomers(customer => customer.Id == p.TargetId).First().Name
                        //        },
                        //        weight = (BO.WeightCategories)p.Weight,
                        //        priority = (BO.Priorities)p.Priority,
                        //        drone = new droneForParcel
                        //        {
                        //            id = 0,
                        //            battery =  0,
                        //            currentLocation = null
                        //        },
                        //        creation = p.Requested,
                        //        match = null,
                        //        pickup = null,
                        //        delivery = null
                        //    });
                        //}
                        return (new parcel
                        {
                            id = p.Id,
                            sender = new customerForParcel
                            {
                                id = p.SenderId,
                                name = dl.DisplayCustomers(customer => customer.Id == p.SenderId).First().Name
                            },
                            receiver = new customerForParcel
                            {
                                id = p.TargetId,
                                name = dl.DisplayCustomers(customer => customer.Id == p.TargetId).First().Name
                            },
                            weight = (BO.WeightCategories)p.Weight,
                            priority = (BO.Priorities)p.Priority,
                            drone = newDrone(p),
                            creation = p.Requested,
                            match = p.Scheduled,
                            pickup = p.PickedUp,
                            delivery = p.Delivered
                        }) ;
                    }
                }
                return new parcel();
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        private droneForParcel newDrone(Parcel p)
        {
            if (p.DroneId == 0)
                return new droneForParcel
                {
                    id = 0,
                    battery = 0,
                    currentLocation = null
                };
            if(p.DroneId == -1)
                return new droneForParcel
                {
                    id = -1,
                    battery =0,
                    currentLocation = null
                };
            return new droneForParcel
            {
                id = p.DroneId,
                battery = getBattery(p.DroneId),
                currentLocation = drones.Find(drone => drone.id == p.DroneId).currentLocation
            };
        }
        public IEnumerable<parcelForList> displayParcelList() //displays the list of all parcels 
        {
            foreach (var p in dl.DisplayParcels(parcel=>true))
            {
                yield return (new parcelForList
                {
                    id = p.Id,
                    sender = dl.DisplayCustomers(customer=>customer.Id==p.SenderId).FirstOrDefault().Name,
                    receiver = dl.DisplayCustomers(customer => customer.Id == p.TargetId).FirstOrDefault().Name,  
                    weight = (BO.WeightCategories)p.Weight,
                    priority = (BO.Priorities)p.Priority,
                    status = getStatus(p.Id)
                });

            }
        }
        public ParcelStatus getStatus(int id) //the function returns the ParcelStatus of the parcel
        {
            DO.Parcel p = dl.DisplayParcels(parcel => parcel.Id == id).First();
            DateTime? x = null;
            if (p.Delivered != x)
                return ParcelStatus.Delivered;
            if (p.PickedUp != x)
                return ParcelStatus.PickedUp;
            if (p.Scheduled != x)
                return ParcelStatus.Scheduled;
            return ParcelStatus.Requested;
        }
        public IEnumerable<parcelForList> displayParcelListWithoutDrone() //displays all the parcels that havent been matched with a drone
        {
            foreach (var p in dl.DisplayParcels(parcel=>parcel.DroneId==0))
            {
                if (getStatus(p.Id) == ParcelStatus.Requested)
                {
                    yield return (new parcelForList
                    {
                        id = p.Id,
                        sender = dl.DisplayCustomers(customer=>customer.Id==p.SenderId).First().Name, 
                        receiver = dl.DisplayCustomers(customer => customer.Id == p.TargetId).First().Name,  
                        weight = (BO.WeightCategories)p.Weight,
                        priority = (BO.Priorities)p.Priority,
                        status = getStatus(p.Id) 
                    });
                }
            }
        }
    }
}