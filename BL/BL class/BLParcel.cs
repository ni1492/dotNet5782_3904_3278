using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;
using System.Runtime.CompilerServices;

namespace BlApi
{
    public partial class BL : IBL
    {
        #region add parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcel(parcelForList parcel)
        {
            lock (dl)
            {
                try
                {
                    DateTime? x = new DateTime
                        (DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    dl.AddParcel(new Parcel { Id = 0, SenderId = dl.DisplayCustomers(c => c.Name == parcel.sender).FirstOrDefault().Id, TargetId = dl.DisplayCustomers(c => c.Name == parcel.receiver).FirstOrDefault().Id, Weight = (DO.WeightCategories)(parcel.weight - 1), Priority = (DO.Priorities)(parcel.priority - 1), DroneId = 0, Requested = x, Scheduled = null, PickedUp = null, Delivered = null });
                }
                catch (Exception ex) //catches if the ID already exists
                {
                    throw new BO.exceptions.ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
                }
            }
        }
        #endregion

        #region display parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public parcel displayParcel(int id) //display the parcel requested
        {
            lock (dl)
            {
                try
                {
                    if (id == 0)
                        return null;
                    foreach (var p in dl.DisplayParcels(parcel => true))
                    {
                        if (p.Id == id)
                        {
                            return (new parcel
                            {
                                id = p.Id,
                                sender = new customerForParcel
                                {
                                    id = p.SenderId,
                                    name = dl.DisplayCustomers(customer => customer.Id == p.SenderId).FirstOrDefault().Name
                                },
                                receiver = new customerForParcel
                                {
                                    id = p.TargetId,
                                    name = dl.DisplayCustomers(customer => customer.Id == p.TargetId).FirstOrDefault().Name
                                },
                                weight = (BO.WeightCategories)p.Weight + 1,
                                priority = (BO.Priorities)p.Priority + 1,
                                drone = newDrone(p),
                                creation = p.Requested,
                                match = p.Scheduled,
                                pickup = p.PickedUp,
                                delivery = p.Delivered
                            });
                        }
                    }
                    return new parcel();
                }
                catch (Exception ex) //catches if the ID not exists
                {
                    throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
                }

            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelForList> displayParcelList() //displays the list of all parcels 
        {
            lock (dl)
            {
                foreach (var p in dl.DisplayParcels(parcel => true))
                {
                    yield return (new parcelForList
                    {
                        id = p.Id,
                        sender = dl.DisplayCustomers(customer => customer.Id == p.SenderId).FirstOrDefault().Name,
                        receiver = dl.DisplayCustomers(customer => customer.Id == p.TargetId).FirstOrDefault().Name,
                        weight = (BO.WeightCategories)p.Weight + 1,
                        priority = (BO.Priorities)p.Priority + 1,
                        status = getStatus(p.Id)
                    });

                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelForList> displayParcelListWithoutDrone() //displays all the parcels that havent been matched with a drone
        {
            lock (dl)
            {
                foreach (var p in dl.DisplayParcels(parcel => parcel.DroneId == 0))
                {
                    if (getStatus(p.Id) == ParcelStatus.Requested)
                    {
                        yield return (new parcelForList
                        {
                            id = p.Id,
                            sender = dl.DisplayCustomers(customer => customer.Id == p.SenderId).FirstOrDefault().Name,
                            receiver = dl.DisplayCustomers(customer => customer.Id == p.TargetId).FirstOrDefault().Name,
                            weight = (BO.WeightCategories)p.Weight,
                            priority = (BO.Priorities)p.Priority,
                            status = getStatus(p.Id)
                        });
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelForList> displayParcels(Predicate<parcelForList> match) //display all parcels that match the predicate
        {
            foreach (var parcel in displayParcelList())
            {
                if (match(parcel))
                    yield return parcel;
            }
        }
        #endregion

        #region assistant functions
        private droneForParcel newDrone(Parcel p) //returns an empty drone for list 
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
        #endregion

        #region get status
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ParcelStatus getStatus(int id) //the function returns the ParcelStatus of the parcel
        {
            lock (dl)
            {
                DO.Parcel p = dl.DisplayParcels(parcel => parcel.Id == id).FirstOrDefault();
                DateTime? x = null;
                if (p.Delivered != x)
                    return ParcelStatus.Delivered;
                if (p.PickedUp != x)
                    return ParcelStatus.PickedUp;
                if (p.Scheduled != x)
                    return ParcelStatus.Scheduled;
                return ParcelStatus.Requested;
            }
        }
        #endregion

        #region delete parcel
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteParcel(int id)
        {
            lock (dl)
            {
                if (dl.DisplayParcels(p => p.Id == id).FirstOrDefault().Scheduled == null)
                    dl.deleteParcel(id);

                else
                    throw new BO.exceptions.DeleteException("cannot delete parcel");
            }
        }
        #endregion
       
    }
}
