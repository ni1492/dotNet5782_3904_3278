using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAL.DalApi;


namespace DALObject
{
    class DALObject : IDal
    {
        #region singelton
        internal static readonly DALObject instance = new DALObject();
        static DALObject()
        {
            DataSource.Initialize();
        }
        DALObject() { }
       public static DALObject Instance => instance;
        #endregion
        //public DALObject()
        //{
        //    DataSource.Initialize();
        //}

        #region station
        public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots)//add a new station
        {
            //initialize new station object:
            foreach (Station s in DataSource.baseStations)
            {
                if (s.Id == Id)
                {
                    throw new ExistException("station alredy exist");
                }
            }
            DO.Station station = new DO.Station();
            station.Id = Id;
            station.Name = name;
            station.Longitude = longitude;
            station.Lattitude = lattitude;
            station.ChargeSlots = chargeSlots;
            //adds to base station list:
            DataSource.baseStations.Add(station);
        }
        public IEnumerable<Station> DisplayStations(Predicate<Station> match)
        {
            foreach (Station station in DataSource.baseStations)//goes over all the stations and prints all of them
            {
                if (match(station))
                    yield return station;
            }
        }
        //public Station PrintStation(int id)//display station by station ID
        //{
        //    foreach (Station station in DataSource.baseStations)//goes over the list of stations to find the station with that ID
        //    {
        //        if (station.Id == id)//when found- displays the station 
        //        {
        //            return station;
        //        }
        //    }
        //    throw new NotFoundException("station doesn't exist");

        //}
        //public IEnumerable<Station> PrintAllStation()//display all stations
        //{
        //    foreach (Station station in DataSource.baseStations)//goes over all the stations and prints all of them
        //    {
        //        yield return station;
        //    }
        //}
        //public IEnumerable<Station> PrintStationWithChargeSlots()//display all stations with available charging slots 
        //{
        //    foreach (Station station in DataSource.baseStations)//goes over all the stations and if the station has any available charging slots - it displays it
        //    {
        //        if (station.ChargeSlots > 0)//station with available charging slots= at least one charging slot
        //            yield return station;
        //    }
        //}
        public Station ConvertStation(int id)//returns the station of the ID that was given
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
        public void deleteStation(int id)
        {
            bool b = false;
            foreach (Station s in DataSource.baseStations)
            {
                if (s.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("station doesn't exist");
            DataSource.baseStations.RemoveAll(Station => Station.Id == id);
        }
        #endregion

        #region drone
        public void AddDrone(int Id, string model, WeightCategories maxWeight)//add a new drone
        {
            //initialize new drone object:
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == Id)
                {
                    throw new ExistException("drone alredy exist");
                }
            }
            DO.Drone drone = new DO.Drone();
            drone.Id = Id;
            drone.Model = model;
            drone.MaxWeight = maxWeight;
            //  drone.Status = status;
            //  drone.Battery = battery;
            //adds to drones list:
            DataSource.drones.Add(drone);
        }

