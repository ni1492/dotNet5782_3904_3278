using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using DAL.DalApi;
using System.Xml.Linq;
using System.Xml.Serialization;
using DAL;

namespace DALXML
{
    class DALXML : IDal
    {
        #region singelton
        internal static readonly DALXML instance = new DALXML();

        static DALXML()
        {
        }

        DALXML() { }
        public static DALXML Instance => instance;
        #endregion


        #region DS XML Files

        private readonly string dronesPath = @"DroneXML.xml"; //XMLSerializer
        private readonly string stationsPath = @"StationXML.xml"; //XElement
        private readonly string parcelsPath = @"ParcelXML.xml"; //XMLSerializer
        private readonly string customersPath = @"CustomerXML.xml"; //XElement
        private readonly string dronesInChargingPath = @"DroneChargeXML.xml"; //XMLSerializer
        private readonly string configPath = @"config.xml"; //XElement
        private readonly string counterPath = @"Counter.xml"; //XElement
        private readonly string usersPath = @"UserXML.xml"; //XElement


        #endregion

        #region DRONE
        #region add drone
        public void AddDrone(int Id, string model, WeightCategories maxWeight)//add a new drone
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == Id).Id != 0)
                throw new DO.ExistException("drone already exist");
            Drone drone = new Drone
            {
                Id = Id,
                Model = model,
                MaxWeight = maxWeight
            };
            ListDrones.Add(drone);
            XMLTools.SaveListToXMLSerializer(ListDrones, dronesPath);
        }

        #endregion
        #region match
        public void Match(int pId, int dId) //matches a drone to a parcel
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == pId).Equals(null))
                throw new DO.NotFoundException("parcel doesn't exist");

            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == dId).Equals(null))
                throw new DO.NotFoundException("drone doesn't exist");


            Parcel parcel = ListParcels.Find(parcel => parcel.Id == pId);
            parcel.DroneId = dId;
            parcel.Scheduled = DateTime.Now;
            ListParcels.RemoveAll(parcel => parcel.Id == pId);
            ListParcels.Add(parcel);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }

        #endregion
        #region PickUp update
        public void PickUpTime(Parcel parcel)//Update pickup parcel by drone
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == parcel.Id).Equals(null))
                throw new DO.NotFoundException("parcel doesn't exist");

            Parcel p = ListParcels.Find(Parcel => Parcel.Id == parcel.Id);
            p.PickedUp = DateTime.Now;
            ListParcels.RemoveAll(Parcel => Parcel.Id == parcel.Id);
            ListParcels.Add(p);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }

        #endregion
        #region Delivery update
        public void DeliveryTime(Parcel parcel)//Update delivery parcel status
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == parcel.Id).Equals(null))
                throw new DO.NotFoundException("parcel doesn't exist");

            Parcel p = ListParcels.Find(Parcel => Parcel.Id == parcel.Id);
            p.Delivered = DateTime.Now;
            ListParcels.RemoveAll(Parcel => Parcel.Id == parcel.Id);
            ListParcels.Add(p);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);


        }

        #endregion
        #region display drones
        public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match)
        {
            List<Drone> ListBusLineStations = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            return from drone in ListBusLineStations
                   where match(drone)
                   select drone;
        }

        #endregion
        #region convert drone
        public Drone ConvertDrone(int id)//returns the drone of the ID that was given
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            return drones.Find(drone => drone.Id == id);

        }

        #endregion
        #region power
        public double[] powerUse()
        {
            XElement configRootElem = XMLTools.LoadListFromXMLElement(configPath);

            double[] power = new double[5];
            int i = 0;
            foreach (var item in battery())
            {
                power[i++] = item;
                if (i == 5)
                    break;
            }
            return power;
        }
        private IEnumerable<double> battery()
        {
            XElement configRootElem = XMLTools.LoadListFromXMLElement(configPath);
            return from b in configRootElem.Elements() select double.Parse(b.Value);
        }
        #endregion
        #region delete drone
        public void deleteDrone(int id)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == id).Equals(null))
                throw new DO.NotFoundException("drone dosen't exist");
        }
        #endregion
        #endregion
        #region STATION
        #region add station
        public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots)//add a new station
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == Id.ToString()
                                select b).FirstOrDefault();

            if (station != null)
                throw new DO.ExistException("station already exist");

            XElement stationElem = new XElement("Station",
                                  new XElement("Id", Id.ToString()),
                                  new XElement("Name", name),
                                  new XElement("Longitude", longitude.ToString()),
                                  new XElement("Lattitude", lattitude.ToString()),
                                  new XElement("ChargeSlots", chargeSlots.ToString()));

            stationRootElem.Add(stationElem);

            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);
        }
        #endregion
        #region display stations
        public IEnumerable<Station> DisplayStations(Predicate<Station> match)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            return from b in stationRootElem.Elements()
                   let station = new Station()
                   {
                       Id = Int32.Parse(b.Element("Id").Value),
                       Name = b.Element("Name").Value,
                       Longitude = double.Parse(b.Element("Longitude").Value),
                       Lattitude = double.Parse(b.Element("Lattitude").Value),
                       ChargeSlots = Int32.Parse(b.Element("ChargeSlots").Value)
                   }
                   where match(station)
                   select station;
        }

        #endregion
        #region convert station
        public Station ConvertStation(int id)//returns the station of the ID that was given
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == id.ToString()
                                select b).FirstOrDefault();
            return new Station
            {
                Id = Int32.Parse(station.Element("Id").Value),
                Name = station.Element("Name").Value,
                Longitude = double.Parse(station.Element("Longitude").Value),
                Lattitude = double.Parse(station.Element("Lattitude").Value),
                ChargeSlots = Int32.Parse(station.Element("ChargeSlots").Value)
            };
        }

        #endregion
        #region display charging
        public IEnumerable<DroneCharge> displayChargings(int id)
        {
            List<DroneCharge> charges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);
            return from charge in charges
                   where charge.StationId == id
                   select charge;

        }
        #endregion
        #region delete station
        public void deleteStation(int id)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == id.ToString()
                                select b).FirstOrDefault();

            if (station == null)
                throw new DO.NotFoundException("station doesn't exist");
            station.Remove();
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);

        }
        #endregion
        #endregion
        #region PARCEL
        #region add parcel
        public void AddParcel(Parcel p)//add new parcel
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);


            XElement counterRootElem = XMLTools.LoadListFromXMLElement(counterPath);
            int count = Int32.Parse(counterRootElem.Element("ParcelID").Value);
            if (p.Id == 0)
            {
                p.Id = count++;
                counterRootElem.RemoveAll();
                XElement counter =new XElement("ParcelID", count.ToString());
                counterRootElem.Add(counter);
                XMLTools.SaveListToXMLElement(counterRootElem, counterPath);
            }
            Parcel parcel = new Parcel
            {
                Id = p.Id,
                SenderId = p.SenderId,
                TargetId = p.TargetId,
                Weight = p.Weight,
                Priority = p.Priority,
                DroneId = p.DroneId,
                Requested = p.Requested,
                Scheduled = p.Scheduled,
                PickedUp = p.PickedUp,
                Delivered = p.Delivered
            };
            ListParcels.Add(parcel);
            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }

        #endregion
        #region display parcels
        public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            return from parcel in parcels
                   where match(parcel)
                   select parcel;
        }

        #endregion
        #region convert parcel
        public Parcel ConvertParcel(int id)//returns the parcel of the ID that was given
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            return parcels.Find(parcel => parcel.Id == id);
        }

        #endregion
        #region delete parcel
        public void deleteParcel(int id)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == id).Equals(null))
                throw new DO.NotFoundException("parcel doesn't exist");


            ListParcels.RemoveAll(Parcel => Parcel.Id == id);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }
        #endregion
        #endregion
        #region CUSTOMER
        #region add customer
        public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)//add a new customer
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customersPath);
            XElement customer = (from b in customerRootElem.Elements()
                                 where b.Element("Id").Value == Id.ToString()
                                 select b).FirstOrDefault();

            if (customer != null)
                throw new DO.ExistException("customer already exist");

            XElement customerElem = new XElement("Customer",
                                  new XElement("Id", Id.ToString()),
                                  new XElement("Name", name),
                                  new XElement("Phone", phone),
                                  new XElement("Longitude", longitude.ToString()),
                                  new XElement("Lattitude", lattitude.ToString()));

            customerRootElem.Add(customerElem);

            XMLTools.SaveListToXMLElement(customerRootElem, customersPath);
        }

        #endregion
        #region display customers
        public IEnumerable<Customer> DisplayCustomers(Predicate<Customer> match)
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customersPath);

            return from b in customerRootElem.Elements()
                   let customer = new Customer()
                   {
                       Id = Int32.Parse(b.Element("Id").Value),
                       Name = b.Element("Name").Value,
                       Phone = b.Element("Phone").Value,
                       Longitude = double.Parse(b.Element("Longitude").Value),
                       Lattitude = double.Parse(b.Element("Lattitude").Value)
                   }
                   where match(customer)
                   select customer;
        }

        #endregion
        #region delete customer
        public void deleteCustomer(int id)
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customersPath);
            XElement customer = (from b in customerRootElem.Elements()
                                 where b.Element("Id").Value == id.ToString()
                                 select b).FirstOrDefault();

            if (customer == null)
                throw new DO.NotFoundException("customer doesn't exist");
            customer.Remove();
            XMLTools.SaveListToXMLElement(customerRootElem, customersPath);
        }
        #endregion
        #endregion
        #region CHARGING
        #region add charging
        public void AddCharging(int dId, int sId)//adds a drone to charging
        {
            List<DroneCharge> ListDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);

            DroneCharge drone = new DroneCharge
            {
                DroneId = dId,
                StationId = sId,
                chargeTime = DateTime.Now
            };
            ListDrones.Add(drone);
            XMLTools.SaveListToXMLSerializer(ListDrones, dronesInChargingPath);

        }

        #endregion
        #region charging drone
        public void ChargingDrone(int dId, int sId)//send drone to charge
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == dId).Equals(null))
                throw new DO.NotFoundException("drone dosen't exist");



            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == sId.ToString()
                                select b).FirstOrDefault();

            if (station == null)
                throw new DO.NotFoundException("station dosen't exist");


            List<DroneCharge> ListCharging = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);

            if (ListCharging.FirstOrDefault(s => s.DroneId == dId).DroneId == dId)
                throw new DO.NotFoundException("charge exist");

            station.Element("ChargeSlots").Value = (Int32.Parse(station.Element("ChargeSlots").Value) - 1).ToString();

            AddCharging(dId, sId);
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);
        }

        #endregion
        #region release drone frome charging 
        public void ReleaseChargingDrone(int id)//release drone from charging
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == id).Equals(null))
                throw new DO.NotFoundException("drone dosen't exist");


            List<DroneCharge> ListCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);
            if (ListCharges.FirstOrDefault(d => d.DroneId == id).Equals(null))
                throw new DO.NotFoundException("drone dosen't exist");
            int stationID = ListCharges.Find(d => d.DroneId == id).StationId;
            ListCharges.RemoveAll(d => d.DroneId == id);
            XMLTools.SaveListToXMLSerializer(ListCharges, dronesInChargingPath);


            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == stationID.ToString()
                                select b).FirstOrDefault();

            station.Element("ChargeSlots").Value = (Int32.Parse(station.Element("ChargeSlots").Value) + 1).ToString();
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);

        }

        #endregion
        #region CalculateDistance
        public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)//calculate the distance between two coordinates
        {
            double lat1 = latitude1 * (Math.PI / 180.0);
            double long1 = longitude1 * (Math.PI / 180.0);
            double lat2 = latitude2 * (Math.PI / 180.0);
            double long2 = longitude2 * (Math.PI / 180.0) - long1;
            double distance = Math.Pow(Math.Sin((lat2 - lat1) / 2.0), 2.0) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(long2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(distance), Math.Sqrt(1.0 - distance))) / 1000;
        }

        #endregion
        #region display charges
        public IEnumerable<DroneCharge> displayDronesInCharge(Predicate<DroneCharge> match)
        {
            List<DroneCharge> charges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);
            return from charge in charges
                   where match(charge)
                   select charge;

        }
        #endregion
        #endregion
        #region USER
        #region add user
        public void AddUser(int id, string userN, string email, string password, bool isManager)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            XElement user = (from b in userRootElem.Elements()
                             where b.Element("Id").Value == id.ToString()
                             select b).FirstOrDefault();

            if (user != null)
                throw new DO.ExistException("user already exist");
            int salt = PasswordHandler.generateSalt();

            XElement userElem = new XElement("User",
                                  new XElement("Id", id.ToString()),
                                  new XElement("UserName", userN),
                                  new XElement("Email", email),
                                  new XElement("IsManager", isManager.ToString()),
                                  new XElement("Salt", salt.ToString()),
                                  new XElement("HashedPassword", PasswordHandler.generateNewPassword(password, salt).ToString()));

            userRootElem.Add(userElem);

            XMLTools.SaveListToXMLElement(userRootElem, usersPath);
        }
        #endregion
        #region delete user
        public void deleteUser(int id)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            XElement user = (from b in userRootElem.Elements()
                             where b.Element("Id").Value == id.ToString()
                             select b).FirstOrDefault();

            if (user == null)
                throw new DO.ExistException("user dosen't exist");

            user.Remove();
            XMLTools.SaveListToXMLElement(userRootElem, usersPath);
        }

        #endregion
        #region display users
        //public User displayUser(string userN)
        //{
        //    XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
        //    XElement user = (from b in userRootElem.Elements()
        //                        where b.Element("UserName").Value == userN.ToString()
        //                        select b).FirstOrDefault();
        //    if(user==null)
        //        throw new DO.NotFoundException("station doesn't exist");

        //    return new User()
        //    {
        //        Id = Int32.Parse(user.Element("Id").Value),
        //        UserName = user.Element("UserName").Value,
        //        Email = user.Element("Email").Value,
        //        IsManager = bool.Parse(user.Element("IsManager").Value),
        //        Salt = Int32.Parse(user.Element("Salt").Value),
        //        HashedPassword = user.Element("HashedPassword").Value
        //    };

          
        //}
        public IEnumerable<User> displayUsers(Predicate<User> match)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            return from b in userRootElem.Elements()
                   let user = new User()
                   {
                       Id = Int32.Parse(b.Element("Id").Value),
                       UserName = b.Element("UserName").Value,
                       Email = b.Element("Email").Value,
                       IsManager = bool.Parse(b.Element("IsManager").Value),
                       Salt = Int32.Parse(b.Element("Salt").Value),
                       HashedPassword = b.Element("HashedPassword").Value
                   }
                   where match(user)
                   select user;



        }

        #endregion
        #region find user
        public bool userCorrect(string userN, string password, bool isManager)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            XElement user = (from b in userRootElem.Elements()
                             where b.Element("UserName").Value == userN.ToString()
                             select b).FirstOrDefault();
            if (bool.Parse(user.Element("IsManager").Value) == isManager &&
               PasswordHandler.checkPassword(password, user.Element("HashedPassword").Value, Int32.Parse(user.Element("Salt").Value)))
                return true;
            else
                return false;
        }
        #endregion
        #endregion

    }

}