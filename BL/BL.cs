using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IDAL.DO;
using DAL.IDAL;
using IBL.BO;
namespace IBL
{
    public partial class BL : IBL
    {
        public IDal dl;
        public List<droneForList> drones;
        public double availablePK;
        public double lightPK;
        public double mediumPK;
        public double heavyPK;
        public double chargingPH;
        public BL()
        {
            dl = new DALObject.DALObject();
            drones = new List<droneForList>();
            double[] powerUse = dl.powerUse();
            availablePK = powerUse[0];
            lightPK = powerUse[1];
            mediumPK = powerUse[2];
            heavyPK = powerUse[3];
            chargingPH = powerUse[4];

        }
        private void initializeDrone()
        {
            foreach (var item in dl.PrintAllDrone())
            {
                drones.Add(new droneForList
                {
                    id = item.Id,
                    model = item.Model,
                    weight = (WeightCategories)item.MaxWeight
                });
            }
            foreach (var item in drones)
            {
                if (item.)
            }

        }
        private bool isDelivered(int droneId)
        {

            foreach (var item in dl.PrintAllParcel())
            {
                if ((item.DroneId == droneId) && (item.Delivered != new DateTime(0, 0, 0, 0, 0, 0)))
                {
                    return true;
                }
            }
            return false;
        }
        private bool isPickedUp(int droneId)
        {
            foreach (var item in dl.PrintAllParcel())
            {
                if ((item.DroneId == droneId) && (item.PickedUp != new DateTime(0, 0, 0, 0, 0, 0)))
                {
                    return true;
                }
            }
            return false;
        }
        private bool isMatched(int droneId)
        {
            foreach (var item in dl.PrintAllParcel())
            {
                if (item.DroneId == droneId)
                {
                    return true;
                }
            }
            return false;
        }
        private location senderLocation(int droneId)
        {
            foreach (var item in dl.PrintAllParcel())
            {
                if (item.DroneId == droneId)
                {
                    foreach (var item2 in dl.PrintAllCustomer())
                    {
                        if (item.SenderId == item2.Id)
                        {
                            location l = new location();
                            l.Latitude = item2.Lattitude;
                            l.Longitude = item2.Longitude;
                            return l;
                        }
                    }
                }
            }
            location x = new location();
            x.Latitude = 0;
            x.Longitude = 0;
            return x;

        }
        private location targetLocation(int targetId)
        {
            foreach (var item in dl.PrintAllCustomer())
            {
                if (targetId == item.Id)
                {
                    location l = new location();
                    l.Latitude = item.Lattitude;
                    l.Longitude = item.Longitude;
                    return l;
                }
            }
            location x = new location();
            x.Latitude = 0;
            x.Longitude = 0;
            return x;

        }
        private location nearestStation(int droneId)
        {
            location sender = senderLocation(droneId);
            List<baseStation> locations = new List<baseStation>();
            foreach (var baseStation in dl.PrintAllStation())
            {
                locations.Add(new baseStation
                {
                    location = new location
                    {
                        Latitude = baseStation.Lattitude,
                        Longitude = baseStation.Longitude
                    }
                });
            }
            location location = locations[0].location;
            double smallest = calcDistance(sender, locations[0].location);
            double temp;
            for (int i = 0; i < locations.Count(); i++)
            {
                temp = calcDistance(sender, locations[i].location);
                if (temp < smallest)
                {
                    smallest = temp;
                    location = locations[i].location;
                }
            }
            return location;
        }
        private location stationLocation(int stationId)
        {
            foreach (var item in dl.PrintAllStation())
            {
                if (item.Id == stationId)
                    return (new location
                    {
                        Latitude = item.Lattitude,
                        Longitude = item.Longitude
                    });
            }
            return new location();
        }
        /*private double calcMinBattery(droneForList d)
        {
            if (d.status == DroneStatuses.available)
            {
                location location = nearestStation(d.id);
                return calcDistance(d.currentLocation, location) * availablePK;
            }
            if (d.status == DroneStatuses.delivery)
            {
                WeightCategories weight;
                foreach (var item in dl.PrintAllParcel())
                {
                    if (item.Id == d.parcelID && isPickedUp(d.id))
                    {
                        weight = (WeightCategories)item.Weight;
                       double distance1= calcDistance(d.currentLocation, targetLocation(item.TargetId));
                        double distance2 = calcDistance(targetLocation(item.TargetId), nearestStation();

                        switch (weight)
                        {
                            case WeightCategories.light:
                                return (distance1* lightPK)
                                break;
                            case WeightCategories.medium:
                                break;
                            case WeightCategories.heavy:
                                break;
                            default:
                                break;
                        }
                    }
                }

            }
        }*/
    }

}
