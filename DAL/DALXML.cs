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

        string dronesPath = @"DronesXML.xml"; //XElement
        string stationsPath = @"StationsXML.xml"; //XMLSerializer
        string parcelsPath = @"ParcelsXML.xml"; //XMLSerializer
        string customersPath = @"CustomersXML.xml"; //XMLSerializer
        string dronesInChargingPath = @"DronesInChargingXML.xml"; //XElement
       
        #endregion

        public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots)//add a new station
        {
            XElement stationRootElem = XMLTools.LoadListFromXMLElement(stationsPath);
            //XElement station = (from b in stationRootElem.Elements()
            //                    where b.Element("LicenseNumber").Value == station.LicenseNumber
            //                    select b).FirstOrDefault();

            //if (station != null)
            //    throw new DO.InvalidInformationException("Duplicate bus license number");

            //XElement busElem = new XElement("Bus", new XElement("LicenseNumber", bus.LicenseNumber),
            //                      new XElement("RunningDate", bus.RunningDate.ToString()),
            //                      new XElement("LastTreatment", bus.LastTreatment.ToString()),
            //                      new XElement("Fuel", bus.Fuel.ToString()),
            //                      new XElement("KM", bus.KM.ToString()),
            //                      new XElement("BeforeTreatKM", bus.BeforeTreatKM.ToString()),
            //                      new XElement("Status", bus.Status.ToString()));

            //busesRootElem.Add(busElem);

            XMLTools.SaveListToXMLElement(stationRootElem, stationsPath);
            return;
        }
        public void AddDrone(int Id, string model, WeightCategories maxWeight)//add a new drone
        {
            return;

        }
        public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude)//add a new customer
        {
            return;

        }
        public void AddParcel(Parcel p)//add new parcel
        {
            return;

        }
        public void AddCharging(int dId, int sId)//adds a drone to charging
        {
            return;

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
