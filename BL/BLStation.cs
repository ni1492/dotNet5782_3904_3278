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
                    battery = calcBattery(item.DroneId)
                });
            }
            return stationBO;
        }

        private double calcBattery(int droneId)
        {

        }

        public IEnumerable<baseStationForList> displayStationList()
        {

        }

        public IEnumerable<baseStationForList> displayStationListSlotsAvailable()
        {

        }
    }
}
