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
        public string phone { get; set; }
        public location location { get; set; }
        public List<parcelAtCustomer> fromCus { get; set; }
        public List<parcelAtCustomer> toCus { get; set; }
        public override string ToString()//custom print function for customer 
        {
            string from="";
            foreach (parcelAtCustomer p in fromCus) 
            {
                from += p.ToString();
            }
            string to = "";
            foreach (parcelAtCustomer p in toCus)
            {
                to += p.ToString();
            }
            return ("Customer Id: " + id + "\nCustomer Name: " + name + "\nPhone Number: " + phone
                + "\nLocation:\n" + location + "\nParcels From Customer:\n" + from + "\nParcels To Customer:\n" + to + "\n");
        }
    }
}
