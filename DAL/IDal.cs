using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    namespace DalApi
    {
        public interface IDal
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddStation(int Id, string name, double longitude, double lattitude, int chargeSlots);//add a new station
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddDrone(int Id, string model, WeightCategories maxWeight);//add a new drone
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddCustomer(int Id, string name, string phone, double longitude, double lattitude);//add a new customer
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddParcel(Parcel p);//add new parcel
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void AddCharging(int dId, int sId);//adds a drone to charging
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void Match(int pId, int dId); //matches a drone to a parcel
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void PickUpTime(Parcel parcel);//Update pickup parcel by drone
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void DeliveryTime(Parcel parcel);//Update delivery parcel status
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void ChargingDrone(int dId, int sId);//send drone to charge
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void ReleaseChargingDrone(int id);//release drone from charging
            [MethodImpl(MethodImplOptions.Synchronized)]

            public IEnumerable<Station> DisplayStations(Predicate<Station> match);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<Drone> DisplayDrones(Predicate<Drone> match);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<Customer> DisplayCustomers(Predicate<Customer> match);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<Parcel> DisplayParcels(Predicate<Parcel> match);
            [MethodImpl(MethodImplOptions.Synchronized)]

            public Parcel ConvertParcel(int id);//returns the parcel of the ID that was given
            [MethodImpl(MethodImplOptions.Synchronized)]
            public Drone ConvertDrone(int id);//returns the drone of the ID that was given
            [MethodImpl(MethodImplOptions.Synchronized)]
            public Station ConvertStation(int id);//returns the station of the ID that was given
            [MethodImpl(MethodImplOptions.Synchronized)]
            public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public double[] powerUse();
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<DroneCharge> displayChargings(int id);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<DroneCharge> displayDronesInCharge(Predicate<DroneCharge> match);
            [MethodImpl(MethodImplOptions.Synchronized)]

            public void deleteDrone(int id);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void deleteCustomer(int id);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void deleteStation(int id);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void deleteParcel(int id);
            [MethodImpl(MethodImplOptions.Synchronized)]

            public void AddUser(int id, string userN, string email, string password, bool isManager);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void deleteUser(int id);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public IEnumerable<User> displayUsers(Predicate<User> match);
            [MethodImpl(MethodImplOptions.Synchronized)]
            public bool userCorrect(string userN, string password, bool isManager);
           
        }

    }

}
