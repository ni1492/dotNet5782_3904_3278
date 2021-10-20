using System;
using IDAL.DO;
using DALObject;
namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputVal1=0;
            int inputVal2 = 0;
            int id=0;
            string model="";
            WeightCategories weight = WeightCategories.light;
            DroneStatuses status = DroneStatuses.available;
            double battery = 0;
            int name = 0;//station
            double longitude = 0;
            double lattitude = 0;
            int chargeSlots = 0;
            string customerName = "";
            string phone = "";
            int sId = 0;
            int tId = 0;
            int dId = 0;
            Priorities priority = Priorities.regular;
            DateTime req = DateTime.Now;
            DateTime sch = DateTime.Now;
            DateTime pUp = DateTime.Now;
            DateTime del = DateTime.Now;
            Console.WriteLine("Choose one of the following options:\n");
            Console.WriteLine("1: Adding options\n");
            Console.WriteLine("2: Update options\n");
            Console.WriteLine("3: Display options\n");
            Console.WriteLine("4: List display options\n");
            Console.WriteLine("5: exit\n");
            do
            {
                string input = Console.ReadLine();
                Int32.TryParse(input, out inputVal1);
                switch (inputVal1)
                {
                    case 1:
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
                                case 1:
                                    {
                                        Console.WriteLine("enter: id, name, longitude, lattitude, charge slots number");
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
                                case 2:
                                    {
                                        Console.WriteLine("enter: id, model, max weight, drone status, battery");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        model = Console.ReadLine();
                                        input = Console.ReadLine();
                                        WeightCategories.TryParse(input, out weight);
                                        input = Console.ReadLine();
                                        DroneStatuses.TryParse(input, out status);
                                        input = Console.ReadLine();
                                        double.TryParse(input, out battery);
                                        DALObject.DALObject.AddDrone(id, model, weight, status, battery);
                                        break;
                                    }
                                case 3:
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
                                case 4:
                                    {
                                        Console.WriteLine("enter: id, model, max weight, drone status, battery");
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
                    case 2:
                        {
                            Console.WriteLine("Choose one of the following options:\n");
                            Console.WriteLine("1: UpDate parcel and drone connection\n");
                            Console.WriteLine("2: UpDate pickup parcel by drone\n");
                            Console.WriteLine("3: UpDate delivery parcel status\n");
                            Console.WriteLine("4: UpDate battery status: send drone to charge\n");
                            Console.WriteLine("5: UpDate battery status: release drone frome charging\n");
                            input = Console.ReadLine();
                            Int32.TryParse(input, out inputVal2);
                            switch (inputVal2)
                            {
                                case 1:
                                    {
                                        Console.WriteLine("enter parcel and drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out dId);//drone
                                        DALObject.DALObject.Match(DALObject.DALObject.ConvertParcel(id), DALObject.DALObject.ConvertDrone(dId));
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        DALObject.DALObject.PickUpTime(DALObject.DALObject.ConvertParcel(id));
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);//parcel
                                        DALObject.DALObject.DeliveryTime(DALObject.DALObject.ConvertParcel(id));
                                        break;
                                    }
                                case 4:
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
                                case 5:
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
                    case 3:
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
                                case 1:
                                    {
                                        Console.WriteLine("enter the station id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintStation(id);
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("enter the drone id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintDrone(id);
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("enter the customer id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintCustomer(id);
                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("enter the parcel id");
                                        input = Console.ReadLine();
                                        Int32.TryParse(input, out id);
                                        DALObject.DALObject.PrintParcel(id);
                                        break;
                                    }
                                default:
                                    Console.WriteLine("ERROR\n");
                                    break;
                            }
                            break;
                        }
                    case 4:
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
                                case 1:
                                    {
                                        DALObject.DALObject.PrintAllStation();
                                        break;
                                    }
                                case 2:
                                    {
                                        DALObject.DALObject.PrintAllDrone();
                                        break;
                                    }
                                case 3:
                                    {
                                        DALObject.DALObject.PrintAllCustomer();
                                        break;
                                    }
                                case 4:
                                    {
                                        DALObject.DALObject.PrintAllParcel();
                                        break;
                                    }
                                case 5:
                                    {
                                        DALObject.DALObject.PrintParcelsWithNoDrone();
                                        break;
                                    }
                                case 6:
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
                    case 5:
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
