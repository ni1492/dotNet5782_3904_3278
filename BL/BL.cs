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
    public partial class BL:IBL
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
            Random r = new Random();
            foreach (var item in dl.PrintAllDrone())//bulding list of drones
            {
                drones.Add(new droneForList
                {
                    id = item.Id,
                    model = item.Model,
                    weight = (WeightCategories)item.MaxWeight
                });//adding drone to the list
            }
            foreach (var item in drones)
            {
                if ((isMatched(item.id)) && (!isDelivered(item.id)) && (isPickedUp(item.id))) //if matched and picked up but not delivered-on delivery
                {
                    item.status = DroneStatuses.delivery;
                    item.currentLocation = senderLocation(findParcelDelivery(item.id));//returns the location of the sender

                    double minBattery = calcMinBattery(item)+1; //returns the minimum battery needed to allow the drone to make the delivery
                    item.battery = (double)r.Next((int)minBattery, 100);

                }
                if ((isMatched(item.id)) && !(isPickedUp(item.id))) //id matched and not yet picked up
                {
                    item.status = DroneStatuses.delivery;
                    item.currentLocation = nearestStation(senderLocation(findParcelDelivery(item.id)));//returns the closet station to the sender

                    double minBattery = calcMinBattery(item)+1; //returns the minimum battery needed to allow the drone to make the delivery
                    item.battery = (double)r.Next((int)minBattery, 100);

                }
                else//not matched to any parcel
                {
                    item.status = (DroneStatuses)r.Next(1, 2);
                    if (item.status == DroneStatuses.available)
                    {
                        List<location> locations = new List<location>();
                        int count = 0;
                        foreach (var cus in dl.PrintAllCustomer())
                        {
                            bool accepted = false;
                            foreach (var parcel in dl.PrintAllParcel())
                            {
                                if(parcel.TargetId==cus.Id)
                                {
                                    accepted = true;
                                    break;
                                }

                            }
                            if(accepted)
                            {
                                locations.Add(new location
                                {
                                    Latitude = cus.Lattitude,
                                    Longitude=cus.Longitude
                                });
                                count++;
                            }

                        }
                        item.currentLocation = locations[r.Next(0, count)];
                        //the location in random from a list of customers that have had parcels delivered to them
                        double minBattery = calcMinBattery(item)+1; //returns the minimum battery needed to allow the drone to make the delivery
                        item.battery = (double)r.Next((int)minBattery, 100);
                    }
                    if (item.status == DroneStatuses.maintenance)
                    {
                        int stationId = r.Next(1, dl.PrintAllStation().Count());
                        item.currentLocation = stationLocation(stationId);
                            //the drone location will be the location of the station chosen
                        item.battery = (double)r.Next(0, 20);

                    }
                }
            }
        }
        private double calcDistance(location from, location to)//calculate thedistance between two locations 
            {
                int R = 6371 * 1000;
                double phi1=from.Latitude * Math.PI;
                double phi2 = to.Latitude * Math.PI;
                double deltaPhi = (to.Latitude - from.Latitude) * Math.PI / 180;
                double deltaLambda = (to.Longitude - from.Longitude) * Math.PI / 180;
                double a = Math.Sin(deltaPhi / 2) * Math.Sin(deltaPhi / 2) +
                    Math.Cos(phi1) * Math.Cos(phi2) *
                    Math.Sin(deltaLambda / 2) * Math.Sin(deltaLambda / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double d = R * c / 1000;
                return d;
            }
        private bool isDelivered(int droneId)//chack if the drone is deliverd
        {

            foreach (var item in dl.PrintAllParcel())//goes over all the parcel in the DAL layer
            {
                if ((item.DroneId == droneId) && (item.Delivered != new DateTime(0, 0, 0, 0, 0, 0)))//chack if the drone is deliverd
                {
                    return true;
                }
            }
            return false;
        }
        private bool isPickedUp(int droneId)//chack if the parcel is picked up
        {
            foreach (var item in dl.PrintAllParcel())//goes over all the parcel in the DAL layer
            {
                if ((item.DroneId == droneId) && (item.PickedUp != new DateTime(0, 0, 0, 0, 0, 0)))//chack if the parcel is picked up
                {
                    return true;
                }
            }
            return false;
        }
        private bool isMatched(int droneId)//chack if the drone is match to parcel
        {
            foreach (var item in dl.PrintAllParcel())//goes over all the parcel in the DAL layer
            {
                if (item.DroneId == droneId)//chack if the drone is match to parcel
                {
                    return true;
                }
            }
            return false;
        }
        private location senderLocation(int parcelId)//return tne sender location by the parcel id
        {
            foreach (var item in dl.PrintAllParcel())//goes over the list of parcels to find the parcel 
             {
                if (item.Id == parcelId)
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
        private location targetLocation(int parcelId)//return tne target location by the parcel id
        {
            foreach (var item in dl.PrintAllParcel())//goes over the list of parcels to find the parcel 
            {
                if (item.Id == parcelId)
                {
                    foreach (var item2 in dl.PrintAllCustomer())//goes over thre lust of customers in the DAL layer 
                    {
                        if (item.TargetId == item2.Id)//fide find the target customer
                        {
                            location l = new location();
                            l.Latitude = item2.Lattitude;
                            l.Longitude = item2.Longitude;
                            return l;//return the lication
                        }
                    }
                }
            }
            //maybe we need to do trow and catch in here.
            location x = new location();
            x.Latitude = 0;
            x.Longitude = 0;
            return x;

        }
        private location nearestStation(location loc)//return the closest statuon to the location given by the user
        { 
            List<baseStation> locations = new List<baseStation>();
            foreach (var baseStation in dl.PrintAllStation())//buliding a list of all the station with the locations
            {
                locations.Add(new baseStation
                {
                    location = new location
                    {
                        Latitude = baseStation.Lattitude,
                        Longitude = baseStation.Longitude
                    }
                });//add location
            }
            location location = locations[0].location;
            double smallest = calcDistance(loc, locations[0].location);
            double temp;
            for (int i = 0; i < locations.Count(); i++)//compering the distance to find the closest location
            {
                temp = calcDistance(loc, locations[i].location);
                if (temp < smallest)
                {
                    smallest = temp;
                    location = locations[i].location;
                }
            }
            return location;
        }
        private location nearestCharging(location loc)//return the closest statuon to the location given by the user
        {

            List<baseStation> locations = new List<baseStation>();
            foreach (var baseStation in dl.PrintAllStation())//buliding a list of all the station with the locations
            {
                if(baseStation.ChargeSlots>0)
                {
                    locations.Add(new baseStation
                    {
                        location = new location
                        {
                            Latitude = baseStation.Lattitude,
                            Longitude = baseStation.Longitude
                        }
                    });//add location
                }
            }
            location location = locations[0].location;
            double smallest = calcDistance(loc, locations[0].location);
            double temp;
            for (int i = 0; i < locations.Count(); i++)//compering the distance to find the closest location
            {
                temp = calcDistance(loc, locations[i].location);
                if (temp < smallest)
                {
                    smallest = temp;
                    location = locations[i].location;
                }
            }
            return location;
        }
        private location stationLocation(int stationId)//return thr location of the station, by the id
        {
            foreach (var item in dl.PrintAllStation())//gos over the list of station in the DAL layer
            {
                if (item.Id == stationId)//fide the station by the id
                    return (new location
                    {
                        Latitude = item.Lattitude,
                        Longitude = item.Longitude
                    });//return the location
            }
            return new location();
        }
        private double calcMinBattery(droneForList d)//calculate the battery needed to complish the delivery
        {
            if (d.status == DroneStatuses.available)//if the drone is avilable-need to calc the batery to get to a station with a free slot
            {
                location location = nearestCharging(d.currentLocation);
                return calcDistance(d.currentLocation, location) * availablePK;
            }
            if (d.status == DroneStatuses.delivery)//if the drone is avilable-need to calc the batery to get to the dastination and to a station with a free slot
            {
                WeightCategories weight;
                foreach (var item in dl.PrintAllParcel())
                {
                    if (item.Id == d.parcelID && isPickedUp(d.id))
                    {
                        weight = (WeightCategories)item.Weight;
                       double distance1= calcDistance(d.currentLocation, targetLocation(item.Id));//the distance between the drone and the destination
                        double distance2 = calcDistance(targetLocation(item.Id), nearestCharging(targetLocation(item.Id)));//the distance between the destination and the nearest station

                        switch (weight)//calculate the battery
                        {
                            case WeightCategories.light:
                                return ((distance1 * lightPK) + (distance2 * availablePK));
                            case WeightCategories.medium:
                                return ((distance1 * mediumPK) + (distance2 * availablePK));
                            case WeightCategories.heavy:
                                return ((distance1 * heavyPK) + (distance2 * availablePK));
                            default:
                                break;
                        }
                    }
                    else
                    {
                        weight = (WeightCategories)item.Weight;
                        double distance1 = calcDistance(d.currentLocation, targetLocation(item.Id));//the distance between the drone and the destination
                        double distance2 = calcDistance(targetLocation(item.Id), nearestCharging(targetLocation(item.Id)));//the distance between the destination and the nearest station
                        double distance3 = calcDistance(d.currentLocation, senderLocation(item.Id));//the distance between the drone and the parcel to collect
                        switch (weight)//calculate the battery
                        {
                            case WeightCategories.light:
                                return ((distance1 * lightPK) + ((distance2+distance3)* availablePK));
                            case WeightCategories.medium:
                                return ((distance1 * mediumPK) + ((distance2 + distance3) * availablePK));
                            case WeightCategories.heavy:
                                return ((distance1 * heavyPK) + ((distance2 + distance3) * availablePK));
                            default:
                                break;
                        }
                    }
                }

            }
            return 0;
        }
        private int findParcelDelivery(int droneId)//find the parcel that in delivery
        {
            foreach (var item in dl.PrintAllParcel())//goes over the list of parcels to find the parcel 
            {
                if (item.DroneId == droneId)//find the parcel that in delivery
                {
                    return item.Id;
                }
            }
            return 0;
        }
    }

}

