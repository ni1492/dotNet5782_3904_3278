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

        string dronesPath = @"DroneXML.xml"; //XElement
        string stationsPath = @"StationXML.xml"; //XMLSerializer
        string parcelsPath = @"ParcelXML.xml"; //XMLSerializer
        string customersPath = @"CustomerXML.xml"; //XMLSerializer
        string dronesInChargingPath = @"DroneChargingXML.xml"; //XElement

        #endregion

        public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots)//add a new station
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == Id.ToString()
                                select b).FirstOrDefault();

            if (station != null)
                throw new DO.InvalidInformationException("Duplicate bus license number");

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
                throw new DO.InvalidInformationException("Duplicate bus license number");

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
                throw new DO.InvalidInformationException("Duplicate bus license number");

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
                throw new DO.InvalidInformationException("Duplicate bus license number");

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
            XElement droneRootElem = XMLTools.LoadListFromXMLElement(dronesPath);
            XElement drone = (from b in droneRootElem.Elements()
                              where b.Element("Id").Value == dId.ToString()
                              select b).FirstOrDefault();

            if (drone == null)
                throw new DO.InvalidInformationException("Duplicate bus license number");
          
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            XElement station = (from b in stationRootElem.Elements()
                                where b.Element("Id").Value == sId.ToString()
                                select b).FirstOrDefault();

            if (station == null)
                throw new DO.InvalidInformationException("Duplicate bus license number");
            XElement chargingRootElem = XMLTools.LoadListFromXMLElement(dronesInChargingPath);
            XElement charging = (from b in chargingRootElem.Elements()
                                     where b.Element("DroneId").Value == dId.ToString()
                                     select b).FirstOrDefault();

            if (charging != null)
                throw new DO.InvalidInformationException("Duplicate bus license number");

            XElement chargingElem = new XElement("DroneCharge",
                                  new XElement("DroneId", dId.ToString()),
                                  new XElement("StationId", sId.ToString()));

            chargingRootElem.Add(chargingElem);

            XMLTools.SaveListToXMLElement(chargingRootElem, dronesInChargingPath);
        }
        public void Match(int pId, int dId) //matches a drone to a parcel
        {
            return;


        }
        public void PickUpTime(Parcel parcel)//Update pickup parcel by drone
        {
            return;

        }
        public void DeliveryTime(Parcel parcel)//Update delivery parcel status
        {
            return;

        }
        public void ChargingDrone(int dId, int sId)//send drone to charge
        {
            return;

        }
        public void ReleaseChargingDrone(int id)//release drone from charging
        {
            return;

        }
        public IEnumerable<Station> DisplayStations(Predicate<Station> match)
        {
            return null;

        }
        public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match)
        {
            return null;

        }
        public IEnumerable<Customer> DisplayCustomers(Predicate<Customer> match)
        {
            return null;


        }
        public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match)
        {
            return null;

        }

        public Parcel ConvertParcel(int id)//returns the parcel of the ID that was given
        {
            return new();

        }
        public Drone ConvertDrone(int id)//returns the drone of the ID that was given
        {
            return new();


        }
        public Station ConvertStation(int id)//returns the station of the ID that was given
        {
            return new();

        }
        public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)//calculate the distance between two coordinates
        {
            return new();

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
            return;

        }
        public void deleteCustomer(int id)
        {
            return;
        }
        public void deleteStation(int id)
        {
            return;
        }
        public void deleteParcel(int id)
        {
            return;
        }
    }

}
