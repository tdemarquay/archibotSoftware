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
                path = Console.ReadLine();
            }
                
            else
            {
                Archibot_manager test = new Archibot_manager(13, args[0]);

                Console.ReadLine();
            }
            

        }
    }
}
