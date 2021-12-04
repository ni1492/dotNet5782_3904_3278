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
        public void addDrone(droneForList drone, int stationId) //adds new drone (to the list in DAL layer and in the BL layer)
        {
            drone.battery = (double)r.Next(20, 40); // the battery startes between 20-40 percent
            drone.status = DroneStatuses.maintenance; //started in charging
            drone.currentLocation = stationLocation(stationId); //the starting location is the charging station location
            drone.parcelID = 0;
            try
            {
                dl.AddDrone(drone.id, drone.model, (IDAL.DO.WeightCategories)drone.weight);
            }
            catch (Exception ex) //catches if the ID already exists
            {
                throw new BO.exceptions.ExistException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
            try
            {
                dl.ChargingDrone(drone.id, stationId); //we need to add to the charging list (DAL)
            }
            catch (Exception ex) //catches if the ID of the drone and the station exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
            drones.Add(drone); //if it doesnt already exist then we will add to the list of drones (BL)

        }
        public void matchParcelToDrone(int id)  //match fuction - recieves a drone ID
        {
            try
            {
                if (isMatched(id))
                {
                    int parcelId = parcelToMatch(id); //catch

                    dl.Match(parcelId, id); //catch
                    foreach (var item in drones)
                    {
                        if (item.id == id)
                        {
                            item.status = DroneStatuses.delivery;
                        }
                    }
                }
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public void updateDrone(int id, string model)  //update the drone model name
        {
            try
            {
                IDAL.DO.Drone tempDL = dl.PrintDrone(id); //catch  //find the drone in the DAL
                dl.deleteDrone(id); //catch  //delltes the old drone before the changes
                dl.AddDrone(tempDL.Id, model, tempDL.MaxWeight); //catch  //adds the drone with the changes
                droneForList tempBL = drones.Find(drone => drone.id == id); //finds it in the drone list
                tempBL.model = model; ///changes the model name
                drones.RemoveAll(drone => drone.id == id); //removes the old drone from the list
                drones.Add(tempBL); //adds the drone with the changes
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public void pickupParcel(int id) //sends a drone to pick up a parcel
        {
            try
            {
                int parcelId = drones.Find(drone => drone.id == id).parcelID;
                if (parcelId == 0 || getStatus(parcelId) != ParcelStatus.Scheduled)
                {
                    throw new BO.exceptions.TimeException("parcel not scheduled yet");
                }
                dl.PickUpTime(new Parcel
                {
                    Id = parcelId,
                    DroneId = id
                });
                foreach (var parcel in dl.PrintAllParcel())
                {
                    if (parcel.Id == parcelId)
                    {
                        foreach (var cust in dl.PrintAllCustomer())
                        {
                            if (cust.Id == parcel.SenderId)
                            {
                                drones.Find(drone => drone.id == id).battery -= availablePK * calcDistance(drones.Find(drone => drone.id == id).currentLocation, new location
                                {
                                    Latitude = cust.Lattitude,
                                    Longitude = cust.Longitude
                                });
                                drones.Find(drone => drone.id == id).currentLocation = (new location
                                {
                                    Latitude = cust.Lattitude,
                                    Longitude = cust.Longitude
                                });
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }

        }
        public void deliverParcel(int id) // drone delivers the parcel
        {
            try
            {
                int parcelId = drones.Find(drone => drone.id == id).parcelID;
                if (parcelId == 0 || getStatus(parcelId) != ParcelStatus.PickedUp)
                {
                    throw new BO.exceptions.TimeException("parcel not picked up yet");
                }
                dl.DeliveryTime(new Parcel
                {
                    Id = parcelId,
                    DroneId = id
                });
                foreach (var parcel in dl.PrintAllParcel())
                {
                    if (parcel.Id == parcelId)
                    {
                        foreach (var cust in dl.PrintAllCustomer())
                        {
                            if (cust.Id == parcel.TargetId)
                            {
                                drones.Find(drone => drone.id == id).battery -= availablePK * calcDistance(drones.Find(drone => drone.id == id).currentLocation, new location
                                {
                                    Latitude = cust.Lattitude,
                                    Longitude = cust.Longitude
                                });
                                drones.Find(drone => drone.id == id).currentLocation = (new location
                                {
                                    Latitude = cust.Lattitude,
                                    Longitude = cust.Longitude
                                });
                                break;
                            }
                        }
                        break;
                    }
                }
                drones.Find(drone => drone.id == id).status = DroneStatuses.available;
                drones.Find(drone => drone.id == id).parcelID = 0;
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public void sendDroneToCharge(int id) //sends the drone to charge
        {
            try
            {
                droneForList d = drones.Find(drone => drone.id == id);
                if (d.status != DroneStatuses.available)
                {
                    throw new BO.exceptions.TimeException("drone not available");
                    //throw-drone in delivery/maintane- cant send to charge
                }
                double battery = calcMinBattery(d);
                if (battery > d.battery)
                {
                    //throw- battery too low to get to station
                }
                location l = nearestCharging(d.currentLocation);
                foreach (var item in dl.PrintAllStation())
                {
                    if (item.Lattitude == l.Latitude && item.Longitude == l.Longitude)
                    {
                        dl.ChargingDrone(id, item.Id);
                    }
                }
                drones.Find(drone => drone.id == id).currentLocation = l;
                drones.Find(drone => drone.id == id).status = DroneStatuses.maintenance;
                drones.Find(drone => drone.id == id).battery -= battery;
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public void releaseDroneFromCharge(int id, DateTime time) //releases the drone from charging
        {
            try
            {
                if (drones.Find(drone => drone.id == id).status != DroneStatuses.maintenance)
                {
                    throw new BO.exceptions.TimeException("drone not in charging");
                }
                double battery = chargingPH * (time.Hour + time.Minute / 60);
                drones.Find(drone => drone.id == id).status = DroneStatuses.available;
                drones.Find(drone => drone.id == id).battery += battery;
                dl.ReleaseChargingDrone(id);
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        } 
        public drone displayDrone(int id) //displays the requested drone //remember to throw and catch
        {
            try
            {
                droneForList drone = drones.Find(drone => drone.id == id);
                parcel temp = displayParcel(drone.parcelID);
                if (temp != null)
                {
                    parcelInDelivery p = (new parcelInDelivery
                    {
                        id = temp.id,
                        weight = temp.weight,
                        priority = temp.priority,
                        sender = temp.sender,
                        receiver = temp.receiver,
                        pickUp = senderLocation(temp.id),
                        destination = targetLocation(temp.id)
                    });
                    if (getStatus(temp.id) == ParcelStatus.PickedUp)
                        p.status = true;
                    else
                        p.status = false;
                    return (new drone
                    {
                        id = drone.id,
                        model = drone.model,
                        weight = drone.weight,
                        currentLocation = drone.currentLocation,
                        battery = drone.battery,
                        status = drone.status,
                        parcel = p
                    });
                }
                return (new drone
                {
                    id = drone.id,
                    model = drone.model,
                    weight = drone.weight,
                    currentLocation = drone.currentLocation,
                    battery = drone.battery,
                    status = drone.status,
                    parcel = null
                });
            }
            catch (Exception ex) //catches if the ID not exists
            {
                throw new BO.exceptions.NotFoundException(ex.Message, ex); //sending inner exception for the exception returning from the DAL
            }
        }
        public IEnumerable<droneForList> displayDroneList() //displays the list of drones
        {
            foreach (var drone in drones)
            {
                yield return drone;
            }
        }
        private IEnumerable<parcelInDelivery> parcelsByPriority(List<parcelInDelivery> list, BO.Priorities prioritiy) //returns all the parcels based on the priority requested ---should this be in BLParcel??
        {
            foreach (var parcel in list)
            {
                if (parcel.priority == prioritiy)
                    yield return parcel;
            }
        }
        private IEnumerable<parcelInDelivery> parcelsByWeight(List<parcelInDelivery> list, BO.WeightCategories weight)//returns all the parcels based on the weight requested ----should this be in BLParcel??
        {
            foreach (var parcel in list)
            {
                if (parcel.weight == weight)
                    yield return parcel;
            }
        }
        private int parcelToMatch(int droneId) //finds the parcel that should be matched to the drone given (according to various standards)
        {
            BO.WeightCategories w = new();
            double battery = 0;
            droneForList d = new();
            foreach (var drone in drones)
            {
                if (droneId == drone.id)
                {
                    w = drone.weight;
                    battery = drone.battery;
                    d = (new droneForList
                    {
                        id = drone.id,
                        status = drone.status,
                        currentLocation = drone.currentLocation
                    });

                }
            }
            List<parcelInDelivery> list = new List<parcelInDelivery>();
            foreach (var parcel in displayParcelListWithoutDrone())
            {
                    list.Add(new parcelInDelivery
                    {
                        id = parcel.id,
                        weight = parcel.weight,
                        priority = parcel.priority,
                        pickUp = senderLocation(parcel.id),
                        destination = targetLocation(parcel.id),
                        status=false
                    });
            }
            BO.Priorities p = BO.Priorities.urgent;
            List<parcelInDelivery> parcels = new List<parcelInDelivery>();
            do
            {
                foreach (var item in parcelsByPriority(list, p))
                {
                    parcels.Add(item);
                }
                p--;
            } while (parcels == null && p != BO.Priorities.regular);
            if (p == BO.Priorities.regular)
                //trow exeption- no parcel to match
                list.Clear();
            do
            {
                foreach (var item in parcelsByWeight(parcels, w))
                {
                    list.Add(item);
                }
                w--;
            } while (list == null);
            double smallest = calcDistance(d.currentLocation, list[0].pickUp);
            int parcelId = list[0].id;
            foreach (var item in list)
            {
                double temp = calcDistance(d.currentLocation, item.pickUp);
                d.parcelID = item.id;
                if ((temp < smallest) && (calcMinBattery(d) <= battery))
                {
                    smallest = temp;
                    parcelId = item.id;
                }
            }
            return parcelId;
        }
    }
}
