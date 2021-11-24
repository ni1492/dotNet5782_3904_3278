using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class baseStation
    {
        public int id { get; set; }
        public string name { get; set; }
        public location location { get; set; }
        public int chargingSlots { get; set; }
        public List<droneInCharging> dronesInCharging { get; set; }

    }
}
