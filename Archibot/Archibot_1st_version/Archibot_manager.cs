using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ArchibotNewVersion
{

   public class Archibot_manager
    {
       

        public static int value_index;
        // transalation en x par rapport au x de départ 
        public static int value_tx;
        // transalation en y par rapport au y départ
        public static int value_ty;
       public static double angle_orientation;
        public static StreamWriter file_orders;


        static void Main(string[] args)
        {



            file_orders = new StreamWriter(@"Orders.txt");
            Mapping  scan = new Mapping(args[0]);


            //Console.WriteLine(Math.Atan2(9,8));
           set_data();
            scan.readInformation();
            scan.Test_crit();
            
           scan.give_order();
            scan.update_data();
           // scan.update_data(); 
            
          
           Launch_Python(scan);
        }
       

        public static void   set_data()
        { 
        string chaine = "";
        int counter = 0;
         ///// fichier de stockage de données
            System.IO.StreamReader file_data = new System.IO.StreamReader(@"./data2.txt");

            while ((chaine = file_data.ReadLine()) != null)
            {
                counter++;
                if (counter == 1)
                {
                    value_index = Convert.ToInt32(chaine) ;

                }
                else
                {

                    // On itère pour pouvoir accèder a la derniere position la  derniere position du robot 
                    value_tx = Convert.ToInt32(chaine.Split('.')[0]);
                    value_ty = Convert.ToInt32(chaine.Split('.')[1]);
                   
                }
                
            }
            Console.WriteLine("Iteration : " + value_index);
            Console.WriteLine("Tx  : " + value_tx);
            Console.WriteLine("Ty : " + value_ty);
       
            file_data.Close();
        }

        public static void Launch_Python(Mapping scan) { 
        
            //Before de let the Python programm running , we save data 
            Point.XmlPoint.SaveXmlFile1(scan.getListe());
            Point.XmlPoint.SaveXmlFile2(scan.getCritListe());
            //  launch python and quit the program and

             Process process = new Process();
             process.StartInfo.FileName = "Ccall";
             process.StartInfo.Arguments = @"./Orders.txt";
             process.Start();
           
            Process.GetCurrentProcess().Kill();
        }

        
    }
}
