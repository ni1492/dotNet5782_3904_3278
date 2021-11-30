using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class parcelInDelivery
    {
        public int id { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public bool status { get; set; }
        public customerForParcel sender { get; set; }
        public customerForParcel receiver { get; set; }
        public location pickUp { get; set; }
        public location destination { get; set; }
        public double distance { get; set; }//needs a function to calculate the distance 
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + id + "\nWeight of the parcel: " + weight + "\nPriority: " + priority
                + "\nDrone Status: " + status + "\nSender: " + sender + "\nReceiver: " + receiver + "\nCreation Time: " +
                "\nPickedUp Location:" + pickUp + "\nDestination Location: " + destination + "\nDistance:" + distance + "\n");
        }
    }
}
