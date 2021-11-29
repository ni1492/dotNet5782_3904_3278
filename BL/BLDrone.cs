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
        
        public void addDrone(drone drone)
        {
            foreach (Drone item in dl.PrintAllDrone())
            {
                drones.Add(item);
            }
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
