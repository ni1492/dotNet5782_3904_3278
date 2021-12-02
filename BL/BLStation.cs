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
        public void addStation(baseStation station)
        {
            station.dronesInCharging = null;
            try
            {
                dl.AddStation(station.id,station.name , station.location.Longitude, station.location.Latitude, station.chargingSlots);
            }
            catch
            {
                //exception
                //question - when do we initialize the list in each station??
            }
        }
        public void updateStation(int id, string name, int chargingSlots)
        {
            IDAL.DO.Station tempDL = dl.PrintStation(id);
            dl.deleteStation(id);
            if (name != null)
                tempDL.Name = name;
            if(chargingSlots!=0)
            {
                if(chargingSlots> dl.displayChargings(id).Count())
                    tempDL.ChargeSlots = chargingSlots - dl.displayChargings(id).Count();
               //else- throw- not enoght slots
            }
            dl.AddStation(tempDL.Id, tempDL.Name, tempDL.Longitude, tempDL.Lattitude, tempDL.ChargeSlots);
        }
        public baseStation displayStation(int id)
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

        private double getBattery(int droneId)
        {
            return drones.Find(drone => drone.id == droneId).battery;
        }

        public IEnumerable<baseStationForList> displayStationList()
        {
            foreach (var item in dl.PrintAllStation())
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
