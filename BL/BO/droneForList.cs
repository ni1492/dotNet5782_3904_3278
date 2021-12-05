using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class droneForList
    {
        public int id { get; set; }
        public string model { get; set; }
        public WeightCategories weight { get; set; }
        public double battery { get; set; }
        public DroneStatuses status { get; set; }
        public location currentLocation { get; set; }
        public int parcelID { get; set; }
        public override string ToString()//custom print function for drone 
        {
            return ("Drone Id: " + id + "\nDrone Model: " + model + "\nWeight: " + weight
            + "\nDrone Battery:" + battery + "\nDrone status: " + status 
            + "\nCurrent Location:\n" + currentLocation + "\nParcel Id:"+parcelID+ "\n");
        }
    }
}
