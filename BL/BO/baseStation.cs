using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class baseStation
    {
        public int id { get; set; }
        public string name { get; set; }
        public location location { get; set; }
        public int chargingSlots { get; set; }
        public List<droneInCharging> dronesInCharging { get; set; }
        public override string ToString()
        {
            string ch = "";
            foreach (droneInCharging d in dronesInCharging)
            {
                ch += d.ToString();
            }
            return ("Base Station Id: " + id + "\nBase Station Name: " + name + "\nLocation" + location
                + "\nNumber of Slots:" + chargingSlots+"\nDrones in Charging:" + ch + "\n");
        }

    }
}
