using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;


namespace DALObject
{
    public class DALObject
    {
        public static void AddStation(int Id,int name, double longitude, double lattitude, int chargeSlots)
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.Id = Id;
            station.Name = name;
            station.Longitude = longitude;
            station.Lattitude = lattitude;
            station.ChargeSlots = chargeSlots;
            DataSource.baseStations.Add(station);
        }
        public static void AddDrone(int Id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.Id = Id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            drone.Status = status;
            drone.Battery = battery;
            DataSource.drones.Add(drone);
        }
        public static void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            customer.Id = Id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = longitude;
            customer.Lattitude = lattitude;
            DataSource.customers.Add(customer);
        }
        public static void AddParcel(int Id, int sId, int tId, WeightCategories weight, Priorities priority, int dId, DateTime req, DateTime sch, DateTime pUp, DateTime del)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            parcel.Id = Id;
            parcel.SenderId = sId;
            parcel.TargetId = tId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.DroneId = dId;
            parcel.Requested = req;
            parcel.Scheduled = sch;
            parcel.PickedUp = pUp;
            parcel.Delivered = del;
            DataSource.parcels.Add(parcel);
        }
        public static void AddCharging(int dId, int sId)
        {
            DroneCharge charging = new DroneCharge();
            charging.DroneId = dId;
            charging.StationId = sId;
            DataSource.inChargeing.Add(charging);
        }
        public static void Match(Parcel parcel)
        {
            int dId = 0;
            //parcel.DroneId = drone.Id;
            int index=0;
            foreach (Drone drone in DataSource.drones)
             {
                 index++;
                 if ((drone.Status == DroneStatuses.available)&&(drone.MaxWeight>=parcel.Weight))
                 {
                    AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.delivery, drone.Battery);
                    DataSource.drones.RemoveAt(index);
                    dId = drone.Id;
                    break;
                 }
             }
            //DataSource.drones.RemoveAt(index);
            parcel.DroneId = dId;
        }
        public static void PickUpTime(Parcel parcel)
        {
            parcel.PickedUp = DateTime.Now;
            int index = 0;
            foreach (Drone drone in DataSource.drones)
            {
                index++;
                if (drone.Id == parcel.DroneId)
                {
                    AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.delivery, drone.Battery);
                    break;
                }
            }
            DataSource.drones.RemoveAt(index);
        }
        public static void DeliveryTime(Parcel parcel)
        {
            parcel.Delivered = DateTime.Now;
            int index = 0;
            foreach (Drone drone in DataSource.drones)
            {
                index++;
                if (drone.Id == parcel.DroneId)
                {
                    AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.available, drone.Battery);
                    break;
                }
            }
            DataSource.drones.RemoveAt(index);
        }
        public static void ChargingDrone(Drone drone, Station station)
        {
            if(station.ChargeSlots>0)
            {
                drone.Status = DroneStatuses.maintenance;
                station.ChargeSlots--;
                AddCharging(drone.Id, station.Id);
            } 
        }
        public static void ReleaseChargingDrone(Drone drone)
        {
            drone.Status = DroneStatuses.available;
            int index = 0;
            int sId = 0;
            foreach (DroneCharge charge in DataSource.inChargeing)
            {
                index++;
                if (charge.DroneId==drone.Id)
                {
                    sId = charge.StationId; 
                }
            }
            DataSource.inChargeing.RemoveAt(index);
            index = 0;
            foreach (Station station in DataSource.baseStations)
            {
                index++;
                if (station.Id ==sId)
                {
                    AddStation(station.Id, station.Name, station.Longitude, station.Lattitude, (station.ChargeSlots - 1));
                }
            }
            DataSource.baseStations.RemoveAt(index);
        }
        public static void PrintStation(int id)
        {
            foreach (Station station in DataSource.baseStations)
            {
               if(station.Id==id)
                    Console.WriteLine(station);
            }
        }
        public static void PrintDrone(int id)
        {
            foreach (Drone drone in DataSource.drones)
            {
                if (drone.Id == id)
                    Console.WriteLine(drone);
            }
        }
        public static void PrintCustomer(int id)
        {
            foreach (Customer customer in DataSource.customers)
            {
                if (customer.Id == id)
                    Console.WriteLine(customer);
            }
        }
        public static void PrintParcel(int id)
        {
            foreach (Parcel parcel in DataSource.parcels)
            {
                if (parcel.Id == id)
                    Console.WriteLine(parcel);
            }
        }
        public static void PrintAllStation()
        {
            foreach (Station station in DataSource.baseStations)
            {
                Console.WriteLine(station);
            }
        }
        public static void PrintAllDrone()
        {
            foreach (Drone drone in DataSource.drones)
            {
                Console.WriteLine(drone);
            }
        }
        public static void PrintAllCustomer()
        {
            foreach (Customer customer in DataSource.customers)
            {
                    Console.WriteLine(customer);
            }
        }
        public static void PrintAllParcel()
        {
            foreach (Parcel parcel in DataSource.parcels)
            {
                    Console.WriteLine(parcel);
            }
        }
        public static void PrintParcelsWithNoDrone()
        {
            foreach (Parcel parcel in DataSource.parcels)
            {
                if(parcel.DroneId==0)
                    Console.WriteLine(parcel);
            }
        }
        public static void PrintStationWithChargeSlots()
        {
            foreach (Station station in DataSource.baseStations)
            {
                if(station.ChargeSlots>0)
                    Console.WriteLine(station);
            }
        }
        public static Parcel ConvertParcel(int id)
        {
            int index = 0;
            foreach (Parcel parcel in DataSource.parcels)
            {
                index++;
                if (id == parcel.Id)
                {
                    return parcel;
                }
            }
            Parcel p = new Parcel();
            return p;
        }
        public static Drone ConvertDrone(int id)
        {
            int index = 0;
            foreach (Drone drone in DataSource.drones)
            {
                index++;
                if (id == drone.Id)
                {
                    return drone;
                }
            }
            Drone d = new Drone();
            return d;
        }
        public static Station ConvertStation(int id)
        {
            int index = 0;
            foreach (Station station in DataSource.baseStations)
            {
                index++;
                if (id == station.Id)
                {
                    return station;
                }
            }
            Station s = new Station();
            return s;
        }
    }
}
