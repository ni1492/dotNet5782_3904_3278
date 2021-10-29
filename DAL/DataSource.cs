using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALObject
{
    public class DataSource
    {
        internal static Random R = new Random();
        //lists of stored information: drones, stations, customers, parcels, inCharging.
        internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>();
        internal static List<IDAL.DO.Station> baseStations = new List<IDAL.DO.Station>();
        internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
        internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
        internal static List<IDAL.DO.DroneCharge> inChargeing = new List<IDAL.DO.DroneCharge>();

        internal class Config//initialization of running numbers for station list, drone list, customer list, and parcel list
        {
            internal static int StationID = 1;
            internal static int DroneID = 1000;
            internal static int CustomerID = 100000000;
            internal static int ParcelID = 1000;
        }
        public static void Initialize()//initialization of data for the program
        {
            int num = R.Next(2, 5); 
            for (int i = 0; i < num; i++)//initialization of 2-4 stations
            {
                //
               // DALObject.AddStation(Config.StationID++, R.Next(1000, 10000), R.Next(-50, 0)+R.NextDouble(), R.Next(0, 50) + R.NextDouble(), R.Next(50));
                DALObject.AddStation(Config.StationID++, R.Next(1000, 10000), R.Next(-180, 180) + (double)(R.Next(1000,10000))/10000, R.Next(-90, 90) + (double)(R.Next(1000, 10000)) / 10000, R.Next(50));
            }
            num = R.Next(5, 10);
            for (int i = 0; i < num; i++)//initialization of 5-9 drones
            {
                string model = ((IDAL.DO.Models)R.Next(13)).ToString();//initialization of random model name using enum
                IDAL.DO.WeightCategories weight = ((IDAL.DO.WeightCategories)R.Next(3));//initialization of random weight using enum
                IDAL.DO.DroneStatuses statuse = ((IDAL.DO.DroneStatuses)R.Next(3));//initialization of random status using enum
                DALObject.AddDrone(Config.DroneID++, model, weight, statuse, R.Next(0, 100) + R.NextDouble());//adds the new drone to the list of drones
            }
            num = R.Next(10, 15);
            string[] names = { "hanna", "lenny", "ginny", "minnie", "bob", "benny", "yakob", "shuva", "etya", "hamutal",
                "nelly", "hellen", "braidy", "daisy", "anastasia", "kevin" };//potential names for initialization of customers
            for (int i = 0; i < num; i++)//initialization of 10-14 customers
            {
                //
                //DALObject.AddCustomer(Config.CustomerID++, names[i], ("0" + R.Next(100000000, 1000000000).ToString()), R.Next(-50, 0) + R.NextDouble(), R.Next(0, 50) + R.NextDouble());
                DALObject.AddCustomer(Config.CustomerID++, names[i], ("0" + R.Next(100000000, 1000000000).ToString()), R.Next(-180, 180) + (double)(R.Next(1000, 10000)) / 10000, R.Next(-90, 90) + (double)(R.Next(1000, 10000)) / 10000);
            }
            num = R.Next(10, 15);
            for (int i = 0; i < num; i++)//initialization of 10-14 parcels
            {
                int dId = 0;
                IDAL.DO.WeightCategories weight = ((IDAL.DO.WeightCategories)R.Next(3));//initialization of random weight using enum
                IDAL.DO.Priorities priority = ((IDAL.DO.Priorities)R.Next(3));//initialization of random priority using enum
                DateTime req = new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//initialization of random request time
                DateTime sch= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//initialization of random schedule time
                DateTime pUp= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//initialization of random pick up time
                DateTime del= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//initialization of random delivery time
                int sId = R.Next(100000000, 1000000000);//initialization of random sender id
                int tId = R.Next(100000000, 1000000000);//initialization of random target id
                DALObject.AddParcel(Config.ParcelID++, sId, tId, weight, priority, dId, req, sch, pUp, del);//adds the new parcels to the list of parcels
                DALObject.Match(DALObject.ConvertParcel((Config.ParcelID - 1)));//assign the parcel to a drone
            }
        }
    }
}
