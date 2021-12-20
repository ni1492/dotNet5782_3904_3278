using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
//using IDAL.DO;

namespace BlApi
{
    public interface IBL
    {
        public void addStation(baseStation station); //add station
        public void updateStation(int id, string name, int chargingSlots); //update station details 
        public baseStation displayStation(int id); //display requested station
        public IEnumerable<baseStationForList> displayStationList(); //display all stations
        public IEnumerable<baseStationForList> displayStationListSlotsAvailable(); //display all the stations with available charging slots

        public void addDrone(droneForList drone, int stationId); //add drone 
        public void updateDrone(int id, string model); //update deone details
        public void sendDroneToCharge(int id); //send drone to charge 
        public void releaseDroneFromCharge(int id); //release drone from charging
        public drone displayDrone(int id); //display requested drone 
        public IEnumerable<droneForList> displayDroneList(); //display all drones
        public IEnumerable<droneForList> displayDrones(Predicate<droneForList> match); //display all drones

        public void addCustomer(customer customer); //add customer
        public void updateCustomer(int id, string name, string phone); //update customer details
        public customer displayCustomer(int id); //display requested customer
        public IEnumerable<customerForList> displayCustomerList(); //display all customers

        public void addParcel(parcelInDelivery parcel); //add parcel 
        public void matchParcelToDrone(int id); //match function
        public void pickupParcel(int id); //parcel is picked up by drone
        public void deliverParcel(int id); //parcel is delivered by drone
        public parcel displayParcel(int id); //display requested parcel
        public IEnumerable<parcelForList> displayParcelList(); //display all parcels
        public IEnumerable<parcelForList> displayParcelListWithoutDrone(); //display all parcels that arent matched
        public ParcelStatus getStatus(int id);

    }
}
