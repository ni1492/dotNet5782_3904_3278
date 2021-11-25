using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        public void addStation(baseStation station);
        public void addDrone(drone drone);
        public void addCustomer(customer customer);
        public void addParcel(parcel parcel);
        public void updateDrone(int id, string model);
        public void updateStation(int id, string name, int chargingSlots);
        public void updateCustomer(int id, string name, int phone);
        public void sendDroneToCharge(int id);
        public void releaseDroneFromCharge(int id, int time);//time???
        public void matchParcelToDrone(int id);
        public void pickupParcel(int id);
        public void deliverParcel(int id);
        public baseStation displayStation(int id);
        public drone displayDrone(int id);
        public customer displayCustomer(int id);
        public parcel displayParcel(int id);
        public IEnumerable<baseStationForList> displayStationList();
        public IEnumerable<droneForList> displayDroneList();
        public IEnumerable<customerForList> displayCustomerList();
        public IEnumerable<parcelForList> displayParcelList();
        public IEnumerable<parcelForList> displayParcelListWithoutDrone();
        public IEnumerable<baseStationForList> displayStationListSlotsAvailable();

    }
}
