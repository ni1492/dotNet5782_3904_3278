using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class customerForParcel
    {
        public int id { get; set; }
        public string name { get; set; }
        public override string ToString()
        {
            return ("Customer Id: " + id + "\nCustomer Name: " + name + "\n");
        }
    }
}
