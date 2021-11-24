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
        public DroneStatuses status { get; set; }
        public customerForParcel sender { get; set; }
        public customerForParcel receiver { get; set; }
        public location pickUp { get; set; }
        public location destination { get; set; }
        public double distance { get; set; }//needs a function to calculate the distance 
    }
}
