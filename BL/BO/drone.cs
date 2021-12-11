using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class drone
    {
        public int id { get; set; }
        public string model { get; set; }
        public WeightCategories weight { get; set; }
        public double battery { get; set; }
        public DroneStatuses status { get; set; }
        public parcelInDelivery parcel { get; set; }
        public location currentLocation { get; set; }
        public override string ToString()//custom print function for drone 
        {
            return ("Drone Id: " + id + "\nDrone Model: " + model + "\nWeight: " + weight
            + "\nDrone Battery:" + battery + "\nDrone status: " + status + "\nParcel:" + parcel 
            + "\nCurrent Location:" + currentLocation + "\n");
        }
    }
}
