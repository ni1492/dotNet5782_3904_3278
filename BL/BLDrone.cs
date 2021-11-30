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
            dl.AddCharging(drone.id, stationId);
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

        }
        public void sendDroneToCharge(int id)
        {
            
        }
        public void releaseDroneFromCharge(int id, DateTime time)
        {
            //time???
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
