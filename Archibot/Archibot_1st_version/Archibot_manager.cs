using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ArchibotNewVersion
{

   public class Archibot_manager
    {
       

        public static int value_index;
        // transalation en x par rapport au x de départ 
        public static int value_tx;
        // transalation en y par rapport au y départ
        public static int value_ty;
        public static int angle_orientation;
        public static StreamWriter file_orders;


        static void Main(string[] args)
        {



            file_orders = new StreamWriter(@"Orders.txt");
            Mapping  scan = new Mapping("je teste");
            scan.give_order(new Point(2, 2));
            

           /*set_data();
            scan.readInformation();
            scan.Test_crit();
            scan.choose_crit_point();
            //Array tab = new Array(path);
            //tab.set_array();
            //tab.Test_crit1();
            //tab.SaveCrits();
            //tab.give_order(new Point(2,2));
            //  Archibot_manager test = new Archibot_manager(path);
            // test.set_data();

            //   Array test = new Array (3, path);
            // test.give_order(new Point(2, 2));

            //test.set_array();
            //test.liste.Add(new Point(14, -1));
            // test.liste .Add(new Point(15, -4));
            //test.show();
            // test.Check(test.liste.ElementAt(0));
            //  test.Test_crit1();*/
            Console.ReadLine();
        }
       

        public static void   set_data()
        { 
        string chaine = "";
        int counter = 0;
         ///// fichier de stockage de données
            System.IO.StreamReader file_data = new System.IO.StreamReader(@"./data.txt");

            while ((chaine = file_data.ReadLine()) != null)
            {
                counter++;
                if (counter == 1)
                {
                    value_index = Convert.ToInt32(chaine) + 1;

                }
                else
                {

                    // On itère pour pouvoir accèder a la derniere position la  derniere position du robot 
                    value_tx = Convert.ToInt32(chaine.Split(',')[0]);
                    value_ty = Convert.ToInt32(chaine.Split(',')[1]);
                    angle_orientation = Convert.ToInt32(chaine.Split(',')[2]);
                }

            }
            Console.WriteLine("Iteration : " + value_index);
            Console.WriteLine("Tx  : " + value_tx);
            Console.WriteLine("Ty : " + value_ty);
            Console.WriteLine("Orientation angle : " + value_ty);
        
        }

        public static void Launch_Python(Mapping scan) { 
        
            //Before de let the Python programm running , we save data 
            Point.XmlPoint.SaveXmlFile1(scan.getListe());
            Point.XmlPoint.SaveXmlFile2(scan.getCritListe());
            //  launch python and quit the program and
        }

        
    }
}
