using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
namespace IBL
{
    public partial class BL : IBL
    {
        public void addCustomer(customer customer)
        {
            try
            {
                dl.AddCustomer(customer.id,customer.name,customer.phone,customer.location.Longitude,customer.location.Latitude);
            }
            catch
            {
                //exception - exists
            }

        }
        public void updateCustomer(int id, string name, int phone)
        {

        }
        public customer displayCustomer(int id)
        {

        }
        public IEnumerable<customerForList> displayCustomerList()
        {

        }
    }
}
