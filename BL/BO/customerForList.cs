using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class customerForList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public int deliveredPar { get; set; }
        public int notDeliveredPar { get; set; }
        public int acceptedPar { get; set; }
        public int notAcceptedPar { get; set; }
        public override string ToString()
        {
            return ("Customer Id: " + id + "\nCustomer Name: " + name + "\nPhone Number: " + phone
                + "\nNumber of Parcels Sent and Delivered: " + deliveredPar + "\nNumber of Parcels Sent and  NOT Delivered: "
                + notDeliveredPar + "\nNumber of Parcels Accepted: " + acceptedPar + "\nNumber of Parcels Sent and NOT Accepted: "
                + notAcceptedPar + "\n");
        }
    }
}
