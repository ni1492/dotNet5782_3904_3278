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
        internal static List<DroneCharge> inCharging = new();

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
            Config.availablePK = R.Next(0, 30) + R.NextDouble();
            Config.lightPK = R.Next(0, 30) + R.NextDouble();
            Config.mediumPK = R.Next(0, 30) + R.NextDouble();
            Config.heavyPK = R.Next(0, 30) + R.NextDouble();
            Config.chargingPH = R.Next(30, 90) + R.NextDouble();
            int num = R.Next(2, 5);
            for (int i = 0; i < num; i++)//initialization of 2-4 stations
            {
                Station station = new Station();
                station.Id = Config.StationID++;
                station.Name = "station " + (i+1);
                station.Longitude = R.Next(30, 33) + (double)(R.Next(1000, 10000)) / 10000;
                station.Lattitude = R.Next(-30, -27) + (double)(R.Next(1000, 10000)) / 10000;
                station.ChargeSlots = R.Next(50);
                baseStations.Add(station);
            }
            num = R.Next(5, 10);
            for (int i = 0; i < num; i++)//initialization of 5-9 drones
            {
                Drone drone = new Drone();
                drone.Id = Config.DroneID++;
                drone.Model=((IDAL.DO.Models)R.Next(13)).ToString();//initialization of random model name using enum
                drone.MaxWeight = ((IDAL.DO.WeightCategories)R.Next(1,4));//initialization of random weight using enum
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
                customer.Longitude = R.Next(30, 33) + (double)(R.Next(1000, 10000)) / 10000;
                customer.Lattitude = R.Next(-30, -27) + (double)(R.Next(1000, 10000)) / 10000;
                customers.Add(customer);
            }
            num = R.Next(10, 15);
            for (int i = 0; i < num; i++)//initialization of 10-14 parcels
            {
                Parcel parcel = new Parcel();
                parcel.Id = Config.ParcelID++;
                parcel.SenderId = R.Next(100000000, Config.CustomerID);//initialization of random sender id
                parcel.TargetId = R.Next(100000000, Config.CustomerID);//initialization of random target id
                if (parcel.SenderId == parcel.TargetId)
                    parcel.SenderId++;
               parcel.Weight = ((IDAL.DO.WeightCategories)R.Next(1,4));//initialization of random weight using enum
               parcel.Priority = ((IDAL.DO.Priorities)R.Next(1,4));//initialization of random priority using enum
                parcel.Delivered = new DateTime(R.Next(4, DateTime.Now.Year), R.Next(1, DateTime.Now.Month - 1), R.Next(1, 29), R.Next(0, 24), R.Next(0, 60), R.Next(0, 60));//initialization of random delivery time
                parcel.PickedUp = new DateTime(R.Next(3, parcel.Delivered.Year), R.Next(1, 13), R.Next(1, 29), R.Next(0, 24), R.Next(0, 60), R.Next(0, 60));//initialization of random pick up time
                parcel.Scheduled = new DateTime(R.Next(2, parcel.PickedUp.Year), parcel.Requested.Year, R.Next(1, 29), R.Next(0, 24), R.Next(0, 60), R.Next(0, 60));//initialization of random schedule time
                parcel.Requested =new DateTime(R.Next(1, parcel.Scheduled.Year),R.Next(1,13),R.Next(1,29),R.Next(0,24),R.Next(0,60),R.Next(0,60));//initialization of random request time
                parcel.DroneId = 0;
                bool deliver = false;
               //need to make sure it make sence
                foreach (Drone drone in DataSource.drones)//goes over the list of drones and finds the first one that matches the standards of the given parcel
                {
                    bool matched = false;
                    foreach (Parcel parcel1 in DataSource.parcels)
                    {
                        if (parcel1.DroneId == drone.Id && parcel1.Delivered < DateTime.Now) 
                        {
                            matched = true;
                            break;
                        }
                    }
                    if ((drone.MaxWeight >= parcel.Weight) && (!matched))//makes sure the maximum weight of the drone can hold the parcel
                    {
                        parcel.DroneId = drone.Id;
                        parcel.Delivered = DateTime.MinValue;
                        if (R.Next(0, 2) == 0)
                            parcel.PickedUp = DateTime.MinValue;
                        break;
                    }
                    else if ((drone.MaxWeight >= parcel.Weight)&& R.Next(0, 2) == 0)
                    {
                        parcel.DroneId = 0;
                        deliver = true;
                        break;
                    }
                }
                if(!deliver&&parcel.DroneId==0)
                {
                    parcel.Delivered = DateTime.MinValue;
                    parcel.PickedUp = DateTime.MinValue;
                    parcel.Scheduled = DateTime.MinValue;
                }
                parcels.Add(parcel);
            }
        }
    }
}
