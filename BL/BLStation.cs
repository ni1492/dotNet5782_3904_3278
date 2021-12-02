using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using IDAL.DO;

namespace IBL
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
                throw new ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public void updateStation(int id, string name, int chargingSlots) //update station details
        {
            IDAL.DO.Station tempDL = dl.PrintStation(id); //catch  //finds the station in the DAL layer
            dl.deleteStation(id);  //catch //deletes the station before the changes 
            if (name != null) //if the function recieves the name to update  - it changes the name
                tempDL.Name = name;
            if(chargingSlots!=0) //if the function recieves the number of charging slots to update  -it changes it
            {
                if(chargingSlots> dl.displayChargings(id).Count())
                    tempDL.ChargeSlots = chargingSlots - dl.displayChargings(id).Count();
               //else- throw- not enoght slots //catch for display
            }
            dl.AddStation(tempDL.Id, tempDL.Name, tempDL.Longitude, tempDL.Lattitude, tempDL.ChargeSlots); //catch
        }
        public baseStation displayStation(int id) //display the base station requested
        {
            IDAL.DO.Station stationDO = dl.PrintStation(id);
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
            foreach (var item in dl.displayChargings(id)) //catch
            {
                stationBO.dronesInCharging.Add(new droneInCharging
                {
                    id = item.DroneId,
                    battery = getBattery(item.DroneId) //catch
                });
            }
            return stationBO;
        }

        private double getBattery(int droneId)
        {
            return drones.Find(drone => drone.id == droneId).battery;
            //throw - if the Drone doesnt exist
        }

        public IEnumerable<baseStationForList> displayStationList()//displays the list of stations
        {
            foreach (var item in dl.PrintAllStation())
            {
                yield return (new baseStationForList
                {
                    id = item.Id,
                    name=item.Name,
                    availableSlots=item.ChargeSlots,
                    usedSlots= dl.displayChargings(item.Id).Count() //catch

                });          
            }
        }

        public IEnumerable<baseStationForList> displayStationListSlotsAvailable()
        {
            foreach (var item in dl.PrintStationWithChargeSlots())
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
