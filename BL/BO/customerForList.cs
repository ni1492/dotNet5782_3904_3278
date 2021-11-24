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
        public int phone { get; set; }
        public int deliveredPar { get; set; }
        public int notDeliveredPar { get; set; }
        public int acceptedPar { get; set; }
        public int notAcceptedPar { get; set; }
    }
}
