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
        public Random r = new Random();
        public void addDrone(droneForList drone, int stationId)
        {
            drone.battery = (double)r.Next(20, 40);
            drone.status = DroneStatuses.maintenance;
            //List<int> stationIds = dl.PrintStationWithChargeSlots().Select(bs => bs.Id).ToList();
            //int stationId = stationIds[r.Next(stationIds.Count)];
            dl.AddCharging(drone.id, stationId);
            drone.currentLocation = stationLocation(stationId);
            drone.parcelID = 0;
            drones.Add(drone);
            //add exception - drone exists - check in the dal layer 
            //and add exception for station id
        }
        public void updateDrone(int id, string model)
        {

        }
        public void sendDroneToCharge(int id)
        {

        }
        public void releaseDroneFromCharge(int id, DateTime time)
        {
            //time???
        }
        public drone displayDrone(int id)
        {

        }
        public IEnumerable<droneForList> displayDroneList()
        {

        }
    }
}
