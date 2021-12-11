////targil 2
//using System;
//using IBL.BO;
//using IBL;
//namespace ConsoleUI_BL
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            IBL.IBL bl = new BL();
//            int inputVal1 = 0;//first user choice - outer switch-case
//            int inputVal2 = 0;//second user choice - inner switch-case
//            int id = 0;//id for parcel, station, customer
//            string model = "";//drone model
//            WeightCategories weight = WeightCategories.light;//weight within drone object and parcel
//            string name = null;//station
//            double longitude = 0;//0-50
//            double lattitude = 0;//0-50
//            int chargeSlots = 0;//amount of charging spots
//            string customerName = "";//customer name
//            string phone = "";//in order to write a zero in the beginning
//            int sId = 0;//sender
//            int tId = 0;//target
//            int dId = 0;//drone
//            int num = 0;
//            DateTime time;
//            Priorities priority = Priorities.regular;//enum priorities
//            //end of initialization
//            do
//            {
//                //main menu for user: add, update, display, list display, exit.
//                Console.WriteLine("Choose one of the following options:\n");
//                Console.WriteLine("1: Adding options\n");
//                Console.WriteLine("2: Update options\n");
//                Console.WriteLine("3: Display options\n");
//                Console.WriteLine("4: List display options\n");
//                Console.WriteLine("5: exit\n");
//                string input = Console.ReadLine();//read first choice
//                Int32.TryParse(input, out inputVal1);
//                switch (inputVal1)
//                {
//                    case 1://add
//                        {
//                            Console.WriteLine("Choose one of the following options:\n");
//                            Console.WriteLine("1: Add new base-station \n");
//                            Console.WriteLine("2: Add new drone\n");
//                            Console.WriteLine("3: Add new customer\n");
//                            Console.WriteLine("4: Add new parcel for delivery\n");
//                            input = Console.ReadLine();
//                            Int32.TryParse(input, out inputVal2);
//                            switch (inputVal2)
//                            {
//                                case 1://add station
//                                    {
//                                        Console.WriteLine("enter: id, name, charge slots number, longitude(30-33), lattitude(-30- -27)");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        name = Console.ReadLine();
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out chargeSlots);
//                                        do
//                                        {
//                                            Console.WriteLine("enter: longitude(30-33)");
//                                            input = Console.ReadLine();
//                                            double.TryParse(input, out longitude);
//                                        } while (longitude > 33 || longitude < 30);
//                                        do
//                                        {
//                                            Console.WriteLine("enter: lattitude(-30- -27)");
//                                            input = Console.ReadLine();
//                                            double.TryParse(input, out lattitude);
//                                        } while (lattitude > -27 || lattitude < -30);

//                                        try
//                                        {
//                                            bl.addStation(new baseStation
//                                            {
//                                                id = id,
//                                                name = name,
//                                                location = new location
//                                                {
//                                                    Longitude = longitude,
//                                                    Latitude = lattitude
//                                                },
//                                                chargingSlots = chargeSlots,
//                                                dronesInCharging = new()
//                                            });
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }

//                                        break;
//                                    }
//                                case 2://add drone
//                                    {
//                                        Console.WriteLine("enter: id, model, max weight, station id");
//                                        Console.WriteLine("weight options: light, medium, heavy");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out dId);
//                                        model = Console.ReadLine();
//                                        input = Console.ReadLine();
//                                        WeightCategories.TryParse(input, out weight);
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            bl.addDrone(new droneForList
//                                            {
//                                                id = dId,
//                                                model = model,
//                                                weight = weight
//                                            }, id);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }

//                                        break;
//                                    }
//                                case 3://add customer
//                                    {
//                                        Console.WriteLine("enter: id, name, phone number, longitude(30-33), lattitude(-30- -27)");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        customerName = Console.ReadLine();
//                                        phone = Console.ReadLine();
//                                        do
//                                        {
//                                            Console.WriteLine("enter: longitude(30-33)");
//                                            input = Console.ReadLine();
//                                            double.TryParse(input, out longitude);
//                                        } while (longitude > 33 || longitude < 30);
//                                        do
//                                        {
//                                            Console.WriteLine("enter: lattitude(-30- -27)");
//                                            input = Console.ReadLine();
//                                            double.TryParse(input, out lattitude);
//                                        } while (lattitude > -27 || lattitude < -30);
//                                        try
//                                        {
//                                            bl.addCustomer(new customer
//                                            {
//                                                id = id,
//                                                name = customerName,
//                                                phone = phone,
//                                                location = new location
//                                                {
//                                                    Latitude = lattitude,
//                                                    Longitude = longitude
//                                                }
//                                            });
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }

