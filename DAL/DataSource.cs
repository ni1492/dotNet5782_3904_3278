using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//קווי הרוחב 33º-29º 

namespace DALObject
{ }
public class DataSource
{
    internal static Random R = new Random();
    internal static List<IDAL.DO.Drone> drones = null;
    internal static List<IDAL.DO.Station> baseStations = null;
    internal static List<IDAL.DO.Customer> customers = null;
    internal static List<IDAL.DO.Parcel> parcels = null;
    internal static List<IDAL.DO.DroneCharge> inChargeing = null;

    internal class Config
    {
        internal static int StationID = 1;
        internal static int DroneID = 1000;
        internal static int CustomerID = 100000000;
        internal static int ParcelID = 1000;
    }
    internal static void Initialize()
    {
        int num = R.Next(2, 5);
        for (int i = 0; i < num; i++)
        {
            DALObject.DALObject.AddStation(Config.StationID++, R.Next(1000, 10000), R.Next(1000, 10000) / 100, R.Next(1000, 10000) / 100, R.Next(50));
        }
        num = R.Next(5, 10);
        for (int i = 0; i < num; i++)
        {
            string model = ((IDAL.DO.Models)R.Next(13)).ToString();
            IDAL.DO.WeightCategories weight = ((IDAL.DO.WeightCategories)R.Next(3));
            IDAL.DO.DroneStatuses statuse = ((IDAL.DO.DroneStatuses)R.Next(3));
            DALObject.DALObject.AddDrone(Config.DroneID++, model, weight, statuse, R.Next(1000, 10000) / 100);
        }
        num = R.Next(10, 15);
        string[] names = { "hanna", "lenny", "ginny", "minnie", "bob", "benny", "yakob", "shuva", "etya", "hamutal","nelly","hellen","braidy","daisy","anastasia","kevin" };
        for (int i = 0; i < num; i++)
        {
            DALObject.DALObject.AddCustomer(Config.CustomerID++, names[i], ("0" + R.Next(100000000, 1000000000).ToString()), R.Next(1000, 10000) / 100, R.Next(1000, 10000) / 100);
        }
        for (int i = 0; i < num; i++)
        {
            IDAL.DO.WeightCategories weight = ((IDAL.DO.WeightCategories)R.Next(3));
            IDAL.DO.Priorities priority = ((IDAL.DO.Priorities)R.Next(3));
         //  DALObject.DALObject.AddParcel(Config.ParcelID,,,weight,priority,)
        }
    }
}
}
