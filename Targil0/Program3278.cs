using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3278();
            Welcome3904();
            Console.ReadKey();
        }
        static partial void Welcome3904();
        private static void Welcome3278()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my FirstOrDefault console application", userName);
        }
    }
}
