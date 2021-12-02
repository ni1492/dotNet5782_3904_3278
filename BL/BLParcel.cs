using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public partial class BL : IBL
    {
        public void addParcel(parcelInDelivery parcel)
        {
            DateTime x = DateTime.MinValue;
            try
            {
                dl.AddParcel(0, parcel.sender.id, parcel.receiver.id, (IDAL.DO.WeightCategories)parcel.weight, (IDAL.DO.Priorities)parcel.priority, 0);
            }
            catch(Exception)
            {
                throw;
            }
        }
        public parcel displayParcel(int id)
        {
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
                        },
                        receiver = new customerForParcel
                        {
                            id = p.TargetId,
                            name = dl.PrintCustomer(p.TargetId).Name
                        },
                        weight = (BO.WeightCategories)p.Weight,
                        priority = (BO.Priorities)p.Priority,
                        drone = new droneForParcel
                        {
                            id = p.DroneId,
                            battery = getBattery(p.DroneId),
                            currentLocation = drones.Find(drone => drone.id == p.DroneId).currentLocation
                        },
                        creation = p.Requested,
                        match = p.Scheduled,
                        pickup = p.PickedUp,
                        delivery = p.Delivered
                    });
                }
            }
            //throw- parcel not exist
            return new parcel();
                }
        public IEnumerable<parcelForList> displayParcelList()
        {
            foreach (var p in dl.PrintAllParcel())
            {
                yield return (new parcelForList
                {
                    id = p.Id,
                    sender = dl.PrintCustomer(p.SenderId).Name,
                    receiver = dl.PrintCustomer(p.TargetId).Name,
                    weight = (BO.WeightCategories)p.Weight,
                    priority = (BO.Priorities)p.Priority,
                    status=getStatus(p.Id)
                });
                
            }
        }
        private ParcelStatus getStatus(int id)
        {
            parcel p = displayParcel(id);
            DateTime x = DateTime.MinValue;
            if (p.delivery != x)
                return ParcelStatus.Delivered;
            if (p.pickup != x)
                return ParcelStatus.PickedUp;
            if (p.match != x)
                return ParcelStatus.Scheduled;
            return ParcelStatus.Requested;
        }
        public IEnumerable<parcelForList> displayParcelListWithoutDrone()
        {
            foreach (var p in dl.PrintAllParcel())
            {
                if(getStatus(p.Id)!=ParcelStatus.Requested)
                {
                    yield return (new parcelForList
                    {
                        id = p.Id,
                        sender = dl.PrintCustomer(p.SenderId).Name,
                        receiver = dl.PrintCustomer(p.TargetId).Name,
                        weight = (BO.WeightCategories)p.Weight,
                        priority = (BO.Priorities)p.Priority,
                        status = getStatus(p.Id)
                    });
                }
            }
        }
    }
}
