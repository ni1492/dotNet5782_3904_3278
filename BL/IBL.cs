using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
//using IDAL.DO;

namespace IBL
{
    public interface IBL
    {
        public void addStation(baseStation station);
        public void updateStation(int id, string name, int chargingSlots);
        public baseStation displayStation(int id);
        public IEnumerable<baseStationForList> displayStationList();
        public IEnumerable<baseStationForList> displayStationListSlotsAvailable();

        public void addDrone(drone drone);
        public void updateDrone(int id, string model);
        public void sendDroneToCharge(int id);
        public void releaseDroneFromCharge(int id, DateTime time);//time???
        public drone displayDrone(int id);
        public IEnumerable<droneForList> displayDroneList();

        public void addCustomer(customer customer);
        public void updateCustomer(int id, string name, int phone);
        public customer displayCustomer(int id);
        public IEnumerable<customerForList> displayCustomerList();

        public void addParcel(parcel parcel);
        public void matchParcelToDrone(int id);
        public void pickupParcel(int id);
        public void deliverParcel(int id);
        public parcel displayParcel(int id);
        public IEnumerable<parcelForList> displayParcelList();
        public IEnumerable<parcelForList> displayParcelListWithoutDrone();


    }
}
