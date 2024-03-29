﻿using System;
using DAL.DalApi;
using DAL;
using DO;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DAL.DalApi.IDal mainObject = DalApi.DalFactory.getDal(false);
            //DALObject.DALObject mainObject = new DALObject.DALObject();
            //the following initializations is going to be used later on in the program:
            int inputVal1 = 0;//FirstOrDefault user choice - outer switch-case
            int inputVal2 = 0;//second user choice - inner switch-case
            int id = 0;//id for parcel, station, customer
            string model = "";//drone model
            WeightCategories weight = WeightCategories.light;//weight within drone object and parcel
            //DroneStatuses status = DroneStatuses.available;//enum status
            double battery = 0; //0-100
            string name = null;//station
            double longitude = 0;//0-50
            double lattitude = 0;//0-50
            int chargeSlots = 0;//amount of charging spots
            string customerName = "";//customer name
            string phone = "";//in order to write a zero in the beginning
            int sId = 0;//sender
            int tId = 0;//target
            int dId = 0;//drone
            Priorities priority = Priorities.regular;//enum priorities
            //end of initialization
            do
            {
                //main menu for user: add, update, display, list display, exit.
                Console.WriteLine("Choose one of the following options:\n");
                Console.WriteLine("1: Adding options\n");
                Console.WriteLine("2: Update options\n");
                Console.WriteLine("3: Display options\n");
                Console.WriteLine("4: List display options\n");
                Console.WriteLine("5: exit\n");
                string input = Console.ReadLine();//read FirstOrDefault choice
                Int32.TryParse(input, out inputVal1);
                switch (inputVal1)
                {
                    case 1://add
                        {
                            Console.WriteLine("Choose one of the following options:\n");
                            Console.WriteLine("1: Add new base-station \n");
                            Console.WriteLine("2: Add new drone\n");
                            Console.WriteLine("3: Add new customer\n");
                            Console.WriteLine("4: Add new parcel for delivery\n");
                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1://add station
                                    {
                                        Console.WriteLine("enter: id, name(number), longitude, lattitude, charge slots number");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        name = Console.ReadLine();
                                        input = Console.ReadLine();
                                        double.TryParse(input, out longitude);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out lattitude);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out chargeSlots);
                                        mainObject.AddStation(id, name, longitude, lattitude, chargeSlots);
                                        break;
                                    }
                                case 2://add drone
                                    {
                                        Console.WriteLine("enter: id, model, max weight, drone status, battery");
                                        Console.WriteLine("weight options: light, medium, heavy");
                                        Console.WriteLine("DroneStatuses options: available, maintenance, delivery");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out dId);
                                        model = Console.ReadLine();
                                        input = Console.ReadLine();
                                        WeightCategories.TryParse(input, out weight);
                                        input = Console.ReadLine();
                                        //DroneStatuses.TryParse(input, out status);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out battery);
                                        mainObject.AddDrone(dId, model, weight);
                                        break;
                                    }
                                case 3://add customer
                                    {
                                        Console.WriteLine("enter: id, name, phone number, longitude, lattitude");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        customerName = Console.ReadLine();
                                        phone = Console.ReadLine();
                                        input = Console.ReadLine();
                                        double.TryParse(input, out longitude);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out lattitude);
                                        mainObject.AddCustomer(id, customerName, phone, longitude, lattitude);
                                        break;
                                    }
                                case 4://add parcel
                                    {
                                        Console.WriteLine("enter: sender id, target id, weight, priority");
                                        Console.WriteLine("weight options: light, medium, heavy");
                                        Console.WriteLine("Priorities options: regular, quick, urgent");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out sId);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out tId);
                                        input = Console.ReadLine();
                                        WeightCategories.TryParse(input, out weight);
                                        input = Console.ReadLine();
                                        Priorities.TryParse(input, out priority);
                                        mainObject.AddParcel(new Parcel
                                        {
                                            Id=0,
                                            SenderId= sId,
                                            TargetId= tId,
                                            Weight=weight,
                                            Priority=priority,
                                            DroneId=0,
                                            Requested=DateTime.Now,
                                            Delivered=null,
                                            PickedUp=null,
                                            Scheduled=null
                                        });
                                        break;
                                    }
                                default:
                                    Console.WriteLine("ERROR\n");
                                    break;
                            }
                            break;
                        }
                    case 2://update
                        {
                            Console.WriteLine("Choose one of the following options:\n");
                            Console.WriteLine("1: Update parcel and drone connection\n");
                            Console.WriteLine("2: Update pickup parcel by drone\n");
                            Console.WriteLine("3: Update delivery parcel status\n");
                            Console.WriteLine("4: Update battery status: send drone to charge\n");
                            Console.WriteLine("5: Update battery status: release drone frome charging\n");
                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1://match drone to parcel
                                    {
                                        Console.WriteLine("enter parcel id and drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out dId);//drone
                                        mainObject.Match(id, dId);
                                        break;
                                    }
                                case 2://update pick up by drone
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        mainObject.PickUpTime(mainObject.ConvertParcel(id));
                                        break;
                                    }
                                case 3://update delivery parcel status
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        mainObject.DeliveryTime(mainObject.ConvertParcel(id));
                                        break;
                                    }
                                case 4://send drone to charge
                                    {
                                        Console.WriteLine("all the available stations:");
                                        mainObject.DisplayStations(station => station.ChargeSlots != 0);
                                        Console.WriteLine("enter the drone and station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out sId);
                                        mainObject.ChargingDrone(id, sId);
                                        break;
                                    }
                                case 5://release drone from charging
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        mainObject.ReleaseChargingDrone(id);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("ERROR\n");
                                    break;
                            }
                            break;
                        }
                    case 3://display
                        {
                            Console.WriteLine("Choose one of the following options:\n");
                            Console.WriteLine("1: Display base-station\n");
                            Console.WriteLine("2: Display drone\n");
                            Console.WriteLine("3: Display customer\n");
                            Console.WriteLine("4: Display parcel\n");
                            Console.WriteLine("5: Display distance from station\n");
                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1://display base station
                                    {
                                        Console.WriteLine("enter the station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(mainObject.DisplayStations(station => station.Id == id));
                                        break;
                                    }
                                case 2://display drone
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(mainObject.DisplayDrones(drone => drone.Id == id));
                                        break;
                                    }
                                case 3://display customer
                                    {
                                        Console.WriteLine("enter the customer id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(mainObject.DisplayCustomers(customer => customer.Id == id));
                                        break;
                                    }
                                case 4://display parcel
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(mainObject.DisplayParcels(parcel => parcel.Id == id));
                                        break;
                                    }
                                case 5://display distance from station to user location
                                    {
                                        Console.WriteLine("enter the station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Station station = mainObject.ConvertStation(id);
                                        Console.WriteLine("enter your location");
                                        input = Console.ReadLine();
                                        double.TryParse(input, out longitude);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out lattitude);
                                        double distance = mainObject.CalculateDistance(station.Longitude, station.Lattitude, longitude, lattitude);
                                        Console.WriteLine(distance);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("ERROR\n");
                                    break;
                            }
                            break;
                        }
                    case 4://display list
                        {
                            Console.WriteLine("Choose one of the following options:\n");
                            Console.WriteLine("1: Display all base-stations\n");
                            Console.WriteLine("2: Display all drones\n");
                            Console.WriteLine("3: Display all customers\n");
                            Console.WriteLine("4: Display all parcels\n");
                            Console.WriteLine("5: Display all parcels not asigned to any drone\n");
                            Console.WriteLine("6: Display all base-stations with available charging slots\n");
                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1://display all base stations
                                    {
                                        foreach (Station item in mainObject.DisplayStations(station => true))
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 2://display all drones
                                    {
                                        foreach (Drone item in mainObject.DisplayDrones(drone => true))
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 3://display all customers
                                    {
                                        foreach (Customer item in mainObject.DisplayCustomers(customer => true))
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 4://display all parcels
                                    {
                                        foreach (Parcel item in mainObject.DisplayParcels(parcel => true))
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 5://display all parcels not assigned to any drone
                                    {
                                        foreach (Parcel item in mainObject.DisplayParcels(parcel => parcel.DroneId == 0))
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 6://display all base stations with available charging slots
                                    {
                                        foreach (Station item in mainObject.DisplayStations(station => station.ChargeSlots != 0))
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                default:
                                    Console.WriteLine("ERROR\n");
                                    break;
                            }
                            break;
                        }
                    case 5://exit
                        {
                            Console.WriteLine("5: exit program\n");
                            break;
                        }
                    default:
                        break;
                }

            } while (inputVal1 != 5);
        }
    }
}
