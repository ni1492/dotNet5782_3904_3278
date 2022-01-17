using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;


namespace BlApi
{
    public interface IBL
    {
        #region station functions
        /// <summary>
        ///add station
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addStation(baseStation station);
        /// <summary>
        ///update station details 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateStation(int id, string name, int chargingSlots);
        /// <summary>
        ///display requested station
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public baseStation displayStation(int id);
        /// <summary>
        ///display all stations
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<baseStationForList> displayStationList();
        /// <summary>
        ///display all the stations with available charging slots
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<baseStationForList> displayStationListSlotsAvailable();
        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>
        ///display all drones in the station 
        /// </summary>
        public IEnumerable<droneInCharging> displayDronesInCharge(int id);
        #endregion

        #region drone functions
        /// <summary>
        ///add drone 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addDrone(droneForList drone, int stationId);
        /// <summary>
        ///update deone details
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateDrone(int id, string model);
        /// <summary>
        ///send drone to charge 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void sendDroneToCharge(int id);
        /// <summary>
        ///release drone from charging 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void releaseDroneFromCharge(int id);
        /// <summary>
        ///display requested drone  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public drone displayDrone(int id);
        /// <summary>
        ///display all drones 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<droneForList> displayDroneList();
        /// <summary>
        ///display all drones 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<droneForList> displayDrones(Predicate<droneForList> match);
 
     

        #endregion

        #region customer functions
        /// <summary>
        ///add customer 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addCustomer(customer customer);
        /// <summary>
        ///update customer details 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void updateCustomer(int id, string name, string phone);
        /// <summary>
        ///display requested customer 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public customer displayCustomer(int id);
        /// <summary>
        ///display all customers 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<customerForList> displayCustomerList();
        #endregion

        #region parcel functions
        /// <summary>
        ///add parcel  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addParcel(parcelForList parcel);
        /// <summary>
        ///match the drone to a suitable parcel
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void matchParcelToDrone(int id);
        /// <summary>
        ///parcel is picked up by drone 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void pickupParcel(int id);
        /// <summary>
        ///parcel is delivered by drone 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deliverParcel(int id);
        /// <summary>
        ///display requested parcel 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public parcel displayParcel(int id);
        /// <summary>
        ///display all parcels that match the predicate 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelForList> displayParcels(Predicate<parcelForList> match);
        /// <summary>
        ///display all parcels 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelForList> displayParcelList();
        /// <summary>
        ///display all parcels that arent matched 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<parcelForList> displayParcelListWithoutDrone();
        /// <summary>
        ///returen the status of the parcel 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public ParcelStatus getStatus(int id);
        /// <summary>
        ///delete parcel 
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void deleteParcel(int id);
        #endregion

        #region user functions
        /// <summary>
        ///add user  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddUser(int id, string userN, string email, string password, bool isManager);
        /// <summary>
        ///display the user by the userName  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public UserForDisplay displayUser(string userN);
        /// <summary>
        ///display all users  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<UserForDisplay> displayUsersList();
        /// <summary>
        ///check the password of the user  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool userCorrect(string userN, string password, bool isManager);
        /// <summary>
        ///changing the password for the user  
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
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
