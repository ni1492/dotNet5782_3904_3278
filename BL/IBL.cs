using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    public interface IBL
    {
        #region station functions
        /// <summary>
        ///add station
        /// </summary>
        public void addStation(baseStation station);
        /// <summary>
        ///update station details 
        /// </summary>
        public void updateStation(int id, string name, int chargingSlots);
        /// <summary>
        ///display requested station
        /// </summary>
        public baseStation displayStation(int id);
        /// <summary>
        ///display all stations
        /// </summary>
        public IEnumerable<baseStationForList> displayStationList();
        /// <summary>
        ///display all the stations with available charging slots
        /// </summary>
        public IEnumerable<baseStationForList> displayStationListSlotsAvailable();
        #endregion

        #region drone functions
        /// <summary>
        ///add drone 
        /// </summary>
        public void addDrone(droneForList drone, int stationId);
        /// <summary>
        ///update deone details
        /// </summary>
        public void updateDrone(int id, string model);
        /// <summary>
        ///send drone to charge 
        /// </summary>
        public void sendDroneToCharge(int id);
        /// <summary>
        ///release drone from charging 
        /// </summary>
        public void releaseDroneFromCharge(int id);
        /// <summary>
        ///display requested drone  
        /// </summary>
        public drone displayDrone(int id);
        /// <summary>
        ///display all drones 
        /// </summary>
        public IEnumerable<droneForList> displayDroneList();
        /// <summary>
        ///display all drones 
        /// </summary>
        public IEnumerable<droneForList> displayDrones(Predicate<droneForList> match);
       
        #endregion

        #region customer functions
        /// <summary>
        ///add customer 
        /// </summary>
        public void addCustomer(customer customer);
        /// <summary>
        ///update customer details 
        /// </summary>
        public void updateCustomer(int id, string name, string phone);
        /// <summary>
        ///display requested customer 
        /// </summary>
        public customer displayCustomer(int id);
        /// <summary>
        ///display all customers 
        /// </summary>
        public IEnumerable<customerForList> displayCustomerList();
        #endregion

        #region parcel functions
        /// <summary>
        ///add parcel  
        /// </summary>
        public void addParcel(parcelForList parcel);
        /// <summary>
        ///match the drone to a suitable parcel
        /// </summary>
        public void matchParcelToDrone(int id);
        /// <summary>
        ///parcel is picked up by drone 
        /// </summary>
        public void pickupParcel(int id);
        /// <summary>
        ///parcel is delivered by drone 
        /// </summary>
        public void deliverParcel(int id);
        /// <summary>
        ///display requested parcel 
        /// </summary>
        public parcel displayParcel(int id);
        /// <summary>
        ///display all parcels that match the predicate 
        /// </summary>
        public IEnumerable<parcelForList> displayParcels(Predicate<parcelForList> match);
        /// <summary>
        ///display all parcels 
        /// </summary>
        public IEnumerable<parcelForList> displayParcelList();
        /// <summary>
        ///display all parcels that arent matched 
        /// </summary>
        public IEnumerable<parcelForList> displayParcelListWithoutDrone(); 
        /// <summary>
        ///returen the status of the parcel 
        /// </summary>
        public ParcelStatus getStatus(int id);
        /// <summary>
        ///delete parcel 
        /// </summary>
        public void deleteParcel(int id);
        #endregion

        #region user functions
        /// <summary>
        ///add user  
        /// </summary>
        public void AddUser(int id, string userN, string email, string password, bool isManager);
        /// <summary>
        ///display the user by the userName  
        /// </summary>
        public UserForDisplay displayUser(string userN);
        /// <summary>
        ///display all users  
        /// </summary>
        public IEnumerable<UserForDisplay> displayUsersList();
        /// <summary>
        ///check the password of the user  
        /// </summary>
        public bool userCorrect(string userN, string password, bool isManager);
        /// <summary>
        ///changing the password for the user  
        /// </summary>
        public void changePass(string userN, string password);
        #endregion
     
        #region simulation
        /// <summary>
        ///starting the simukation for the drone  
        /// </summary>
        public void startSimulation(int droneId, Action updateDisplay, Func<bool> stop);
        #endregion
    }
}
