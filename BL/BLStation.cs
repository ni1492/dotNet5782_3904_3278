using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using DO;

namespace BlApi
{
    public partial class BL : IBL
    {
        public void addStation(baseStation station)
        {
            station.dronesInCharging = null; //the list of drones in charging is started with NULL
            try
            {
                dl.AddStation(station.id,station.name , station.location.Longitude, station.location.Latitude, station.chargingSlots);
            }
            catch (Exception ex) //catches if the ID already exists
            {
                throw new BO.exceptions.ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public void updateStation(int id, string name, int chargingSlots) //update station details
        {
            try
            {
                DO.Station tempDL = dl.DisplayStations(station => station.Id == id).First(); //finds the station in the DAL layer
               
                if (name != null) //if the function recieves the name to update  - it changes the name
                    tempDL.Name = name;
                if (chargingSlots != 0) //if the function recieves the number of charging slots to update  -it changes it
                {
                    if (chargingSlots >= dl.displayChargings(id).Count())
                        tempDL.ChargeSlots = chargingSlots - dl.displayChargings(id).Count();
                    else
                        throw new BO.exceptions.SlotsException("not enoght slots for the station");
                }
                dl.deleteStation(id); //deletes the station before the changes 
                dl.AddStation(tempDL.Id, tempDL.Name, tempDL.Longitude, tempDL.Lattitude, tempDL.ChargeSlots);
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
            
        }
        public baseStation displayStation(int id) //display the base station requested
        {
            try
            {
                DO.Station stationDO = dl.DisplayStations(station=>station.Id==id).First();
                baseStation stationBO = (new baseStation()
                {
                    id = stationDO.Id,
                    name = stationDO.Name,
                    location = new location
                    {
                        Latitude = stationDO.Lattitude,
                        Longitude = stationDO.Longitude
                    },
                    chargingSlots = stationDO.ChargeSlots,
                    dronesInCharging = new List<droneInCharging>()
                });
                foreach (var item in dl.displayChargings(id))
                {
                    stationBO.dronesInCharging.Add(new droneInCharging
                    {
                        id = item.DroneId,
                        battery = getBattery(item.DroneId)
                    });
                }
                return stationBO;
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
           
        }

        private double getBattery(int droneId)
        {
            return drones.Find(drone => drone.id == droneId).battery;
        }

        public IEnumerable<baseStationForList> displayStationList()//displays the list of stations
        {
            foreach (var item in dl.DisplayStations(station => true))
            {
                yield return (new baseStationForList
                {
                    id = item.Id,
                    name=item.Name,
                    availableSlots=item.ChargeSlots,
                    usedSlots= dl.displayChargings(item.Id).Count() 

                });          
            }
        }

        public IEnumerable<baseStationForList> displayStationListSlotsAvailable()//displays the list of stations with available slots
        {
            foreach (var item in dl.DisplayStations(station=>station.ChargeSlots!=0))
            {
                yield return (new baseStationForList
                {
                    id = item.Id,
                    name = item.Name,
                    availableSlots = item.ChargeSlots,
                    usedSlots = dl.displayChargings(item.Id).Count()

                });
            }
        }
    }
}
