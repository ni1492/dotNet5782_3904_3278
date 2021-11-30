using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class baseStationForList
    {
        public int id { get; set; }
        public int name { get; set; }
        public int availableSlots { get; set; }
        public int usedSlots { get; set; }
        public override string ToString()
        {
            return ("Base Station Id: " + id + "\nBase Station Name: " + name + "\nNumber of Available Slots" + availableSlots
                + "\nNumber of Slots In Usage:" + usedSlots + "\n");
        }

    }
}
