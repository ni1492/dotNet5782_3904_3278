using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class parcel
    {
        public int id { get; set; }
        public customerForParcel sender { get; set; }
        public customerForParcel receiver { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public droneForParcel drone { get; set; }
        public DateTime? creation { get; set; }
      
        public DateTime? match { get; set; }
      
        public DateTime? pickup { get; set; }
      
        public DateTime? delivery { get; set; }
       
        public override string ToString()//custom print function for parcel 
        {
            return ("Parcel Id: " + id + "\nSender: " + sender + "\nReceiver: " + receiver + "\nWeight of the parcel: " + weight +
                "\nPriority: " + priority + "\nDrone: " + drone + "\nCreation Time: " + creation + "\nMatch Time: " + match +
                "\nPickedUp Time:" + pickup + "\nDelivered Time: " + delivery + "\n");
        }
    }
}
