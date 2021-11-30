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
        }
        public IEnumerable<baseStationForList> displayStationList()
        {

        }

        public IEnumerable<baseStationForList> displayStationListSlotsAvailable()
        {

        }
    }
}
