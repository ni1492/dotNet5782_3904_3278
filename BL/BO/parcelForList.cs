using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class parcelForList
    {
        public int id { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public ParcelStatus status { get; set; }
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + id + "\nSender name: " + sender + "\nReceiver name: " + receiver + "\nWeight of the parcel: " + weight +
                "\nPriority: " + priority + "\nParcel Status: " + status + "\n");
        }
    }
}
