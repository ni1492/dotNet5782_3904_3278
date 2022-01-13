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
using System.Runtime.CompilerServices;

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
        /// <summary>
        /// add a new drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(int Id, string model, WeightCategories maxWeight)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);// take all the data from the XML file
            if (ListDrones.FirstOrDefault(s => s.Id == Id).Id != 0)
                throw new DO.ExistException("drone already exist");
            Drone drone = new Drone//new drone to add to the file
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
        /// <summary>
        ///matches a drone to a parcel
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Match(int pId, int dId) 
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == pId).Equals(null))//check that the parcel exist
                throw new DO.NotFoundException("parcel doesn't exist");

            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == dId).Equals(null))//check that the drone exist
                throw new DO.NotFoundException("drone doesn't exist");


            Parcel parcel = ListParcels.Find(parcel => parcel.Id == pId);
            parcel.DroneId = dId;//update the drone id in the parcel
            parcel.Scheduled = DateTime.Now;//update the matching time
            ListParcels.RemoveAll(parcel => parcel.Id == pId);
            ListParcels.Add(parcel);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }

        #endregion
        #region PickUp update
        /// <summary>
        ///Update pickup parcel by drone
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void PickUpTime(Parcel parcel)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == parcel.Id).Equals(null))//check id the parcel exist
                throw new DO.NotFoundException("parcel doesn't exist");

            Parcel p = ListParcels.Find(Parcel => Parcel.Id == parcel.Id);
            p.PickedUp = DateTime.Now;//update the time
            ListParcels.RemoveAll(Parcel => Parcel.Id == parcel.Id);
            ListParcels.Add(p);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }

        #endregion
        #region Delivery update
        /// <summary>
        ///Update delivery parcel status
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeliveryTime(Parcel parcel)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == parcel.Id).Equals(null))//check if the parcel exist
                throw new DO.NotFoundException("parcel doesn't exist");

            Parcel p = ListParcels.Find(Parcel => Parcel.Id == parcel.Id);
            p.Delivered = DateTime.Now;//update time
            ListParcels.RemoveAll(Parcel => Parcel.Id == parcel.Id);
            ListParcels.Add(p);

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);


        }

        #endregion
        #region display drones
        /// <summary>
        /// display drones
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match)
        {
            List<Drone> ListBusLineStations = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            return from drone in ListBusLineStations//goes over all the drones and return those who match(drone) return true
                   where match(drone)
                   select drone;
        }

        #endregion
        #region convert drone
        /// <summary>
        ///returns the drone of the ID that was given
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone ConvertDrone(int id)
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            return drones.Find(drone => drone.Id == id);//return the Drone that match the id

        }

        #endregion
        #region power
        /// <summary>
        ///array with all the data about the power the drone need in the delivery
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double[] powerUse()
        {
            XElement configRootElem = XMLTools.LoadListFromXMLElement(configPath);

            double[] power = new double[5];
            int i = 0;
            foreach (var item in battery())//return the array with all the data about the power the drone need in the delivery
            {
                power[i++] = item;
                if (i == 5)
                    break;
            }
            return power;
        }
        private IEnumerable<double> battery()//help func to return the power data for the drone
        {
            XElement configRootElem = XMLTools.LoadListFromXMLElement(configPath);
            return from b in configRootElem.Elements() select double.Parse(b.Value);
        }
        #endregion
        #region delete drone
        /// <summary>
        ///remove drone from the file
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteDrone(int id)
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == id).Equals(null))//check the drone exist
                throw new DO.NotFoundException("drone dosen't exist");
            ListDrones.RemoveAll(drone => drone.Id == id);//remove the drone from the file

            XMLTools.SaveListToXMLSerializer(ListDrones, dronesPath);
        }
        #endregion
        #endregion
        #region STATION
        #region add station
        /// <summary>
        /// add a new station
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);// take all the data from the XML file
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
                                  new XElement("ChargeSlots", chargeSlots.ToString()));//new station to add

            stationRootElem.Add(stationElem);

            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);
        }
        #endregion
        #region display stations
        /// <summary>
        ///display stations
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                   select station;//select and conbert the station to return
        }

        #endregion
        #region convert station
        /// <summary>
        ///returns the station of the ID that was given
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station ConvertStation(int id)//returns the station of the ID that was given
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);

            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == id.ToString()
                                select b).FirstOrDefault();
            return new Station//new station to return
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
        /// <summary>
        ///display drone charging in the station
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> displayChargings(int id)
        {
            List<DroneCharge> charges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);
            return from charge in charges
                   where charge.StationId == id
                   select charge;//select all the drone that charging in this station

        }
        #endregion
        #region delete station
        /// <summary>
        ///delete station
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteStation(int id)
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == id.ToString()
                                select b).FirstOrDefault();//find the station to delete

            if (station == null)
                throw new DO.NotFoundException("station doesn't exist");
            station.Remove();
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);

        }
        #endregion
        #endregion
        #region PARCEL
        #region add parcel
        /// <summary>
        ///add new parcel
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel p)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);// take all the data from the XML file
            XElement counterRootElem = XMLTools.LoadListFromXMLElement(counterPath);
            int count = Int32.Parse(counterRootElem.Element("ParcelID").Value);
            if (p.Id == 0)//check if the parcel is a new- and then use the running number, else use the id given
            {
                p.Id = count++;
                counterRootElem.RemoveAll();
                XElement counter =new XElement("ParcelID", count.ToString());
                counterRootElem.Add(counter);
                XMLTools.SaveListToXMLElement(counterRootElem, counterPath);
            }
            Parcel parcel = new Parcel//new parcel to add
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
        /// <summary>
        ///display parcels
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            return from parcel in parcels
                   where match(parcel)
                   select parcel;//ereturn all the prcel that matched
        }

        #endregion
        #region convert parcel
        /// <summary>
        ///returns the parcel of the ID that was given
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel ConvertParcel(int id)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            return parcels.Find(parcel => parcel.Id == id);//return the parcel
        }

        #endregion
        #region delete parcel
        /// <summary>
        ///delete parcel
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteParcel(int id)
        {
            List<Parcel> ListParcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            if (ListParcels.FirstOrDefault(s => s.Id == id).Equals(null))
                throw new DO.NotFoundException("parcel doesn't exist");


            ListParcels.RemoveAll(Parcel => Parcel.Id == id);//find the parcel to delete

            XMLTools.SaveListToXMLSerializer(ListParcels, parcelsPath);

        }
        #endregion
        #endregion
        #region CUSTOMER
        #region add customer
        /// <summary>
        ///add a new customer
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customersPath);// take all the data from the XML file
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
                                  new XElement("Lattitude", lattitude.ToString()));//new element with the customer data to add

            customerRootElem.Add(customerElem);

            XMLTools.SaveListToXMLElement(customerRootElem, customersPath);
        }

        #endregion
        #region display customers
        /// <summary>
        ///display customers
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                   select customer;//find and convert all the customer that matched to return
        }

        #endregion
        #region delete customer
        /// <summary>
        ///delet customer 
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteCustomer(int id)
        {
            XElement customerRootElem = XMLTools.LoadListFromXMLElement(customersPath);
            XElement customer = (from b in customerRootElem.Elements()
                                 where b.Element("Id").Value == id.ToString()
                                 select b).FirstOrDefault();//find the customer to delete

            if (customer == null)
                throw new DO.NotFoundException("customer doesn't exist");
            customer.Remove();
            XMLTools.SaveListToXMLElement(customerRootElem, customersPath);
        }
        #endregion
        #endregion
        #region CHARGING
        #region add charging
        /// <summary>
        ///adds a drone to charging
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCharging(int dId, int sId)
        {
            List<DroneCharge> ListDrones = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);

            DroneCharge drone = new DroneCharge//new drone to charge
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
        /// <summary>
        ///send drone to charge
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ChargingDrone(int dId, int sId)
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

            AddCharging(dId, sId);//after checking that the drone can be charged in the station call to the func that will add the charge
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);
        }

        #endregion
        #region release drone frome charging 
        /// <summary>
        ///release drone to charge
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseChargingDrone(int id)//release drone from charging
        {
            List<Drone> ListDrones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            if (ListDrones.FirstOrDefault(s => s.Id == id).Equals(null))
                throw new DO.NotFoundException("drone dosen't exist");


            List<DroneCharge> ListCharges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);
            if (ListCharges.FirstOrDefault(d => d.DroneId == id).Equals(null))
                throw new DO.NotFoundException("drone dosen't in charge");

            int stationID = ListCharges.Find(d => d.DroneId == id).StationId;
            ListCharges.RemoveAll(d => d.DroneId == id);//remove the drone from the charging
            XMLTools.SaveListToXMLSerializer(ListCharges, dronesInChargingPath);


            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == stationID.ToString()
                                select b).FirstOrDefault();

            station.Element("ChargeSlots").Value = (Int32.Parse(station.Element("ChargeSlots").Value) + 1).ToString();//update the station
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);

        }

        #endregion
        #region CalculateDistance
        /// <summary>
        ///calculate distance between two points
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        /// <summary>
        ///display drone in charge
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> displayDronesInCharge(Predicate<DroneCharge> match)
        {
            List<DroneCharge> charges = XMLTools.LoadListFromXMLSerializer<DroneCharge>(dronesInChargingPath);
            return from charge in charges
                   where match(charge)
                   select charge;//return all the drone that matched

        }
        #endregion
        #endregion
        #region USER
        #region add user
        /// <summary>
        ///add user
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddUser(int id, string userN, string email, string password, bool isManager)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);// take all the data from the XML file
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
                                  new XElement("HashedPassword", PasswordHandler.generateNewPassword(password, salt).ToString()));//new element to add the user

            userRootElem.Add(userElem);

            XMLTools.SaveListToXMLElement(userRootElem, usersPath);
        }
        #endregion
        #region delete user
        /// <summary>
        ///delete user
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteUser(int id)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            XElement user = (from b in userRootElem.Elements()
                             where b.Element("Id").Value == id.ToString()
                             select b).FirstOrDefault();//find the user to delete

            if (user == null)
                throw new DO.ExistException("user dosen't exist");

            user.Remove();
            XMLTools.SaveListToXMLElement(userRootElem, usersPath);
        }

        #endregion
        #region display users
        /// <summary>
        ///display users
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                   select user;//find and convert all the user matched and return them



        }

        #endregion
        #region find user
        /// <summary>
        ///check if the password correct
        ///</summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool userCorrect(string userN, string password, bool isManager)
        {
            XElement userRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            XElement user = (from b in userRootElem.Elements()
                             where b.Element("UserName").Value == userN.ToString()
                             select b).FirstOrDefault();//find the user
            if (user == null)
                return false;
            //compaire the pass with the right pass
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