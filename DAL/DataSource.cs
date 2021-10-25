using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//קווי הרוחב 33º-29º 

namespace DALObject
{
    public class DataSource
    {
        internal static Random R = new Random();
        //רשימות לאחסון המידע
        internal static List<IDAL.DO.Drone> drones = new List<IDAL.DO.Drone>();
        internal static List<IDAL.DO.Station> baseStations = new List<IDAL.DO.Station>();
        internal static List<IDAL.DO.Customer> customers = new List<IDAL.DO.Customer>();
        internal static List<IDAL.DO.Parcel> parcels = new List<IDAL.DO.Parcel>();
        internal static List<IDAL.DO.DroneCharge> inChargeing = new List<IDAL.DO.DroneCharge>();

        internal class Config//מספרים רצים לאיתחול
        {
            internal static int StationID = 1;
            internal static int DroneID = 1000;
            internal static int CustomerID = 100000000;
            internal static int ParcelID = 1000;
        }
        public static void Initialize()//איתחול ערכים בתחילת התוכנית
        {
            int num = R.Next(2, 5);
            for (int i = 0; i < num; i++)//איתחול תחנות
            {
                DALObject.AddStation(Config.StationID++, R.Next(1000, 10000), R.Next(-50, 0)+R.NextDouble(), R.Next(0, 50) + R.NextDouble(), R.Next(50));
            }//איתחול תחנות
            num = R.Next(5, 10);
            for (int i = 0; i < num; i++)//איתחול רחפנים
            {
                string model = ((IDAL.DO.Models)R.Next(13)).ToString();//מודל רנדומלי מסוגי המודלים שבאינאם
                IDAL.DO.WeightCategories weight = ((IDAL.DO.WeightCategories)R.Next(3));//משקל רנדומלי
                IDAL.DO.DroneStatuses statuse = ((IDAL.DO.DroneStatuses)R.Next(3));//מצב רנדומלי
                DALObject.AddDrone(Config.DroneID++, model, weight, statuse, R.Next(0, 100) + R.NextDouble());
            }//איתחול רחפנים
            num = R.Next(10, 15);
            string[] names = { "hanna", "lenny", "ginny", "minnie", "bob", "benny", "yakob", "shuva", "etya", "hamutal",
                "nelly", "hellen", "braidy", "daisy", "anastasia", "kevin" };//שמות לאיתחול הלקוחות
            for (int i = 0; i < num; i++)//איתחול לקוחות
            {
                DALObject.AddCustomer(Config.CustomerID++, names[i], ("0" + R.Next(100000000, 1000000000).ToString()), R.Next(-50, 0) + R.NextDouble(), R.Next(0, 50) + R.NextDouble());
            }//איתחול לקוחות
            num = R.Next(10, 15);
            for (int i = 0; i < num; i++)//איתחול חבילות
            {
                int dId = 0;
                IDAL.DO.WeightCategories weight = ((IDAL.DO.WeightCategories)R.Next(3));//משקל חבילה רנדומלי
                IDAL.DO.Priorities priority = ((IDAL.DO.Priorities)R.Next(3));//דחיפות רנדומלית
                DateTime req = new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//זמן בקשה רנדומלי
                DateTime sch= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//זמן משוער רנדומלי
                DateTime pUp= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//זמן איסוף רנדומלי
                DateTime del= new DateTime(R.Next(1, 9999), R.Next(1, 12), R.Next(1, 25), R.Next(1, 23), R.Next(1, 59), R.Next(1, 59));//זמן הגעה רנדומלי
                int sId = R.Next(100000000, 1000000000);//שולח רנדומלי
                int tId = R.Next(100000000, 1000000000);//יעד רנדומלי
                DALObject.AddParcel(Config.ParcelID++, sId, tId, weight, priority, dId, req, sch, pUp, del);
                DALObject.Match(DALObject.ConvertParcel((Config.ParcelID - 1)));//שייוך רחפן מתאים לחבילה
            }//איתחול חבילות
        }
    }
}
