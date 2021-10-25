using System;
using IDAL.DO;
using DALObject;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            DALObject.DataSource.Initialize();//initialize components
            //the following initializations is going to be used later on in the program:
            int inputVal1=0;//first user choice - outer switch-case
            int inputVal2 = 0;//second user choice - inner switch-case
            int id=0;//id for parcel, station, customer
            string model="";//drone model
            WeightCategories weight = WeightCategories.light;//weight within drone object and parcel
            DroneStatuses status = DroneStatuses.available;//enum status
            double battery = 0; //0-100
            int name = 0;//station
            double longitude = 0;//0-50
            double lattitude = 0;//0-50
            int chargeSlots = 0;//amount of charging spots
            string customerName = "";//customer name
            string phone = "";//in order to write a zero in the beginning
            int sId = 0;//sender
            int tId = 0;//target
            int dId = 0;//drone
            Priorities priority = Priorities.regular;//enum priorities
            DateTime req = DateTime.Now;//request
            DateTime sch = DateTime.Now;//schedule
            DateTime pUp = DateTime.Now;//pick up
            DateTime del = DateTime.Now;//delivered
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
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out name);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out longitude);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out lattitude);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out chargeSlots);
                                        DALObject.DALObject.AddStation(id, name, longitude, lattitude, chargeSlots);
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
                                        DroneStatuses.TryParse(input, out status);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out battery);
                                        DALObject.DALObject.AddDrone(dId, model, weight, status, battery);
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
                                        DALObject.DALObject.AddCustomer(id, customerName, phone, longitude, lattitude);
                                        break;
                                    }
                                case 4://add parcel
                                    {
                                        Console.WriteLine("enter: id, sender id, target id, weight, priority, drone id, request, schedule, pick up, delivery");
                                        Console.WriteLine("weight options: light, medium, heavy");
                                        Console.WriteLine("Priorities options: regular, quick, urgent");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out sId);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out tId);
                                        input = Console.ReadLine();
                                        WeightCategories.TryParse(input, out weight);
                                        input = Console.ReadLine();
                                        Priorities.TryParse(input, out priority);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out dId);
                                        input = Console.ReadLine();
                                        DateTime.TryParse(input, out req);
                                        input = Console.ReadLine();
                                        DateTime.TryParse(input, out sch);
                                        input = Console.ReadLine();
                                        DateTime.TryParse(input, out pUp);
                                        input = Console.ReadLine();
                                        DateTime.TryParse(input, out del);
                                        DALObject.DALObject.AddParcel(id, sId, tId, weight, priority, dId, req, sch, pUp, del);
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
                                        Console.WriteLine("enter parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        DALObject.DALObject.Match(DALObject.DALObject.ConvertParcel(id));
                                        break;
                                    }
                                case 2://update pick up by drone
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        DALObject.DALObject.PickUpTime(DALObject.DALObject.ConvertParcel(id));
                                        break;
                                    }
                                case 3://update delivery parcel status
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        DALObject.DALObject.DeliveryTime(DALObject.DALObject.ConvertParcel(id));
                                        break;
                                    }
                                case 4://send drone to charge
                                    {
                                        Console.WriteLine("all the available stations:");
                                        DALObject.DALObject.PrintStationWithChargeSlots();//***
                                        Console.WriteLine("enter the drone and station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out sId);
                                        DALObject.DALObject.ChargingDrone(DALObject.DALObject.ConvertDrone(id), DALObject.DALObject.ConvertStation(sId));
                                        break;
                                    }
                                case 5://release drone from charging
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.ReleaseChargingDrone(DALObject.DALObject.ConvertDrone(id));
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
                                        DALObject.DALObject.PrintStation(id);
                                        break;
                                    }
                                case 2://display drone
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintDrone(id);
                                        break;
                                    }
                                case 3://display customer
                                    {
                                        Console.WriteLine("enter the customer id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintCustomer(id);
                                        break;
                                    }
                                case 4://display parcel
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintParcel(id);
                                        break;
                                    }
                                case 5:
                                    {
                                        Console.WriteLine("enter the station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        Station station = DALObject.DALObject.ConvertStation(id);
                                        Console.WriteLine("enter your location");
                                        input = Console.ReadLine();
                                        double.TryParse(input, out longitude);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out lattitude);
                                        double distance = DALObject.DALObject.CalculateDistance(station.Longitude, station.Lattitude, longitude, lattitude);
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
                                        DALObject.DALObject.PrintAllStation();
                                        break;
                                    }
                                case 2://display all drones
                                    {
                                        DALObject.DALObject.PrintAllDrone();
                                        break;
                                    }
                                case 3://display all customers
                                    {
                                        DALObject.DALObject.PrintAllCustomer();
                                        break;
                                    }
                                case 4://display all parcels
                                    {
                                        DALObject.DALObject.PrintAllParcel();
                                        break;
                                    }
                                case 5://display all parcels not assigned to any drone
                                    {
                                        DALObject.DALObject.PrintParcelsWithNoDrone();
                                        break;
                                    }
                                case 6://display all base stations with available charging slots
                                    {
                                        DALObject.DALObject.PrintStationWithChargeSlots();
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
