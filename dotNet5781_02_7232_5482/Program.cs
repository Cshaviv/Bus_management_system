using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{
    class Program
    {
        static void Main(string[] args)
        {
            initialize();
            CHOICE choice;
            do
            {
                Console.WriteLine("Make your mind:");
                Console.WriteLine("ADD,DELETE,FIND,PRINT,EXIT= -1");
                bool success = Enum.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case CHOICE.ADD:
                        break;
                    case CHOICE.DELETE:
                        break;
                    case CHOICE.FIND:
                        break;
                    case CHOICE.PRINT:
                        break;
                    case CHOICE.EXIT:
                        break;
                    default:
                        break;
                }

            } while (choice != CHOICE.EXIT);
        }

        private static void initialize()
        {
            //TODO
        }
    }
    }

