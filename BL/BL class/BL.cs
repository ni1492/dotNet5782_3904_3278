using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IDAL.DO;
using DAL.DalApi;
using BO;
using DalApi;
namespace BlApi
{
    public partial class BL: IBL
    {
        public readonly IDal dl = DalFactory.getDal("DALObject");//initialize the DAL object
        public List<droneForList> drones = new List<droneForList>(); //the list of drones saved in the BL layer
        //שדות נוספים: פנוי, נושא משקל קל, בינוני וכבד+שדה של הטענה לשעה
        public double availablePK;
        public double lightPK;
        public double mediumPK;
        public double heavyPK;
        public double chargingPH;

        #region singelton
        static readonly BL instance = new BL();
        static BL()
        {
            instance.drones = new List<droneForList>(); //initialize the list of drones 
            //initializing the different variables 
            double[] powerUse = instance.dl.powerUse();
            instance.availablePK = powerUse[0];
            instance.lightPK = powerUse[1];
            instance.mediumPK = powerUse[2];
            instance.heavyPK = powerUse[3];
            instance.chargingPH = powerUse[4];
            instance.initializeDrone(); //initializing the program}
        }
        BL() { }
        public static BL Instance => instance;
        #endregion

        //public BL()
        //{
        //    dl = DalFactory.getDal("DALObject");//initialize the DAL object
        //    drones = new List<droneForList>(); //initialize the list of drones 
        //    //initializing the different variables 
        //    double[] powerUse = dl.powerUse(); 
        //    availablePK = powerUse[0];
        //    lightPK = powerUse[1];
        //    mediumPK = powerUse[2];
        //    heavyPK = powerUse[3];
        //    chargingPH = powerUse[4];
        //    initializeDrone(); //initializing the program

        //}
        private void initializeDrone()
        {
            Random r = new Random(); 
            foreach (var item in dl.DisplayDrones(drone => true))//bulding list of drones - adding them from the DAL layer
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
                    item.parcelID = findParcelDelivery(item.id);
                    item.currentLocation = senderLocation(item.parcelID);//returns the location of the sender

                    double minBattery = calcMinBattery(item); //returns the minimum battery needed to allow the drone to make the delivery
                    if (minBattery <= 100)
                        item.battery = (double)r.Next((int)minBattery, 101);
                    else
                    {
                        item.battery = (double)r.Next(50, 101);
                    }
                }
                else if ((isMatched(item.id)) && !(isPickedUp(item.id))) //is matched and not yet picked up
                {
                    item.status = DroneStatuses.delivery;
                    item.parcelID = findParcelDelivery(item.id);
                    item.currentLocation = nearestStation(senderLocation(item.parcelID));//returns the closet station to the sender

                    double minBattery = calcMinBattery(item); //returns the minimum battery needed to allow the drone to make the delivery
                    if (minBattery <= 100)
                        item.battery = (double)r.Next((int)minBattery, 101);
                    else
                    {
                        item.battery = (double)r.Next(50, 101);
                    }

                }
                else//not matched to any parcel
                {
                    item.status = (DroneStatuses)r.Next(1, 3); //the status will be randomized between in available and maintenance
                    if (item.status == DroneStatuses.available) //creating a list of all locations of customers that have had parcels delivered to them
                    {
                        List<location> locations = new List<location>();
                        int count = 0;
                        foreach (var cus in dl.DisplayCustomers(customer=>true))
                        {
                            bool accepted = false;
                            foreach (var parcel in dl.DisplayParcels(parcel => true))
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
                        if (count != 0)
                            item.currentLocation = locations[r.Next(0, count)];
                        else
                            item.currentLocation = new location
                            {
                                Latitude = r.Next(-180, 180) + (double)(r.Next(1000, 10000)) / 10000,
                                Longitude = r.Next(-90, 90) + (double)(r.Next(1000, 10000)) / 10000
                            };
                        //the location in random from a list of customers that have had parcels delivered to them
                        double minBattery = calcMinBattery(item); //returns the minimum battery needed to allow the drone to make the delivery
                        if(minBattery<=100)
                             item.battery = (double)r.Next((int)minBattery, 101);
                        else
                        {
                            item.battery = (double)r.Next(50, 101);//cant get to any station

                        }
                    }
                    else if (item.status == DroneStatuses.maintenance) //its location will be randomized between the different existing stations
                    {
                        int stationId = r.Next(1, dl.DisplayStations(station => true).Count()); 
                        item.currentLocation = stationLocation(stationId);
                            //the drone location will be the location of the station chosen
                        item.battery = (double)r.Next(0, 20); // the battery will be randomized between 0-20 percent 
                        dl.AddCharging(item.id, stationId);
                    }
                }
            }
        }
        private double calcDistance(location from, location to)//calculate thedistance between two locations 
            {
            return dl.CalculateDistance(from.Longitude, from.Latitude, to.Longitude, to.Latitude);
            }
        public bool isDelivered(int droneId)//check if the drone is deliverd
        {
            foreach (var item in dl.DisplayParcels(parcel => true))//goes over all the parcel in the DAL layer
            {
                if ((item.DroneId == droneId) && (item.Delivered !=null))//chack if the drone is deliverd
                {
                    return true;
                }
            }
            return false;
        }
        public bool isPickedUp(int droneId)//chack if the parcel is picked up
        {
            foreach (var item in dl.DisplayParcels(parcel => true))//goes over all the parcel in the DAL layer
            {
                if ((item.DroneId == droneId) && (item.PickedUp != null))//chack if the parcel is picked up
                {
                    return true;
                }
            }
            return false;
        }
        public bool isMatched(int droneId)//chack if the drone is match to parcel
        {
            foreach (var item in dl.DisplayParcels(parcel => true))//goes over all the parcel in the DAL layer
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
            var p = dl.DisplayParcels(parcel => parcel.Id == parcelId).First();
            var c = dl.DisplayCustomers(customer => customer.Id == p.SenderId).First();
            return (new location
            {
                Latitude = c.Lattitude,
                Longitude = c.Longitude
            });

        }
        private location targetLocation(int parcelId)//return tne target location by the parcel id
        {
            var p = dl.DisplayParcels(parcel => parcel.Id == parcelId).First();
            var c = dl.DisplayCustomers(customer => customer.Id == p.TargetId).First();
            return (new location
            {
                Latitude = c.Lattitude,
                Longitude = c.Longitude
            });

        }
        private location nearestStation(location loc)//return the closest statuon to the location given by the user
        { 
            List<baseStation> locations = new List<baseStation>();
            foreach (var baseStation in dl.DisplayStations(station=>true))//buliding a list of all the station with the locations
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
            foreach (var baseStation in dl.DisplayStations(station=>true))//buliding a list of all the station with the locations
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
            foreach (var item in dl.DisplayStations(station => true))//gos over the list of station in the DAL layer
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
                foreach (var item in dl.DisplayParcels(parcel => true))
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
            foreach (var item in dl.DisplayParcels(parcel => true))//goes over the list of parcels to find the parcel 
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

