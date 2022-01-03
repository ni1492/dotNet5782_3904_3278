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

        string dronesPath = @"DroneXML.xml"; //XMLSerializer
        string stationsPath = @"StationXML.xml"; //XElement
        string parcelsPath = @"ParcelXML.xml"; //XMLSerializer
        string customersPath = @"CustomerXML.xml"; //XElement
        string dronesInChargingPath = @"DroneChargingXML.xml"; //XElement

        #endregion

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
        public void AddDrone(int Id, string model, WeightCategories maxWeight)//add a new drone
        {
            XElement droneRootElem = XMLTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in droneRootElem.Elements()
                              where b.Element("Id").Value == Id.ToString()
                              select b).FirstOrDefault();

            if (drone != null)
                throw new DO.ExistException("drone already exist");

            XElement droneElem = new XElement("Drone",
                                  new XElement("Id", Id.ToString()),
                                  new XElement("Model", model),
                                  new XElement("MaxWeight", maxWeight));

            droneRootElem.Add(droneElem);

            XMLTools.SaveListToXMLElement(droneRootElem, dronesPath);
        }
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
        public void AddParcel(Parcel p)//add new parcel
        {
            XElement parcelRootElem = XMLTools.LoadListFromXMLElement(parcelsPath);
            XElement parcel = (from b in parcelRootElem.Elements()
                               where b.Element("Id").Value == p.Id.ToString()
                               select b).FirstOrDefault();

            if (parcel != null)
                throw new DO.ExistException("parcel already exist");

            XElement parcelElem = new XElement("Parcel",
                           new XElement("Id", p.Id.ToString()),
                           new XElement("SenderId", p.SenderId.ToString()),
                           new XElement("TargetId", p.TargetId.ToString()),
                           new XElement("Weight", p.Weight),
                           new XElement("Priority", p.Priority),
                           new XElement("DroneId", p.DroneId.ToString()),
                           new XElement("Requested", p.Requested.ToString()),
                           new XElement("Scheduled", p.Scheduled.ToString()),
                           new XElement("PickedUp", p.PickedUp.ToString()),
                           new XElement("Delivered", p.Delivered.ToString()));

            parcelRootElem.Add(parcelElem);

            XMLTools.SaveListToXMLElement(parcelRootElem, parcelsPath);
        }
        public void AddCharging(int dId, int sId)//adds a drone to charging
        {
            XElement chargingRootElem = XMLTools.LoadListFromXMLElement(dronesInChargingPath);
            XElement charging = (from b in chargingRootElem.Elements()
                                 where b.Element("DroneId").Value == dId.ToString()
                                 select b).FirstOrDefault();


            XElement chargingElem = new XElement("DroneCharge",
                                  new XElement("DroneId", dId.ToString()),
                                  new XElement("StationId", sId.ToString()));

            chargingRootElem.Add(chargingElem);

            XMLTools.SaveListToXMLElement(chargingRootElem, dronesInChargingPath);
        }
        public void Match(int pId, int dId) //matches a drone to a parcel
        {
            XElement parcelRootElem = XMLTools.LoadListFromXMLElement(parcelsPath);
            XElement parcel = (from b in parcelRootElem.Elements()
                               where b.Element("Id").Value == pId.ToString()
                               select b).FirstOrDefault();

            XElement droneRootElem = XMLTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in droneRootElem.Elements()
                              where b.Element("Id").Value == dId.ToString()
                              select b).FirstOrDefault();

            if (drone == null)
                throw new DO.NotFoundException("drone doesn't exist");


            if (parcel != null)
            {
                parcel.Element("DroneId").Value = dId.ToString();
                parcel.Element("Scheduled").Value = DateTime.Now.ToString();
                XMLTools.SaveListToXMLElement(parcelRootElem, parcelsPath);
            }
            else
                throw new DO.NotFoundException("parcel doesn't exist");
        }
        public void PickUpTime(Parcel parcel)//Update pickup parcel by drone
        {
            XElement parcelRootElem = XMLTools.LoadListFromXMLElement(parcelsPath);
            XElement Parcel = (from b in parcelRootElem.Elements()
                               where b.Element("Id").Value == parcel.Id.ToString()
                               select b).FirstOrDefault();
            if (Parcel == null)
                throw new DO.NotFoundException("parcel doesn't exist");
            Parcel.Element("PickedUp").Value = DateTime.Now.ToString();
            XMLTools.SaveListToXMLElement(parcelRootElem, parcelsPath);
        }
        public void DeliveryTime(Parcel parcel)//Update delivery parcel status
        {
            XElement parcelRootElem = XMLTools.LoadListFromXMLElement(parcelsPath);
            XElement Parcel = (from b in parcelRootElem.Elements()
                               where b.Element("Id").Value == parcel.Id.ToString()
                               select b).FirstOrDefault();
            if (Parcel == null)
                throw new DO.NotFoundException("parcel doesn't exist");
            Parcel.Element("Delivered").Value = DateTime.Now.ToString();
            XMLTools.SaveListToXMLElement(parcelRootElem, parcelsPath);
        }
        public void ChargingDrone(int dId, int sId)//send drone to charge
        {
            XElement droneRootElem = XMLTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in droneRootElem.Elements()
                              where b.Element("Id").Value == dId.ToString()
                              select b).FirstOrDefault();

            if (drone == null)
                throw new DO.NotFoundException("drone dosen't exist");

            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == sId.ToString()
                                select b).FirstOrDefault();

            if (station == null)
                throw new DO.NotFoundException("station dosen't exist");

            XElement chargingRootElem = XMLTools.LoadListFromXMLElement(dronesInChargingPath);
            XElement charging = (from b in chargingRootElem.Elements()
                                 where b.Element("DroneId").Value == dId.ToString()
                                 select b).FirstOrDefault();

            if (charging != null)
                throw new DO.ExistException("drone already in charge");


            station.Element("ChargeSlots").Value = (Int32.Parse(station.Element("ChargeSlots").Value) - 1).ToString();
            AddCharging(dId, sId);
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);
        }
        public void ReleaseChargingDrone(int id)//release drone from charging
        {
            XElement droneRootElem = XMLTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in droneRootElem.Elements()
                              where b.Element("Id").Value == id.ToString()
                              select b).FirstOrDefault();

            if (drone == null)
                throw new DO.NotFoundException("drone doesn't exist");

            XElement chargingRootElem = XMLTools.LoadListFromXMLElement(dronesInChargingPath);
            XElement charging = (from b in chargingRootElem.Elements()
                                 where b.Element("DroneId").Value == id.ToString()
                                 select b).FirstOrDefault();

            if (charging == null)
                throw new DO.ExistException("drone isn't in charge");

            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == charging.Element("StationId").Value
                                select b).FirstOrDefault();

            station.Element("ChargeSlots").Value = (Int32.Parse(station.Element("ChargeSlots").Value) + 1).ToString();
            charging.Remove();

            XMLTools.SaveListToXMLElement(chargingRootElem, dronesInChargingPath);
            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);

        }
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
        public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match)
        {
            List<Drone> ListBusLineStations = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            return from drone in ListBusLineStations
                   where match(drone)
                   select drone;
        }
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
        public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match)
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            return from parcel in parcels
                   where match(parcel)
                   select parcel;
        }

        public Parcel ConvertParcel(int id)//returns the parcel of the ID that was given
        {
            List<Parcel> parcels = XMLTools.LoadListFromXMLSerializer<Parcel>(parcelsPath);
            return parcels.Find(parcel => parcel.Id == id);
        }
        public Drone ConvertDrone(int id)//returns the drone of the ID that was given
        {
            List<Drone> drones = XMLTools.LoadListFromXMLSerializer<Drone>(dronesPath);
            return drones.Find(drone => drone.Id == id);

        }
        public Station ConvertStation(int id)//returns the station of the ID that was given
        {
            return new();

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
            return null;

        }
        public IEnumerable<DroneCharge> displayChargings(int id)
        {

            return null;

        }
        public IEnumerable<DroneCharge> displayDronesInCharge(Predicate<DroneCharge> match)
        {
            return null;

        }
        public void deleteDrone(int id)
        {
            XElement droneRootElem = XMLTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in droneRootElem.Elements()
                              where b.Element("Id").Value == id.ToString()
                              select b).FirstOrDefault();

            if (drone == null)
                throw new DO.NotFoundException("drone doesn't exist");
            drone.Remove();
            XMLTools.SaveListToXMLElement(droneRootElem, dronesPath);
        }
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
        public void deleteParcel(int id)
        {
            XElement parcelRootElem = XMLTools.LoadListFromXMLElement(parcelsPath);
            XElement parcel = (from b in parcelRootElem.Elements()
                               where b.Element("Id").Value == id.ToString()
                               select b).FirstOrDefault();

            if (parcel == null)
                throw new DO.NotFoundException("parcel doesn't exist");
            parcel.Remove();
            XMLTools.SaveListToXMLElement(parcelRootElem, parcelsPath);
        }
    }

}