//                                        break;
//                                    }
//                                case 4://add parcel
//                                    {
//                                        Console.WriteLine("enter: sender id, target id, weight, priority");
//                                        Console.WriteLine("weight options: light, medium, heavy");
//                                        Console.WriteLine("Priorities options: regular, quick, urgent");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out sId);
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out tId);
//                                        input = Console.ReadLine();
//                                        WeightCategories.TryParse(input, out weight);
//                                        input = Console.ReadLine();
//                                        Priorities.TryParse(input, out priority);
//                                        try
//                                        {
//                                            bl.addParcel(new parcelInDelivery
//                                            {
//                                                sender = new customerForParcel
//                                                {
//                                                    id = sId
//                                                },
//                                                receiver = new customerForParcel
//                                                {
//                                                    id = tId
//                                                },
//                                                weight = weight,
//                                                priority = priority
//                                            });
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }

//                                        break;
//                                    }
//                                default:
//                                    Console.WriteLine("ERROR\n");
//                                    break;
//                            }
//                            break;
//                        }
//                    case 2://update
//                        {
//                            Console.WriteLine("Choose one of the following options:\n");
//                            Console.WriteLine("1: Update drone name\n");
//                            Console.WriteLine("2: Update station details\n");
//                            Console.WriteLine("3: Update customer details\n");
//                            Console.WriteLine("4: Update battery status: send drone to charge\n");
//                            Console.WriteLine("5: Update battery status: release drone frome charging\n");
//                            Console.WriteLine("6: Update parcel and drone connection\n");
//                            Console.WriteLine("7: Update deliver parcel by drone\n");
//                            Console.WriteLine("8: Update pick up parcel by drone\n");


//                            input = Console.ReadLine();
//                            Int32.TryParse(input, out inputVal2);
//                            switch (inputVal2)
//                            {
//                                case 1://Update drone model
//                                    {
//                                        Console.WriteLine("enter drone id and new model");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        model = Console.ReadLine();
//                                        try
//                                        {
//                                            bl.updateDrone(id, model);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 2://Update station detailes
//                                    {
//                                        Console.WriteLine("enter the station id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        Console.WriteLine("(enter 1 for changing name, 2 for changing number of charging slots, 3 for both)");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out num);
//                                        if (num == 1)
//                                        {
//                                            Console.WriteLine("enter name");
//                                            name = Console.ReadLine();
//                                            chargeSlots = 0;
//                                        }
//                                        else if (num == 2)
//                                        {
//                                            Console.WriteLine("enter number of charging slots");
//                                            input = Console.ReadLine();
//                                            Int32.TryParse(input, out chargeSlots);
//                                            name = null;
//                                        }
//                                        else
//                                        {
//                                            Console.WriteLine("enter name and number of charging slots");
//                                            name = Console.ReadLine();
//                                            input = Console.ReadLine();
//                                            Int32.TryParse(input, out chargeSlots);
//                                        }
//                                        try
//                                        {
//                                            bl.updateStation(id, name, chargeSlots);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 3://Update customer detailes
//                                    {
//                                        Console.WriteLine("enter the customer id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        Console.WriteLine("(enter 1 for changing name, 2 for changing phone number, 3 for both)");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out num);
//                                        if (num == 1)
//                                        {
//                                            Console.WriteLine("enter name");
//                                            name = Console.ReadLine();
//                                            phone = null;
//                                        }
//                                        else if (num == 2)
//                                        {
//                                            Console.WriteLine("enter phone number");
//                                            phone = Console.ReadLine();
//                                            name = null;
//                                        }
//                                        else
//                                        {
//                                            Console.WriteLine("enter name and phone number");
//                                            name = Console.ReadLine();
//                                            phone = Console.ReadLine();
//                                        }
//                                        try
//                                        {
//                                            bl.updateCustomer(id, name, phone);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 4://Update battery status: send drone to charge
//                                    {
//                                        Console.WriteLine("enter the drone id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            bl.sendDroneToCharge(id);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 5://Update battery status: release drone frome charging
//                                    {
//                                        Console.WriteLine("enter the drone id and the time it was charging");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        input = Console.ReadLine();
//                                        DateTime.TryParse(input, out time);
//                                        //time = Console.ReadLine().DT();
//                                        try
//                                        {
//                                            bl.releaseDroneFromCharge(id, time);//datetime
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 6://Update parcel and drone connection
//                                    {
//                                        Console.WriteLine("enter the drone id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            bl.matchParcelToDrone(id);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 7://Update deliver parcel by drone
//                                    {
//                                        Console.WriteLine("enter the drone id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            bl.deliverParcel(id);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 8://Update pick up parcel by drone
//                                    {
//                                        Console.WriteLine("enter the drone id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            bl.pickupParcel(id);
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                default:
//                                    Console.WriteLine("ERROR\n");
//                                    break;
//                            }
//                            break;
//                        }
//                    case 3://display
//                        {
//                            Console.WriteLine("Choose one of the following options:\n");
//                            Console.WriteLine("1: Display base-station\n");
//                            Console.WriteLine("2: Display drone\n");
//                            Console.WriteLine("3: Display customer\n");
//                            Console.WriteLine("4: Display parcel\n");
//                            input = Console.ReadLine();
//                            Int32.TryParse(input, out inputVal2);
//                            switch (inputVal2)
//                            {
//                                case 1://display base station
//                                    {
//                                        Console.WriteLine("enter the station id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            Console.WriteLine(bl.displayStation(id));
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }

