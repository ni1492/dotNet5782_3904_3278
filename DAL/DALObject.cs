using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DAL.IDAL;


namespace DALObject
{
    public class DALObject:IDal
    {
        public DALObject()
        {
            DataSource.Initialize();
        }
        public  void AddStation(int Id, int name, double longitude, double lattitude, int chargeSlots)//add a new station
        {
            //initialize new station object:
            foreach (Station s in DataSource.baseStations)
            {
                if (s.Id == Id)
                {
                    throw new ExistException("station alredy exist");
                }
            }
            IDAL.DO.Station station = new IDAL.DO.Station();
            station.Id = Id;
            station.Name = name;
            station.Longitude = longitude;
            station.Lattitude = lattitude;
            station.ChargeSlots = chargeSlots;
            //adds to base station list:
            DataSource.baseStations.Add(station);
        }
        public  void AddDrone(int Id, string model, WeightCategories maxWeight)//add a new drone
        {
            //initialize new drone object:
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == Id)
                {
                    throw new ExistException("drone alredy exist");
                }
            }
            IDAL.DO.Drone drone = new IDAL.DO.Drone();
            drone.Id = Id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
          //  drone.Status = status;
          //  drone.Battery = battery;
            //adds to drones list:
            DataSource.drones.Add(drone);
        }
        public  void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)//add a new customer
        {
            //initialize new customer object:
            foreach (Customer c in DataSource.customers)
            {
                if (c.Id == Id)
                {
                    throw new ExistException("customer alredy exist");
                }
            }
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
            foreach (Parcel p in DataSource.parcels)
            {
                if (p.Id == id)
                {
                    throw new ExistException("parcel alredy exist");
                }
            }
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
            bool b = false;
            foreach (Station s in DataSource.baseStations)
            {
                if (s.Id == sId)
                {
                    b = true;
                }
            }
            if(!b)
                throw new NotFoundException("station doesn't exist");
            b = false;
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == dId)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("drone doesn't exist");

            //initialize new charging object:
            DroneCharge charging = new DroneCharge();
            charging.DroneId = dId;
            charging.StationId = sId;
            //adds to charging list:
            DataSource.inChargeing.Add(charging);
        }
        public  void Match(Parcel parcel) //matches a drone to a parcel
        {
            bool b = false;
            foreach (Parcel p in DataSource.parcels)
            {
                if (p.Id == parcel.Id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("parcel doesn't exist");

            foreach (Drone drone in DataSource.drones)//goes over the list of drones and finds the first one that matches the standards of the given parcel
            {
                bool matched = false;
                foreach (Parcel parcel1 in DataSource.parcels)
                {
                    if (parcel1.DroneId == drone.Id)
                    {
                        matched = true;
                        break;
                    }
                }
                if (drone.MaxWeight >= parcel.Weight && !matched)//makes sure the maximum weight of the drone can hold the parcel
                {
                    for (int i = 0; i < DataSource.parcels.Count(); i++)
                    {
                        if (DataSource.parcels[i].Id == parcel.Id)
                        {
                            Parcel p = DataSource.parcels[i];
                            p.DroneId = drone.Id;
                            DataSource.parcels[i] = p;
                            break;
                        }
                    }
                    break;
                }
            }
           
        }
        public  void PickUpTime(Parcel parcel)//Update pickup parcel by drone
        {
            bool b = false;
            foreach (Parcel p in DataSource.parcels)
            {
                if (p.Id == parcel.Id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("parcel doesn't exist");

            for (int i=0; i<DataSource.drones.Count;i++)//goes over the list of drones to find the drone assigned to the parcel
            {
                if (DataSource.drones[i].Id == parcel.DroneId)//when the drone is found we update status
                {
                    Drone d = DataSource.drones[i];
                    //update pickup
                    DataSource.drones[i] = d;
                    for (int j = 0; j < DataSource.parcels.Count(); j++)//change the pickup time
                    {
                        if (DataSource.parcels[j].Id == parcel.Id)
                        {
                            Parcel p = DataSource.parcels[j];
                            p.PickedUp = DateTime.Now;
                            DataSource.parcels[j] = p;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public  void DeliveryTime(Parcel parcel)//Update delivery parcel status
        {
            bool b = false;
            foreach (Parcel p in DataSource.parcels)
            {
                if (p.Id == parcel.Id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("parcel doesn't exist");

            for (int i = 0; i < DataSource.drones.Count; i++)//goes over the list of drones to find the drone to update
            {
                if (DataSource.drones[i].Id == parcel.DroneId)//when the drone is found we updat status
                {
                    Drone d = DataSource.drones[i];
                    //update pickup
                    DataSource.drones[i] = d;
                    for (int j = 0; j < DataSource.parcels.Count(); j++)//change the delivery time
                    {
                        if (DataSource.parcels[j].Id == parcel.Id)
                        {
                            Parcel p = DataSource.parcels[j];
                            p.Delivered = DateTime.Now;
                            DataSource.parcels[j] = p;
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public  void ChargingDrone(Drone drone, Station station)//send drone to charge
        {
            for (int i=0;i<DataSource.drones.Count;i++)//find the drone to update
            {
                if(DataSource.drones[i].Id==drone.Id)
                {
                    Drone d = DataSource.drones[i];
                    //update charging status
                    DataSource.drones[i] = d;

                    break;
                }
            }
            for (int i = 0; i < DataSource.baseStations.Count; i++)//find the station to update
            {
                if (DataSource.baseStations[i].Id == station.Id)
                {
                    Station s = DataSource.baseStations[i];
                    s.ChargeSlots--;
                        DataSource.baseStations[i] = s;
                    break;
                }
            }
            AddCharging(drone.Id, station.Id);//adds a new charging object to the list

        }
        public  void ReleaseChargingDrone(Drone drone)//release drone from charging
        {
            bool b = false;
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == drone.Id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("drone doesn't exist");

            for (int i = 0; i < DataSource.drones.Count; i++)//goes over the list of drones to find the drone to update
            {
                if (DataSource.drones[i].Id == drone.Id)//when the drone is found we updat status
                {
                    Drone d = DataSource.drones[i];
                    //update charging status
                    DataSource.drones[i] = d;
                    foreach (DroneCharge charge in DataSource.inChargeing)//goes over the list of charging and looks for the one with the drone that was given to release
                    {
                        if (charge.DroneId == drone.Id)//if its found we remove it from the list
                        {
                            for (int j = 0; j < DataSource.baseStations.Count(); j++)//goes over the station list to find the station that the given drone was charging at.
                            {
                                if (DataSource.baseStations[j].Id == charge.StationId)//if found - update number of available charging slots
                                {
                                    Station s = DataSource.baseStations[j];
                                    s.ChargeSlots++;
                                    DataSource.baseStations[j] = s;
                                    break;
                                }
                            }
                            DataSource.inChargeing.Remove(charge);
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public  Station PrintStation(int id)//display station by station ID
        {
            foreach (Station station in DataSource.baseStations)//goes over the list of stations to find the station with that ID
            {
                if (station.Id == id)//when found- displays the station 
                {
                    return station;
                }
            }
        throw new NotFoundException("station doesn't exist");

        }
        public  Drone PrintDrone(int id)//display drone by drone ID
        {
            foreach (Drone drone in DataSource.drones)//goes over the list of drones to find the drone with that ID
            {
                if (drone.Id == id)
                {
                    return drone;
                }
            }
           throw new NotFoundException("drone doesn't exist");
        }
        public  Customer PrintCustomer(int id)//display customer by customer ID
        {
            foreach (Customer customer in DataSource.customers)//goes over the list of customers to find the customer with that ID
            {
                if (customer.Id == id)//when found- displays the customer 
                {
                    return customer;
                }
            }
            throw new NotFoundException("customer doesn't exist");
            
        }
        public  Parcel PrintParcel(int id)//display parcel by parcel ID
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over the list of parcels to find the parcel with that ID
            {
                if (parcel.Id == id)//when found- displays the parcel
                {
                    return parcel;
                }
            }
            throw new NotFoundException("parcel doesn't exist");
            
        }
        public IEnumerable<Station> PrintAllStation()//display all stations
        {
            foreach (Station station in DataSource.baseStations)//goes over all the stations and prints all of them
            {
               yield return station;
            }
        }
        public IEnumerable<Drone> PrintAllDrone()//display all drones
        {
            foreach (Drone drone in DataSource.drones)//goes over all the drones and prints all of them
            {
                yield return drone;
            }
        }
        public IEnumerable<Customer> PrintAllCustomer()//display all customers
        {
            foreach (Customer customer in DataSource.customers)//goes over all the customers and prints all of them
            {
                yield return customer;
            }
        }
        public IEnumerable<Parcel> PrintAllParcel()//display all parcels
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and prints all of them
            {
                yield return parcel;
            }
        }
        public IEnumerable<Parcel> PrintParcelsWithNoDrone()//display all parcels that are not assigned to any drone
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and if they are not assigned to any drone - print the,
            {
                if (parcel.DroneId == 0)//not assigned to drone= drone ID is 0
                    yield return parcel;
            }
        }
        public IEnumerable<Station> PrintStationWithChargeSlots()//display all stations with available charging slots 
        {
            foreach (Station station in DataSource.baseStations)//goes over all the stations and if the station has any available charging slots - it displays it
            {
                if (station.ChargeSlots > 0)//station with available charging slots= at least one charging slot
                    yield return station;
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
        public double[] powerUse()
        {
            double[] power = new double[5];
            power[0] = DataSource.Config.availablePK;
            power[1] = DataSource.Config.lightPK;
            power[2] = DataSource.Config.mediumPK;
            power[3] = DataSource.Config.heavyPK;
            power[4] = DataSource.Config.chargingPH;
            return power;
        }
    }

}