        public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match)
        {
            foreach (Drone drone in DataSource.drones)//goes over all the stations and prints all of them
            {
                if (match(drone))
                    yield return drone;
            }
        }
        //public Drone PrintDrone(int id)//display drone by drone ID
        //{
        //    foreach (Drone drone in DataSource.drones)//goes over the list of drones to find the drone with that ID
        //    {
        //        if (drone.Id == id)
        //        {
        //            return drone;
        //        }
        //    }
        //    throw new NotFoundException("drone doesn't exist");
        //}
        //public IEnumerable<Drone> PrintAllDrone()//display all drones
        //{
        //    foreach (Drone drone in DataSource.drones)//goes over all the drones and prints all of them
        //    {
        //        yield return drone;
        //    }
        //}
        public Drone ConvertDrone(int id)//returns the drone of the ID that was given
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
        public void deleteDrone(int id)
        {
            bool b = false;
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("drone doesn't exist");
            DataSource.drones.RemoveAll(Drone => Drone.Id == id);
        }

        #endregion

        #region customer
        public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)//add a new customer
        {
            //initialize new customer object:
            foreach (Customer c in DataSource.customers)
            {
                if (c.Id == Id)
                {
                    throw new ExistException("customer alredy exist");
                }
            }
            DO.Customer customer = new DO.Customer();
            customer.Id = Id;
            customer.Name = name;
            customer.Phone = phone;
            customer.Longitude = longitude;
            customer.Lattitude = lattitude;
            //adds to customers list:
            DataSource.customers.Add(customer);
        }

        public IEnumerable<Customer> DisplayCustomers(Predicate<Customer> match)
        {
            foreach (Customer customer in DataSource.customers)//goes over all the stations and prints all of them
            {
                if (match(customer))
                    yield return customer;
            }
        }
        //public Customer PrintCustomer(int id)//display customer by customer ID
        //{
        //    foreach (Customer customer in DataSource.customers)//goes over the list of customers to find the customer with that ID
        //    {
        //        if (customer.Id == id)//when found- displays the customer 
        //        {
        //            return customer;
        //        }
        //    }
        //    throw new NotFoundException("customer doesn't exist");

        //}
        //public IEnumerable<Customer> PrintAllCustomer()//display all customers
        //{
        //    foreach (Customer customer in DataSource.customers)//goes over all the customers and prints all of them
        //    {
        //        yield return customer;
        //    }
        //}
        public void deleteCustomer(int id)
        {
            bool b = false;
            foreach (Customer c in DataSource.customers)
            {
                if (c.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("customer doesn't exist");
            DataSource.customers.RemoveAll(Customer => Customer.Id == id);
        }
        #endregion

        #region parcel
        public void AddParcel(Parcel p)//add new parcel
        {
            try
            {
                DisplayCustomers(customer => customer.Id == p.SenderId);
            }
            catch (Exception ex)
            {
                throw new NotFoundException("sender: " + ex.Message, ex);
            }
            try
            {

                DisplayCustomers(customer => customer.Id == p.TargetId);
            }
            catch (Exception ex)
            {

                throw new NotFoundException("target: " + ex.Message, ex);
            }
            DO.Parcel parcel = new DO.Parcel();
            if (p.Id!=0)
                parcel.Id = p.Id;
            else
                parcel.Id = DataSource.Config.ParcelID++;
            parcel.SenderId = p.SenderId;
            parcel.TargetId = p.TargetId;
            parcel.Weight = p.Weight;
            parcel.Priority = p.Priority;
            parcel.DroneId = p.DroneId;
            parcel.Requested = p.Requested;
            parcel.Scheduled = p.Scheduled;
            parcel.PickedUp = p.PickedUp;
            parcel.Delivered = p.Delivered;
            //add to parcels list
            DataSource.parcels.Add(parcel);
        }
        public void Match(int pId, int dId) //matches a drone to a parcel
        {
            bool b = false;
            foreach (Parcel p in DataSource.parcels)
            {
                if (p.Id == pId)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("parcel doesn't exist");
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

            for (int i = 0; i < DataSource.parcels.Count(); i++)
            {
                if (DataSource.parcels[i].Id == pId)
                {
                    Parcel p = DataSource.parcels[i];
                    p.DroneId = dId;
                    p.Scheduled = DateTime.Now;
                    DataSource.parcels[i] = p;
                    break;
                }
            }
        }
        public void PickUpTime(Parcel parcel)//Update pickup parcel by drone
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

            for (int i = 0; i < DataSource.drones.Count; i++)//goes over the list of drones to find the drone assigned to the parcel
            {
                if (DataSource.drones[i].Id == parcel.DroneId)//when the drone is found we update status
                {
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
        public void DeliveryTime(Parcel parcel)//Update delivery parcel status
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
                //if (DataSource.drones[i].Id == parcel.DroneId)//when the drone is found we updat status
                //{
                //    Drone d = DataSource.drones[i];
                //    //update pickup
                //    DataSource.drones[i] = d;
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
                //    break;
                //}
            }
        }

        public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match)
        {
            foreach (Parcel parcel in DataSource.parcels)//goes over all the stations and prints all of them
            {
                if (match(parcel))
                    yield return parcel;
            }
        }

        //public Parcel PrintParcel(int id)//display parcel by parcel ID
        //{
        //    foreach (Parcel parcel in DataSource.parcels)//goes over the list of parcels to find the parcel with that ID
        //    {
        //        if (parcel.Id == id)//when found- displays the parcel
        //        {
        //            return parcel;
        //        }
        //    }
        //    throw new NotFoundException("parcel doesn't exist");

        //}
        //public IEnumerable<Parcel> PrintAllParcel()//display all parcels
        //{
        //    foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and prints all of them
        //    {
        //        yield return parcel;
        //    }
        //}
        //public IEnumerable<Parcel> PrintParcelsWithNoDrone()//display all parcels that are not assigned to any drone
        //{
        //    foreach (Parcel parcel in DataSource.parcels)//goes over all the parcels and if they are not assigned to any drone - print the,
        //    {
        //        if (parcel.DroneId == 0)//not assigned to drone= drone ID is 0
        //            yield return parcel;
        //    }
        //}

        public Parcel ConvertParcel(int id)//returns the parcel of the ID that was given
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
        public void deleteParcel(int id)
        {
            bool b = false;
            foreach (Parcel p in DataSource.parcels)
            {
                if (p.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("parcel doesn't exist");
            DataSource.parcels.RemoveAll(Parcel => Parcel.Id == id);
        }
        #endregion

        #region charging
        public void AddCharging(int dId, int sId)//adds a drone to charging
        {
            bool b = false;
            foreach (Station s in DataSource.baseStations)
            {
                if (s.Id == sId)
                {
                    b = true;
                    break;
                }
            }
            if (!b)
                throw new NotFoundException("station doesn't exist");
            b = false;
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == dId)
                {
                    b = true;
                    break;
                }
            }
            if (!b)
                throw new NotFoundException("drone doesn't exist");

            //initialize new charging object:
            DroneCharge charging = new DroneCharge();
            charging.DroneId = dId;
            charging.StationId = sId;
            //  charging.chargTime = DateTime.Now;
            charging.chargeTime = DateTime.Now;
            //adds to charging list:
            DataSource.inCharging.Add(charging);
        }
        public void ChargingDrone(int dId, int sId)//send drone to charge
        {
            for (int i = 0; i < DataSource.baseStations.Count; i++)//find the station to update
            {
                if (DataSource.baseStations[i].Id == sId)
                {
                    Station s = DataSource.baseStations[i];
                    s.ChargeSlots--;
                    DataSource.baseStations[i] = s;
                    break;
                }
            }
            AddCharging(dId, sId);//adds a new charging object to the list
        }
        public void ReleaseChargingDrone(int id)//release drone from charging
        {
            bool b = false;
            foreach (Drone d in DataSource.drones)
            {
                if (d.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("drone doesn't exist");

            for (int i = 0; i < DataSource.drones.Count; i++)//goes over the list of drones to find the drone to update
            {
                if (DataSource.drones[i].Id == id)//when the drone is found we updat status
                {
                    foreach (DroneCharge charge in DataSource.inCharging)//goes over the list of charging and looks for the one with the drone that was given to release
                    {
                        if (charge.DroneId == id)//if its found we remove it from the list
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
                            DataSource.inCharging.Remove(charge);
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)//calculate the distance between two coordinates
        {
            double lat1 = latitude1 * (Math.PI / 180.0);
            double long1 = longitude1 * (Math.PI / 180.0);
            double lat2 = latitude2 * (Math.PI / 180.0);
            double long2 = longitude2 * (Math.PI / 180.0) - long1;
            double distance = Math.Pow(Math.Sin((lat2 - lat1) / 2.0), 2.0) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(long2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(distance), Math.Sqrt(1.0 - distance))) / 1000;
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
        public IEnumerable<DroneCharge> displayChargings(int id)
        {
            bool b = false;
            foreach (Station s in DataSource.baseStations)
            {
                if (s.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("station doesn't exist");
            foreach (var item in DataSource.inCharging)
            {
                if (item.StationId == id)
                    yield return item;
            }
        }
        public IEnumerable<DroneCharge> displayDronesInCharge(Predicate<DroneCharge> match)
        {
            foreach (var item in DataSource.inCharging)
            {
                if (match(item))
                    yield return item;
            }
        }

        #endregion

        #region user
        public void AddUser(int id, string userN, string email, string password, bool isManager)
        {
            foreach (User u in DataSource.users) //if a user with the same name or id already exists
            {
                if (u.Id == id||u.UserName==userN)
                {
                    throw new ExistException("user alredy exists");
                }
            }
            int salt = PasswordHandler.generateSalt();
            User temp = new()
            {
                Id = id,
                UserName = userN,
                Email = email,
                Salt = salt,
                HashedPassword = PasswordHandler.generateNewPassword(password, salt),
                IsManager = isManager
            };
            DataSource.users.Add(temp);
        }

        public void deleteUser(int id)
        {
            bool b = false;
            foreach (User u in DataSource.users)
            {
                if (u.Id == id)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("user doesn't exist");
            DataSource.users.RemoveAll(u => u.Id == id);
        }

        public User displayUser(string userN)
        {
            bool b = false;
            foreach (User u in DataSource.users)
            {
                if (u.UserName == userN)
                {
                    b = true;
                }
            }
            if (!b)
                throw new NotFoundException("user doesn't exist");
            return DataSource.users.Find(u => u.UserName==userN);
        }

        public bool userCorrect(string userN, string password, bool isManager)
        {
            return DataSource.users.Exists(u => u.UserName == userN && u.IsManager == isManager && PasswordHandler.checkPassword(password, u.HashedPassword, u.Salt));
        }
        #endregion
    }

}
