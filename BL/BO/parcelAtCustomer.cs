using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class parcelAtCustomer
    {
        public int id { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public DroneStatuses status { get; set; }
        public customerForParcel otherCus { get; set; }
    }
}
