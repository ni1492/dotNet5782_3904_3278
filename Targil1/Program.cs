using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int inputVal1=0;
            int inputVal2 = 0;
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
                                case 2:
                                case 3:
                                case 4:
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
                                case 2:
                                case 3:
                                case 4:
                                case 5:
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
                                case 2:
                                case 3:
                                case 4:
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
                                case 2:
                                case 3:
                                case 4:
                                case 5:
                                case 6:
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
