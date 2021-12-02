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
        public Random r = new Random();
        public void addDrone(droneForList drone, int stationId)
        {
            drone.battery = (double)r.Next(20, 40);
            drone.status = DroneStatuses.maintenance;
            dl.ChargingDrone(drone.id, stationId);
            drone.currentLocation = stationLocation(stationId);
            drone.parcelID = 0;
            drones.Add(drone);
            //add exception - drone exists - check in the dal layer 
            //and add exception for station id
        }
        public void matchParcelToDrone(int id)
        {
           // try
           // {
                if (isMatched(id))
                {
                    int parcelId = parcelToMatch(id);

                    dl.Match(parcelId, id);
                    foreach (var item in drones)
                    {
                        if (item.id == id)
                        {
                            item.status = DroneStatuses.delivery;
                        }
                    }
                }
          //  }
         //   catch
          //  {

          //  }
        }
        public void updateDrone(int id, string model)
        {
           IDAL.DO.Drone tempDL = dl.PrintDrone(id);
            dl.deleteDrone(id);
            dl.AddDrone(tempDL.Id, model, tempDL.MaxWeight);
            droneForList tempBL = drones.Find(drone => drone.id == id);
            tempBL.model = model;
            drones.RemoveAll(drone => drone.id == id);
            drones.Add(tempBL);
        }
        public void pickupParcel(int id)
        {
            int parcelId = drones.Find(drone => drone.id == id).parcelID;
            if (parcelId == 0 || getStatus(parcelId) != ParcelStatus.Scheduled) 
            {
                //trow- cant pick up parcel
            }
            dl.PickUpTime(new Parcel
            {
                Id = parcelId,
                DroneId=id
            });
            foreach (var parcel in dl.PrintAllParcel())
            {
                if(parcel.Id==parcelId)
                {
                    foreach (var cust in dl.PrintAllCustomer())
                    {
                        if (cust.Id == parcel.SenderId)
                        {
                            drones.Find(drone => drone.id == id).battery -= availablePK * calcDistance(drones.Find(drone => drone.id == id).currentLocation, new location
                            {
                                Latitude = cust.Lattitude,
                                Longitude = cust.Longitude
                            });
                            drones.Find(drone => drone.id == id).currentLocation = (new location
                            {
                                Latitude = cust.Lattitude,
                                Longitude = cust.Longitude
                            });
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public void deliverParcel(int id)
        {
            int parcelId = drones.Find(drone => drone.id == id).parcelID;
            if (parcelId == 0 || getStatus(parcelId) != ParcelStatus.PickedUp)
            {
                //trow- cant deliver parcel
            }
            dl.DeliveryTime(new Parcel
            {
                Id = parcelId,
                DroneId = id
            });
            foreach (var parcel in dl.PrintAllParcel())
            {
                if (parcel.Id == parcelId)
                {
                    foreach (var cust in dl.PrintAllCustomer())
                    {
                        if (cust.Id == parcel.TargetId)
                        {
                            drones.Find(drone => drone.id == id).battery -= availablePK * calcDistance(drones.Find(drone => drone.id == id).currentLocation, new location
                            {
                                Latitude = cust.Lattitude,
                                Longitude = cust.Longitude
                            });
                            drones.Find(drone => drone.id == id).currentLocation = (new location
                            {
                                Latitude = cust.Lattitude,
                                Longitude = cust.Longitude
                            });
                            break;
                        }
                    }
                    break;
                }
            }
            drones.Find(drone => drone.id == id).status = DroneStatuses.available;
        }
        public void sendDroneToCharge(int id)
        {
            droneForList d = drones.Find(drone => drone.id == id);
            if (d.status != DroneStatuses.available)
            {
                //throw-drone in delivery/maintane- cant send to charge
            }
            double battery = calcMinBattery(d);
            if (battery> d.battery)
            {
                //throw- battery too low to get to station
            }
            location l = nearestCharging(d.currentLocation);
            foreach (var item in dl.PrintAllStation())
            {
                if (item.Lattitude == l.Latitude && item.Longitude==l.Longitude)
                {
                    dl.ChargingDrone(id, item.Id);
                }
            }
            drones.Find(drone => drone.id == id).currentLocation = l;
            drones.Find(drone => drone.id == id).status = DroneStatuses.maintenance;
            drones.Find(drone => drone.id == id).battery -= battery;
        }
        public void releaseDroneFromCharge(int id, DateTime time)
        {
            if (drones.Find(drone => drone.id == id).status != DroneStatuses.maintenance) 
            {
                //throw
            }
            double battery = chargingPH * (time.Hour + time.Minute / 60);
            drones.Find(drone => drone.id == id).status = DroneStatuses.available;
            drones.Find(drone => drone.id == id).battery += battery;
            dl.ReleaseChargingDrone(id);
        }
        public drone displayDrone(int id)
        {
           droneForList drone = drones.Find(drone => drone.id == id);
            parcelInDelivery p =( new parcelInDelivery
            {
                id=drone.id,
                /*weight,
        priority, 
    status ,
sender ,
        customerForParcel receiver
      pickUp
       destination
  distance*/
            });
            BO.drone droneBO = (new drone
            {
                id = drone.id,
                model = drone.model,
                weight = drone.weight,
                currentLocation = drone.currentLocation,
                battery=drone.battery,
                status=drone.status,
            });
            return droneBO;
        }
        public IEnumerable<droneForList> displayDroneList()
        {
            foreach (var drone in drones)
            {
                yield return drone;
            }
        }
        private IEnumerable<parcelInDelivery> parcelsByPriority(List<parcelInDelivery> list, BO.Priorities prioritiy)
        {
            foreach (var parcel in list)
            {
                if (parcel.priority == prioritiy)
                    yield return parcel;
            }
        }
        private IEnumerable<parcelInDelivery> parcelsByWeight(List<parcelInDelivery> list, BO.WeightCategories weight)
        {
            foreach (var parcel in list)
            {
                if (parcel.weight == weight)
                    yield return parcel;
            }
        }
        private int parcelToMatch(int droneId)
        {
            BO.WeightCategories w = new();
            double battery = 0;
            droneForList d = new();
            foreach (var drone in drones)
            {
                if (droneId == drone.id)
                {
                    w = drone.weight;
                    battery = drone.battery;
                    d = (new droneForList
                    {
                        id = drone.id,
                        status = drone.status,
                        currentLocation = drone.currentLocation
                    });

                }
            }
            List<parcelInDelivery> list = new List<parcelInDelivery>();
            foreach (var parcel in displayParcelListWithoutDrone())
            {
                    list.Add(new parcelInDelivery
                    {
                        id = parcel.id,
                        weight = parcel.weight,
                        priority = parcel.priority,
                        pickUp = senderLocation(parcel.id),
                        destination = targetLocation(parcel.id),
                        status=false
                    });
            }
            BO.Priorities p = BO.Priorities.urgent;
            List<parcelInDelivery> parcels = new List<parcelInDelivery>();
            do
            {
                foreach (var item in parcelsByPriority(list, p))
                {
                    parcels.Add(item);
                }
                p--;
            } while (parcels == null && p != BO.Priorities.regular);
            if (p == BO.Priorities.regular)
                //trow exeption- no parcel to match
                list.Clear();
            do
            {
                foreach (var item in parcelsByWeight(parcels, w))
                {
                    list.Add(item);
                }
                w--;
            } while (list == null);
            double smallest = calcDistance(d.currentLocation, list[0].pickUp);
            int parcelId = list[0].id;
            foreach (var item in list)
            {
                double temp = calcDistance(d.currentLocation, item.pickUp);
                d.parcelID = item.id;
                if ((temp < smallest) && (calcMinBattery(d) <= battery))
                {
                    smallest = temp;
                    parcelId = item.id;
                }
            }
            return parcelId;
        }
    }
}
