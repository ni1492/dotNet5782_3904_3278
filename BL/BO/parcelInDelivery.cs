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
        public double distance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = calcDistance();
            }
        }//needs a function to calculate the distance 
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + id + "\nWeight of the parcel: " + weight + "\nPriority: " + priority
                + "\nDrone Status: " + status + "\nSender: " + sender + "\nReceiver: " + receiver + "\nCreation Time: " +
                "\nPickedUp Location:" + pickUp + "\nDestination Location: " + destination + "\nDistance:" + distance + "\n");
        }
        private double calcDistance()//calculate thedistance between two locations 
        {
            int R = 6371 * 1000;
            double phi1 = pickUp.Latitude * Math.PI;
            double phi2 = destination.Latitude * Math.PI;
            double deltaPhi = (destination.Latitude - pickUp.Latitude) * Math.PI / 180;
            double deltaLambda = (destination.Longitude - pickUp.Longitude) * Math.PI / 180;

            double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                Math.Cos(phi1) * Math.Cos(phi2) *
                Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (R * c / 1000);

        }
    }
}
