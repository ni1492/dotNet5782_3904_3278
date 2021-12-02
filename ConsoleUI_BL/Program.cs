﻿using System;
using IBL.BO;
using IBL;
namespace ConsoleUI_BL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBL.IBL bl = new BL();
            int inputVal1 = 0;//first user choice - outer switch-case
            int inputVal2 = 0;//second user choice - inner switch-case
            int id = 0;//id for parcel, station, customer
            string model = "";//drone model
            WeightCategories weight = WeightCategories.light;//weight within drone object and parcel
            DroneStatuses status = DroneStatuses.available;//enum status
            string name = null;//station
            double longitude = 0;//0-50
            double lattitude = 0;//0-50
            int chargeSlots = 0;//amount of charging spots
            string customerName = "";//customer name
            string phone = "";//in order to write a zero in the beginning
            int sId = 0;//sender
            int tId = 0;//target
            int dId = 0;//drone
            int num = 0;
            DateTime time = DateTime.MinValue;
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
                string input = Console.ReadLine();//read first choice
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
                                        bl.addStation(new baseStation
                                        {
                                            id = id,
                                            name = name,
                                            location = new location
                                            {
                                                Longitude = longitude,
                                                Latitude = lattitude
                                            },
                                            chargingSlots = chargeSlots,
                                            dronesInCharging = new()
                                        });  
                                        break;
                                    }
                                case 2://add drone
                                    {
                                        Console.WriteLine("enter: id, model, max weight, drone status, station id");
                                        Console.WriteLine("weight options: light, medium, heavy");
                                        Console.WriteLine("DroneStatuses options: available, maintenance, delivery");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out dId);
                                        model = Console.ReadLine();
                                        input = Console.ReadLine();
                                        WeightCategories.TryParse(input, out weight);
                                        input = Console.ReadLine();
                                        DroneStatuses.TryParse(input, out status);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        bl.addDrone(new droneForList
                                        {
                                            id = dId,
                                            model = model,
                                            weight = weight,
                                            status = status
                                        }, id);
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
                                        bl.addCustomer(new customer
                                        {
                                            id = id,
                                            name = customerName,
                                            phone = phone,
                                            location = new location
                                            {
                                                Latitude = lattitude,
                                                Longitude = longitude
                                            }
                                        });
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
                                        bl.addParcel(new parcelInDelivery
                                        {
                                            sender=new customerForParcel
                                            {
                                                id=sId
                                            },
                                            receiver=new customerForParcel
                                            {
                                                id=tId
                                            },
                                            weight = weight,
                                            priority = priority
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
                            Console.WriteLine("1: Update drone name\n");
                            Console.WriteLine("2: Update station detailes\n");
                            Console.WriteLine("3: Update customer detailes\n");
                            Console.WriteLine("4: Update battery status: send drone to charge\n");
                            Console.WriteLine("5: Update battery status: release drone frome charging\n");
                            Console.WriteLine("6: Update parcel and drone connection\n");
                            Console.WriteLine("7: Update deliver parcel by drone\n");
                            Console.WriteLine("8: Update pick up parcel by drone\n");


                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1://Update drone name//need func
                                    {
                                        Console.WriteLine("enter drone id and new name");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        name = Console.ReadLine();
                                        bl.updateDrone(id, name);
                                        break;
                                    }
                                case 2://Update station detailes//need func
                                    {
                                        Console.WriteLine("enter the station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine("(enter 1 for changing name, 2 for changing number of charging slots, 3 for both)");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out num);
                                        if(num==1)
                                        {
                                            Console.WriteLine("enter name");
                                            name = Console.ReadLine();
                                            chargeSlots = 0;
                                        }
                                        if (num==2)
                                        {
                                            Console.WriteLine("enter number of charging slots");
                                            input = Console.ReadLine();
                                            Int32.TryParse(input, out chargeSlots);
                                            name = null;
                                        }
                                        else
                                        {
                                        Console.WriteLine("enter name and number of charging slots");
                                            name = Console.ReadLine();
                                            input = Console.ReadLine();
                                            Int32.TryParse(input, out chargeSlots);
                                        }
                                        bl.updateStation(id, name, chargeSlots);
                                        break;
                                    }
                                case 3://Update customer detailes//need func
                                    {
                                        Console.WriteLine("enter the customer id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine("(enter 1 for changing name, 2 for changing phone number, 3 for both)");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out num);
                                        if (num == 1)
                                        {
                                            Console.WriteLine("enter name");
                                            name = Console.ReadLine();
                                            phone = null;
                                        }
                                        if (num == 2)
                                        {
                                            Console.WriteLine("enter number of charging slots");
                                            phone = Console.ReadLine();
                                            name = null;
                                        }
                                        else
                                        {
                                            Console.WriteLine("enter name and number of charging slots");
                                            name = Console.ReadLine();
                                            phone = Console.ReadLine();
                                        }
                                        bl.updateCustomer(id, name, phone);
                                        break;
                                    }
                                case 4://Update battery status: send drone to charge
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        bl.sendDroneToCharge(id);
                                        break;
                                    }
                                case 5://Update battery status: release drone frome charging
                                    {
                                        Console.WriteLine("enter the drone id and the time it was charging");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        input = Console.ReadLine();
                                        DateTime.TryParse(input, out time);
                                        bl.releaseDroneFromCharge(id, time);//datetime
                                        break;
                                    }
                                case 6://Update parcel and drone connection
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        bl.matchParcelToDrone(id);
                                        break;
                                    }
                                case 7://Update deliver parcel by drone
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        bl.deliverParcel(id);
                                        break;
                                    }
                                case 8://Update pick up parcel by drone
                                    {
                                        Console.WriteLine("enter the pardel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        bl.pickupParcel(id);
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
                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1://display base station
                                    {
                                        Console.WriteLine("enter the station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(bl.displayStation(id));
                                        break;
                                    }
                                case 2://display drone
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(bl.displayDrone(id));
                                        break;
                                    }
                                case 3://display customer
                                    {
                                        Console.WriteLine("enter the customer id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(bl.displayCustomer(id));
                                        break;
                                    }
                                case 4://display parcel
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Console.WriteLine(bl.displayParcel(id));
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
                                        foreach (var item in bl.displayStationList())
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 2://display all drones
                                    {
                                        foreach (var item in bl.displayDroneList())
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 3://display all customers
                                    {
                                        foreach (var item in bl.displayCustomerList())
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 4://display all parcels
                                    {
                                        foreach (var item in bl.displayParcelList())
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 5://display all parcels not assigned to any drone
                                    {
                                        foreach (var item in bl.displayParcelListWithoutDrone())
                                        {
                                            Console.WriteLine(item);
                                        }
                                        break;
                                    }
                                case 6://display all base stations with available charging slots
                                    {
                                        foreach (var item in bl.displayStationListSlotsAvailable())
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
