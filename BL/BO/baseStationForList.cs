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
        public string name { get; set; }
        public int availableSlots { get; set; }
        public int usedSlots { get; set; }

    }
}
