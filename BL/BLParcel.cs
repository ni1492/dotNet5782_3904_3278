using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void addParcel(parcelInDelivery parcel)
        {
            try
            {
                dl.AddParcel(0, parcel.sender.id, parcel.receiver.id, (IDAL.DO.WeightCategories)parcel.weight, (IDAL.DO.Priorities)parcel.priority, 0);
            }
            catch (Exception ex) //catches if the ID already exists
            {
                throw new ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public parcel displayParcel(int id) //display the parcel requested
        {
            if (id == 0)
                return null;
            foreach (var p in dl.PrintAllParcel())
            {
                if (p.Id == id)
                {
                    return (new parcel
                    {
                        id = p.Id,
                        sender = new customerForParcel
                        {
                            id = p.SenderId,
                            name = dl.PrintCustomer(p.SenderId).Name 
                            //catch
                        },
                        receiver = new customerForParcel
                        {
                            id = p.TargetId,
                            name = dl.PrintCustomer(p.TargetId).Name 
                            //catch
                        },
                        weight = (BO.WeightCategories)p.Weight,
                        priority = (BO.Priorities)p.Priority,
                        drone = new droneForParcel
                        {
                            id = p.DroneId,
                            battery = getBattery(p.DroneId), //catch
                            currentLocation = drones.Find(drone => drone.id == p.DroneId).currentLocation
                        },
                        creation = p.Requested,
                        match = p.Scheduled,
                        pickup = p.PickedUp,
                        delivery = p.Delivered
                    });
                }
            }
            //throw exception is id doesnt exist
            return new parcel();
                }
        public IEnumerable<parcelForList> displayParcelList() //displays the list of all parcels 
        {
            foreach (var p in dl.PrintAllParcel())
            {
                yield return (new parcelForList
                {
                    id = p.Id,
                    sender = dl.PrintCustomer(p.SenderId).Name,   //catch
                    receiver = dl.PrintCustomer(p.TargetId).Name,   //catch
                    weight = (BO.WeightCategories)p.Weight,
                    priority = (BO.Priorities)p.Priority,
                    status=getStatus(p.Id) //catch
                });
                
            }
        }
        private ParcelStatus getStatus(int id) //the function returns the ParcelStatus of the parcel
        {
           IDAL.DO.Parcel p = dl.PrintParcel(id);  //catch
            DateTime x = DateTime.MinValue;
            if (p.Delivered != x)
                return ParcelStatus.Delivered;
            if (p.PickedUp != x)
                return ParcelStatus.PickedUp;
            if (p.Scheduled != x)
                return ParcelStatus.Scheduled;
            return ParcelStatus.Requested;
            //throw exception is id doesnt exist
        }
        public IEnumerable<parcelForList> displayParcelListWithoutDrone() //displays all the parcels that havent been matched with a drone
        {
            foreach (var p in dl.PrintAllParcel())
            {
                if(getStatus(p.Id)!=ParcelStatus.Requested)  //catch
                {
                    yield return (new parcelForList
                    {
                        id = p.Id, 
                        sender = dl.PrintCustomer(p.SenderId).Name,  //catch
                        receiver = dl.PrintCustomer(p.TargetId).Name,   //catch
                        weight = (BO.WeightCategories)p.Weight,
                        priority = (BO.Priorities)p.Priority,
                        status = getStatus(p.Id)   //catch
                    });
                }
            }
        }
    }
}
