using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    namespace DalApi
    {
        public interface IDal
        {
            public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots);//add a new station
            public void AddDrone(int Id, string model, WeightCategories maxWeight);//add a new drone
            public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude);//add a new customer
            public void AddParcel(int sId, int tId, WeightCategories weight, Priorities priority, int dId);//add new parcel
            public void AddCharging(int dId, int sId);//adds a drone to charging
            public void Match(int pId, int dId); //matches a drone to a parcel
            public void PickUpTime(Parcel parcel);//Update pickup parcel by drone
            public void DeliveryTime(Parcel parcel);//Update delivery parcel status
            public void ChargingDrone(int dId, int sId);//send drone to charge
            public void ReleaseChargingDrone(int id);//release drone from charging

            public IEnumerable<Station> DisplayStations(Predicate<Station> match);
            public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match);
            public IEnumerable<Customer> DisplayCustomers(Predicate<Customer> match);
            public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match);

            //public Station PrintStation(int id);//display station by station ID
            //public IEnumerable<Station> PrintAllStation();//display all stations
            //public IEnumerable<Station> PrintStationWithChargeSlots();//display all stations with available charging slots 
            //public Drone PrintDrone(int id);//display drone by drone ID
            //public IEnumerable<Drone> PrintAllDrone();//display all drones
            //public Customer PrintCustomer(int id);//display customer by customer ID
            //public IEnumerable<Customer> PrintAllCustomer();//display all customers
            //public Parcel PrintParcel(int id);//display parcel by parcel ID
            //public IEnumerable<Parcel> PrintAllParcel();//display all parcels
            //public IEnumerable<Parcel> PrintParcelsWithNoDrone();//display all parcels that are not assigned to any drone
          
            public Parcel ConvertParcel(int id);//returns the parcel of the ID that was given
            public Drone ConvertDrone(int id);//returns the drone of the ID that was given
            public Station ConvertStation(int id);//returns the station of the ID that was given
            public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);
            public double[] powerUse();
            public IEnumerable<DroneCharge> displayChargings(int id);
            public void deleteDrone(int id);
            public void deleteCustomer(int id);
            public void deleteStation(int id);
            public void deleteParcel(int id);

        }

    }

}
