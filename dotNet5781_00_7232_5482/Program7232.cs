using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_7232_5482
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome7232();
            Welcome5482();
            Console.ReadKey();

        }
        static  partial void Welcome5482();
        private static void Welcome7232()
        {
            Console.Write("Enter your name: ");
            string str;
            str = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", str);
        }
    }
}
