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
        public DALObject()
        {
            DataSource.Initialize();
        }
        public  void AddStation(int Id, int name, double longitude, double lattitude, int chargeSlots)//add a new station
        {
            //initialize new station object:
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.Id = Id;
            station.Name = name;
            station.Longitude = longitude;
            station.Lattitude = lattitude;
            station.ChargeSlots = chargeSlots;
            //adds to base station list:
            DataSource.baseStations.Add(station);
        }
        public  void AddDrone(int Id, string model, WeightCategories maxWeight, DroneStatuses status, double battery)//add a new drone
        {
            //initialize new drone object:
            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.Id = Id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            drone.Status = status;
            drone.Battery = battery;
            //adds to drones list:
            DataSource.drones.Add(drone);
        }
        public  void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)//add a new customer
        {
            //initialize new customer object:
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            customer.Id = Id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = longitude;
            customer.Lattitude = lattitude;
            //adds to customers list:
            DataSource.customers.Add(customer);
        }
        public  void AddParcel(int id,int sId, int tId, WeightCategories weight, Priorities priority, int dId, DateTime req, DateTime sch, DateTime pUp, DateTime del)//add new parcel
        {
            //initialize new parcel object:
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            if (id == 0)
                parcel.Id = DataSource.Config.ParcelID++;
            else
                parcel.Id = id;
            parcel.SenderId = sId;
            parcel.TargetId = tId;
            parcel.Weight = weight;
            parcel.Priority = priority;
            parcel.DroneId = dId;
            parcel.Requested = req;
            parcel.Scheduled = sch;
            parcel.PickedUp = pUp;
            parcel.Delivered = del;
            //add to parcels list
            DataSource.parcels.Add(parcel);
        }
        public  void AddCharging(int dId, int sId)//adds a drone to charging
        {
            //initialize new charging object:
            DroneCharge charging = new DroneCharge();
            charging.DroneId = dId;
            charging.StationId = sId;
            //adds to charging list:
            DataSource.inChargeing.Add(charging);
        }
        public  void Match(Parcel parcel) //matches a drone to a parcel
        {
            int dId = 0;
            foreach (Drone drone in DataSource.drones)//goes over the list of drones and finds the first one that matches the standards of the given parcel
            {
                if ((drone.Status == DroneStatuses.available) && (drone.MaxWeight >= parcel.Weight))//makes sure the maximum weight of the drone can hold the parcel
                {
                    dId = drone.Id;
                    break;
                }
            }
            AddParcel(parcel.Id,parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, dId,
                parcel.Requested, parcel.Scheduled, parcel.PickedUp, parcel.Delivered);//adds the parcel to the list of parcels while changing the droneId to be the id of the chosen drone
            DataSource.parcels.Remove(parcel);//removes the old parcel from the list of parcels
        }
        public  void PickUpTime(Parcel parcel)//Update pickup parcel by drone
        {
            foreach (Drone drone in DataSource.drones)//goes over the list of drones to find the drone assigned to the parcel
            {
                if (drone.Id == parcel.DroneId)//when the drone is found we add a new drone with the updated status and remove the old drone from the list
                {
                    AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.delivery, drone.Battery);
                    DataSource.drones.Remove(drone);
                    break;
                }
            }
            AddParcel(parcel.Id,parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.DroneId,
                parcel.Requested, parcel.Scheduled, DateTime.Now, parcel.Delivered);//adds a new parcel with the updated pick up time
            DataSource.parcels.Remove(parcel);//removes the old parcel from the list
        }
        public  void DeliveryTime(Parcel parcel)//Update delivery parcel status
        {
            foreach (Drone drone in DataSource.drones)//goes over the list of drones to find the drone assigned to the parcel
            {
                if (drone.Id == parcel.DroneId)//when the drone is found we add a new drone with the updated status and remove the old drone from the list
                {
                    AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.available, drone.Battery);
                    DataSource.drones.Remove(drone);
                    break;
                }
            }
            AddParcel(parcel.Id,parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority, parcel.DroneId,
                parcel.Requested, parcel.Scheduled, parcel.PickedUp, DateTime.Now);//adds a new parcel with the updated delivery time
            DataSource.parcels.Remove(parcel);//removes the old parcel from the list of parcels
        }
        public  void ChargingDrone(Drone drone, Station station)//send drone to charge
        {
            AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.maintenance, drone.Battery);//adds a new drone with updated status
            DataSource.drones.Remove(drone);//removes the old drone from the list of drones
            AddStation(station.Id, station.Name, station.Longitude, station.Lattitude, station.ChargeSlots - 1);//adds a new station to the list of stations with the updated charging slots number
            DataSource.baseStations.Remove(station);//removes the old station from the list of stations
            AddCharging(drone.Id, station.Id);//adds a new charging object to the list

        }
        public  void ReleaseChargingDrone(Drone drone)//release drone from charging
        {
            AddDrone(drone.Id, drone.Model, drone.MaxWeight, DroneStatuses.available, drone.Battery);//adds a new drone to the list f drones with the updated status (available)
            DataSource.drones.Remove(drone);//removes the old drone
            int sId = 0;
            foreach (DroneCharge charge in DataSource.inChargeing)//goes over the list of charging and looks for the one with the drone that was given to release
            {
                if (charge.DroneId == drone.Id)//if its found we remove it from the list
                {
                    sId = charge.StationId;
                    DataSource.inChargeing.Remove(charge);
                    break;
                }
            }
            foreach (Station station in DataSource.baseStations)//goes over the station list to find the station that the given drone was charging at.
            {
                if (station.Id == sId)//if found - adds a new station with the updated number of available charging slots and removes the old station from the list.
                {
                    AddStation(station.Id, station.Name, station.Longitude, station.Lattitude, (station.ChargeSlots + 1));
                    DataSource.baseStations.Remove(station);
                    break;
                }
            }
        }
        public  void PrintStation(int id)//display station by station ID
        {
            foreach (Station station in DataSource.baseStations)//goes over the list of stations to find the station with that ID
            {
                if (station.Id == id)//when found- displays the station 
                    Console.WriteLine(station);
            }
        }
        public  void PrintDrone(int id)//display drone by drone ID
        {
            foreach (Drone drone in DataSource.drones)//goes over the list of drones to find the drone with that ID
            {
                if (drone.Id == id)
                    Console.WriteLine(drone);//when found- displays the drone 
            }
        }
        public  void PrintCustomer(int id)//display customer by customer ID
        {
            foreach (Customer customer in DataSource.customers)//goes over the list of customers to find the customer with that ID
            {
                if (customer.Id == id)//when found- displays the customer 
                    Console.WriteLine(customer);
            }
        }
        public  void PrintParcel(int id)//display parcel by parcel ID
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over the list of parcels to find the parcel with that ID
            {
                if (parcel.Id == id)//when found- displays the parcel
                    Console.WriteLine(parcel);
            }
        }
        public  void PrintAllStation()//display all stations
        {
            foreach (Station station in DataSource.baseStations)//goes over all the stations and prints all of them
            {
                Console.WriteLine(station);
            }
        }
        public  void PrintAllDrone()//display all drones
        {
            foreach (Drone drone in DataSource.drones)//goes over all the drones and prints all of them
            {
                Console.WriteLine(drone);
            }
        }
        public  void PrintAllCustomer()//display all customers
        {
            foreach (Customer customer in DataSource.customers)//goes over all the customers and prints all of them
            {
                Console.WriteLine(customer);
            }
        }
        public  void PrintAllParcel()//display all parcels
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and prints all of them
            {
                Console.WriteLine(parcel);
            }
        }
        public  void PrintParcelsWithNoDrone()//display all parcels that are not assigned to any drone
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and if they are not assigned to any drone - print the,
            {
                if (parcel.DroneId == 0)//not assigned to drone= drone ID is 0
                    Console.WriteLine(parcel);
            }
        }
        public  void PrintStationWithChargeSlots()//display all stations with available charging slots 
        {
            foreach (Station station in DataSource.baseStations)//goes over all the stations and if the station has any available charging slots - it displays it
            {
                if (station.ChargeSlots > 0)//station with available charging slots= at least one charging slot
                    Console.WriteLine(station);
            }
        }
        public  Parcel ConvertParcel(int id)//returns the parcel of the ID that was given
        {
            int index = 0;
            foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and finds the one with the given ID and returns it
            {
                index++;
                if (id == parcel.Id)
                {
                    return parcel;
                }
            }
            Parcel p = new Parcel();//if the parcel does not exist - returns an empty parcel
            return p;
        }
        public  Drone ConvertDrone(int id)//returns the drone of the ID that was given
        {
            int index = 0;
            foreach (Drone drone in DataSource.drones)//goes over all the drones and finds the one with the given ID and returns it
            {
                index++;
                if (id == drone.Id)
                {
                    return drone;
                }
            }
            Drone d = new Drone();//if the parcel does not exist - returns an empty drone
            return d;
        }
        public  Station ConvertStation(int id)//returns the station of the ID that was given
        {
            int index = 0;
            foreach (Station station in DataSource.baseStations)//goes over all the stations and finds the one with the given ID and returns it
            {
                index++;
                if (id == station.Id)
                {
                    return station;
                }
            }
            Station s = new Station();//if the station does not exist - returns an empty station
            return s;
        }
        public  double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)//calculate the distance between two coordinates
        {
            double lat1 = latitude1 * (Math.PI / 180.0);
            double long1 = longitude1 * (Math.PI / 180.0);
            double lat2 = latitude2 * (Math.PI / 180.0);
            double long2 = longitude2 * (Math.PI / 180.0) - long1;
            double distance = Math.Pow(Math.Sin((lat2 - lat1) / 2.0), 2.0) + Math.Cos(lat1) *Math.Cos(lat2) * Math.Pow(Math.Sin(long2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(distance), Math.Sqrt(1.0 - distance)));
        }
    }

}
