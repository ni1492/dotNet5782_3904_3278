using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class customer 
    {
        public int id { get; set; }
        public string name { get; set; }
        public int phone { get; set; }
        public location location { get; set; }
        public List<parcelAtCustomer> fromCus { get; set; }
        public List<parcelAtCustomer> toCus { get; set; }
    }
}
