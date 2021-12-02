using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DAL
{
    namespace IDAL
    {
        public interface IDal
        {
            public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots);//add a new station
            public void AddDrone(int Id, string model, WeightCategories maxWeight);//add a new drone
            public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude);//add a new customer
            public void AddParcel(int id, int sId, int tId, WeightCategories weight, Priorities priority, int dId);//add new parcel
            public void AddCharging(int dId, int sId);//adds a drone to charging
            public void Match(int pId, int dId); //matches a drone to a parcel
            public void PickUpTime(Parcel parcel);//Update pickup parcel by drone
            public void DeliveryTime(Parcel parcel);//Update delivery parcel status
            public void ChargingDrone(Drone drone, Station station);//send drone to charge
            public void ReleaseChargingDrone(Drone drone);//release drone from charging
            public Station PrintStation(int id);//display station by station ID
            public Drone PrintDrone(int id);//display drone by drone ID
            public Customer PrintCustomer(int id);//display customer by customer ID
            public Parcel PrintParcel(int id);//display parcel by parcel ID
            public IEnumerable<Station> PrintAllStation();//display all stations
            public IEnumerable<Drone> PrintAllDrone();//display all drones
            public IEnumerable<Customer> PrintAllCustomer();//display all customers
            public IEnumerable<Parcel> PrintAllParcel();//display all parcels
            public IEnumerable<Parcel> PrintParcelsWithNoDrone();//display all parcels that are not assigned to any drone
            public IEnumerable<Station> PrintStationWithChargeSlots();//display all stations with available charging slots 
            public Parcel ConvertParcel(int id);//returns the parcel of the ID that was given
            public Drone ConvertDrone(int id);//returns the drone of the ID that was given
            public Station ConvertStation(int id);//returns the station of the ID that was given
            public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);
            public double[] powerUse();
            public IEnumerable<DroneCharge> displayChargings(int id);
        }
    }

}
