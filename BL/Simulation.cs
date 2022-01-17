using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using static System.Math;
//using System.Diagnostics;
using BO;


namespace BlApi
{
    class Simulation
    {
        private static double V = 50;
        private static int delayMS = 500;
        private static double accuracy = 0.0001;
        private static BO.droneForList drone;

        enum status { deliver, charge, wait,toCharge};
        private status droneStatus = status.charge;

        private location targetLocation;
        private double distanceFromTarget = 0;
        private double batteryUsage;



        public Simulation(BlApi.BL bl, int dId, Action updateDisplay, Func<bool> stop)
        {
            lock (bl)
            {
                drone = bl.displayDrones(d => d.id == dId).FirstOrDefault();
            }
            while (!stop())
            {
                if (drone.status == DroneStatuses.available)
                {
                    availableDrone(bl);
                }
                else if (drone.status == DroneStatuses.delivery)
                {
                    deliveryDrone(bl);
                }
                else if (drone.status == DroneStatuses.maintenance)
                {
                    chargedDrone(bl);
                }
                updateDisplay();
            }
        }

        private void chargedDrone(BlApi.BL bl)
        {
            if (delay())
            {
                switch (droneStatus)
                {
                    case status.toCharge:
                        try
                        {
                            lock (bl)
                            {
                                lock (bl.dl)
                                {
                                    location currentLoc = drone.currentLocation;
                                    double currentBattery = drone.battery;
                                    drone.status = DroneStatuses.available;
                                    bl.sendDroneToCharge(drone.id);
                                    drone.currentLocation = currentLoc;
                                    drone.battery = currentBattery;
                                    droneStatus = status.deliver;
                                    int stationId = bl.dl.displayDronesInCharge(d => d.DroneId == drone.id).FirstOrDefault().StationId;
                                    location stationLoc = bl.displayStation(stationId).location;
                                    targetLocation = stationLoc;
                                    distanceFromTarget = bl.calcDistance(drone.currentLocation, stationLoc);
                                    batteryUsage = bl.availablePK;
                                }
                            }
                        }
                        catch (Exception ex) when (ex is BO.exceptions.TimeException || ex is BO.exceptions.NotFoundException)
                        {
                            //if the closest station did not have open charging slots
                            drone.status = DroneStatuses.maintenance;
                            droneStatus = status.wait;
                        }
                        break;
                    case status.deliver:
                        lock(bl)
                        {
                            calculate(bl);
                        }
                        if (distanceFromTarget == 0)
                        {
                            droneStatus = status.charge;
                        }
                        break;
                    case status.charge:
                        double timePassed = (double)delayMS / 1000;
                        drone.battery += bl.chargingPH * timePassed;
                        drone.battery = Min(drone.battery, 100);
                        if (drone.battery == 100)
                            lock (bl)
                            {
                                bl.releaseDroneFromCharge(drone.id); 
                            }
                        break;
                    case status.wait: //try sending drone to charge - waiting until a station close enough has empty charge slots
                        droneStatus = status.toCharge;
                        break;
                    default:
                        break;
                }

            }
        }

        private void deliveryDrone(BlApi.BL bl)
        {
            if (delay())
            {
                lock (bl)
                {
                    parcel parcel = bl.displayParcel(drone.parcelID);
                    bool pickedUp = parcel.pickup is not null;
                    targetLocation = pickedUp ? bl.targetLocation(parcel.id) : bl.senderLocation(parcel.id);
                    if (pickedUp)
                    {
                        switch (parcel.weight)
                        {
                            case WeightCategories.light:
                                batteryUsage = bl.lightPK;
                                break;
                            case WeightCategories.medium:
                                batteryUsage = bl.mediumPK;
                                break;
                            case WeightCategories.heavy:
                                batteryUsage = bl.heavyPK;
                                break;
                        }
                    }
                    else
                        batteryUsage = bl.availablePK;
                    calculate(bl);
                    if (distanceFromTarget == 0)
                    {
                        if (!pickedUp)
                            bl.pickupParcel(drone.id);
                        else
                        {
                            bl.deliverParcel(drone.id);
                            batteryUsage = bl.availablePK;
                        }
                    }
                }
            }
        }

        private void availableDrone(BlApi.BL bl)
        {
            if (delay())
            {
                lock (bl)
                {
                    try 
                    {
                        bl.matchParcelToDrone(drone.id); 
                    }
                    catch (BO.exceptions.NotFoundException ex)
                    {
                      if (drone.battery == 100) //drone cant collect any parcel at all
                            return;
                        else if(ex.Message.Equals("no parcel can be matched to the drone"))
                        {
                            drone.status = DroneStatuses.maintenance;
                            droneStatus = status.toCharge;
                        } //drone cant collect any parcel because of his battery
                        else
                        {
                            return;   
                        }
                    }
                }
            }
        }
        private static bool delay()
        {
            try 
            { 
                Thread.Sleep(delayMS); 
            }
            catch (Exception)
            { 
                return false; 
            }
            return true;
        }
      
        private void calculate(BlApi.BL bl)
        {
            lock(bl)
            {
                distanceFromTarget = bl.calcDistance(drone.currentLocation, targetLocation);
                if (distanceFromTarget < accuracy)
                {
                    distanceFromTarget = 0;
                    drone.currentLocation = targetLocation;
                    return;
                }
                double timePassed = (double)delayMS / 1000;
                double distanceChange = V * timePassed;
                double change = Min(distanceChange, distanceFromTarget); //in case the drone has theoretically passed the target
                double proportionalChange = change / distanceFromTarget;
                drone.battery = Max(0.0, drone.battery - change * batteryUsage);
                double droneLat = drone.currentLocation.Latitude;
                double droneLong = drone.currentLocation.Longitude;
                double targetLat = targetLocation.Latitude;
                double targetLong = targetLocation.Longitude;
                double lat = droneLat + (targetLat - droneLat) * proportionalChange; //we ignore the shipua of earth
                double lon = droneLong + (targetLong - droneLong) * proportionalChange;
                drone.currentLocation = new location { Longitude = lon, Latitude = lat };
                distanceFromTarget = bl.calcDistance(drone.currentLocation, targetLocation);
            }
          
        }
    }
}