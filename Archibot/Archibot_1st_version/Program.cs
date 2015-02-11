using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archibot_1st_version
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            if (args.Length == 0)
            {
                Console.WriteLine("Error : no entry file");
                Console.Write("Write the path to the file : ");
                path = Console.ReadLine();
            }
                
            else
            {
                path = args[0];
            }

            Archibot_manager test = new Archibot_manager(500, path);
            Console.ReadLine();

        }
    }
}