//                                        break;
//                                    }
//                                case 2://display drone
//                                    {
//                                        Console.WriteLine("enter the drone id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            Console.WriteLine(bl.displayDrone(id));
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 3://display customer
//                                    {
//                                        Console.WriteLine("enter the customer id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            Console.WriteLine(bl.displayCustomer(id));
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 4://display parcel
//                                    {
//                                        Console.WriteLine("enter the parcel id");
//                                        input = Console.ReadLine();
//                                        Int32.TryParse(input, out id);
//                                        try
//                                        {
//                                            Console.WriteLine(bl.displayParcel(id));
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                default:
//                                    Console.WriteLine("ERROR\n");
//                                    break;
//                            }
//                            break;
//                        }
//                    case 4://display list
//                        {
//                            Console.WriteLine("Choose one of the following options:\n");
//                            Console.WriteLine("1: Display all base-stations\n");
//                            Console.WriteLine("2: Display all drones\n");
//                            Console.WriteLine("3: Display all customers\n");
//                            Console.WriteLine("4: Display all parcels\n");
//                            Console.WriteLine("5: Display all parcels not asigned to any drone\n");
//                            Console.WriteLine("6: Display all base-stations with available charging slots\n");
//                            input = Console.ReadLine();
//                            Int32.TryParse(input, out inputVal2);
//                            switch (inputVal2)
//                            {
//                                case 1://display all base stations
//                                    {
//                                        foreach (var item in bl.displayStationList())
//                                        {
//                                            Console.WriteLine(item);
//                                        }
//                                        break;
//                                    }
//                                case 2://display all drones
//                                    {
//                                        foreach (var item in bl.displayDroneList())
//                                        {
//                                            Console.WriteLine(item);
//                                        }
//                                        break;
//                                    }
//                                case 3://display all customers
//                                    {
//                                        try
//                                        {
//                                            foreach (var item in bl.displayCustomerList())
//                                            {
//                                                Console.WriteLine(item);
//                                            }
//                                        }
//                                        catch (Exception x)
//                                        {
//                                            Console.WriteLine(x.Message);
//                                        }
//                                        break;
//                                    }
//                                case 4://display all parcels
//                                    {
//                                        foreach (var item in bl.displayParcelList())
//                                        {
//                                            Console.WriteLine(item);
//                                        }
//                                        break;
//                                    }
//                                case 5://display all parcels not assigned to any drone
//                                    {
//                                        foreach (var item in bl.displayParcelListWithoutDrone())
//                                        {
//                                            Console.WriteLine(item);
//                                        }
//                                        break;
//                                    }
//                                case 6://display all base stations with available charging slots
//                                    {
//                                        foreach (var item in bl.displayStationListSlotsAvailable())
//                                        {
//                                            Console.WriteLine(item);
//                                        }
//                                        break;
//                                    }
//                                default:
//                                    Console.WriteLine("ERROR\n");
//                                    break;
//                            }
//                            break;
//                        }
//                    case 5://exit
//                        {
//                            Console.WriteLine("5: exit program\n");
//                            break;
//                        }
//                    default:
//                        break;
//                }

//            } while (inputVal1 != 5);
//        }
//    }
//    public static class Extensions
//    {
//        public static DateTime? DT(this string strLine)
//        {
//            DateTime result;
//            if (DateTime.TryParseExact(strLine, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out result))
//            {
//                return result;
//            }
//            else
//            {
//                return null;
//            }
//        }
//    }
//}

