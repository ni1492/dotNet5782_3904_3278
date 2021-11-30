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
        public void addParcel(parcelInDelivery parcel)
        {
            DateTime x = new DateTime(0, 0, 0, 0, 0, 0);
            try
            {
                dl.AddParcel(0, parcel.sender.id, parcel.receiver.id, (IDAL.DO.WeightCategories)parcel.weight, (IDAL.DO.Priorities)parcel.priority, 0, DateTime.Now, x, x, x);
            }
            catch
            {

            }
        }
        public void pickupParcel(int id)
        {

        }
        public void deliverParcel(int id)
        {

        }
        public parcel displayParcel(int id)
        {

        }
        public IEnumerable<parcelForList> displayParcelList()
        {

        }
        public IEnumerable<parcelForList> displayParcelListWithoutDrone()
        {

        }
    }
}
