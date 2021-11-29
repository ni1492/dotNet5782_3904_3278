using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DALObject
{
    internal class DataSource
    {
        internal static Random R = new Random();
        //lists of stored information: drones, stations, customers, parcels, inCharging.
        internal static List<Drone> drones = new();
        internal static List<Station> baseStations = new();
        internal static List<Customer> customers = new();
        internal static List<Parcel> parcels = new();
        internal static List<DroneCharge> inChargeing = new();

        internal class Config//initialization of running numbers for station list, drone list, customer list, and parcel list
        {
            internal static int StationID = 1;
            internal static int DroneID = 1000;
            internal static int CustomerID = 100000000;
            internal static int ParcelID = 1000;
            internal static double availablePK;
            internal static double lightPK;
            internal static double mediumPK;
            internal static double heavyPK;
            internal static double chargingPH;
        }
        public static void Initialize()//initialization of data for the program
        {
            Config.availablePK = R.Next(0, 100) + R.NextDouble();
            Config.lightPK = R.Next(0, 100) + R.NextDouble();
            Config.mediumPK = R.Next(0, 100) + R.NextDouble();
            Config.heavyPK = R.Next(0, 100) + R.NextDouble();
            Config.chargingPH = R.Next(0, 100) + R.NextDouble();
            int num = R.Next(2, 5); 
            for (int i = 0; i < num; i++)//initialization of 2-4 stations
            {
                Station station = new Station();
                station.Id = Config.StationID++;
                station.Name = R.Next(1000, 10000);
                station.Longitude = R.Next(-180, 180) + (double)(R.Next(1000, 10000)) / 10000;
                station.Lattitude = R.Next(-90, 90) + (double)(R.Next(1000, 10000)) / 10000;
                station.ChargeSlots = R.Next(50);
                baseStations.Add(station);
            }
            num = R.Next(5, 10);
            for (int i = 0; i < num; i++)//initialization of 5-9 drones
            {
                Drone drone = new Drone();
                drone.Id = Config.DroneID++;
                drone.Model=((IDAL.DO.Models)R.Next(13)).ToString();//initialization of random model name using enum
                drone.MaxWeight = ((IDAL.DO.WeightCategories)R.Next(3));//initialization of random weight using enum
              // drone.Status = ((IDAL.DO.DroneStatuses)R.Next(3));//initialization of random status using enum
               // drone.Battery = R.Next(0, 100) + R.NextDouble();
                drones.Add(drone);
            }
            num = R.Next(10, 15);
            string[] names = { "hanna", "lenny", "ginny", "minnie", "bob", "benny", "yakob", "shuva", "etya", "hamutal",
                "nelly", "hellen", "braidy", "daisy", "anastasia", "kevin" };//potential names for initialization of customers
            for (int i = 0; i < num; i++)//initialization of 10-14 customers
            {
                Customer customer = new Customer();
                customer.Id = Config.CustomerID++;
                customer.Name = names[i];
                customer.Phone = ("0" + R.Next(100000000, 1000000000).ToString());
                customer.Longitude = R.Next(-180, 180) + (double)(R.Next(1000, 10000)) / 10000;
                customer.Lattitude = R.Next(-90, 90) + (double)(R.Next(1000, 10000)) / 10000;
                customers.Add(customer);
            }
            num = R.Next(10, 15);
            for (int i = 0; i < num; i++)//initialization of 10-14 parcels
            {
                Parcel parcel = new Parcel();
                parcel.Id = Config.ParcelID++;
                parcel.SenderId = R.Next(100000000, 1000000000);//initialization of random sender id
                parcel.TargetId = R.Next(100000000, 1000000000);//initialization of random target id

               parcel.Weight = ((IDAL.DO.WeightCategories)R.Next(3));//initialization of random weight using enum
               parcel.Priority = ((IDAL.DO.Priorities)R.Next(3));//initialization of random priority using enum
                parcel.Requested = new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//initialization of random request time
                parcel.Scheduled= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//initialization of random schedule time
                parcel.PickedUp= new DateTime(0,0,0,0,0,0);//initialization of random pick up time
                parcel.Delivered= new DateTime(0,0,0,0,0,0);//initialization of random delivery time
                parcel.DroneId = 0;
                foreach (Drone drone in DataSource.drones)//goes over the list of drones and finds the first one that matches the standards of the given parcel
                {
                    if (drone.MaxWeight >= parcel.Weight)//makes sure the maximum weight of the drone can hold the parcel
                    {
                        parcel.DroneId = drone.Id;
                        break;
                    }
                }
                parcels.Add(parcel);
            }
        }
    }
}
