using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class parcelAtCustomer
    {
        public int id { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatus status { get; set; }
        public customerForParcel otherCus { get; set; }
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + id + "\nWeight of the parcel: " + weight +
                "\nPriority: " + priority + "\nDrone Status: " + status + "\nSender/receiver: " + otherCus + "\n");
        }
    }
}
