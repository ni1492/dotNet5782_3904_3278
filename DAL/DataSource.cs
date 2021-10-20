using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALObject
{
    public class DataSource
    {
        internal static List<IDAL.DO.Drone> drones = null;
        internal static List<IDAL.DO.Station> baseStations = null;
        internal static List<IDAL.DO.Customer> customers = null;
        internal static List<IDAL.DO.Parcel> parcels = null;
        internal static List<IDAL.DO.DroneCharge> inChargeing = null;

        internal class Config
        {
            internal static int IdNumber = 1;
        }
        internal static void Initialize()
        {
            DALObject.AddStation(Config.IdNumber,0,0.0, 0.0, 0);
        }
    }
}
