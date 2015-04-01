using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace ArchibotNewVersion
{
    public class Point
    {
        public int x{ get; set; }
        public int y{ get; set; }
        public Boolean marque { get; set; }
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.marque=false;
        }
        public Point() { }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
        public void  setX(int x)
        {
        this.x=x;
        
        }
        public void setY(int y)
        {

            this.y = y;

        }
        public void setmarque(Boolean value)
        {

            this.marque = value;
        }

        public override bool Equals(object obj)
        {
           Point Item = (Point) obj ;
           if (x <= Item.getX() + 0.5 && x >= Item.getX() - 0.5)
           {
               if (y <= Item.getY() + 0.5 && y >= Item.getY() - 0.5)
               {
                   return true;
               }

           }
           else { return false; }
           return false;
            
        }
        public override int GetHashCode()
        {
            // Which is preferred?
            int hash = 7;
            hash = 43 * hash + this.x;
            hash = 43 * hash + this.y;
            return hash;

    
            //return this.FooId.GetHashCode();
        }

    // Class for serialization
        public class XmlPoint
        {
            // New instance of XmlSerializer which will allow to serialize HashSet<Point> objects
            public static XmlSerializer xs = new XmlSerializer(typeof(HashSet<Point>));
            
            
            // Function to Load a list of Points from a Xml File
            public static HashSet<Point> LoadXmlFile1()
            {
                // We initialize the name and the path of the Xml File
                String filename = Environment.CurrentDirectory + @"\Points.xml";
                try
                {
                    // We access to the Xml File just with permissions to read
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        // We deserialize the file to have the List of Students
                        return xs.Deserialize(sr) as HashSet<Point>;
                    }
                }
                catch (IOException e)
                {
                    
                    //Console.WriteLine(e.ToString());
                }
                catch
                {
                    // If the file did not exist, we create a new and empty list of Points
                    
                    return new HashSet<Point>();
                }
                // If the file did not exist, we create a new and empty list of Points
                //Console.WriteLine("Autre");
               return new HashSet<Point>();
            }
            //Function to Load the list of critial points into a XML file
            public static HashSet<Point> LoadXmlFile2()
            {
                // We initialize the name and the path of the Xml File
                String filename = Environment.CurrentDirectory + @"\critPoints.xml";
                try
                {
                    // We access to the Xml File just with permissions to read
                    using (StreamReader sr = new StreamReader(filename))
                    {
                        // We deserialize the file to have the List of Points
                        return xs.Deserialize(sr) as HashSet<Point>;
                    }
                }
                catch (IOException e)
                {
                   // Console.WriteLine(e.ToString());
                }
                catch
                {
                    // If the file did not exist, we create a new and empty list of students
                   return new HashSet<Point>();
                }
                // If the file did not exist, we create a new and empty list of students
               return new HashSet<Point>();
            }

            // Function to Save a list of Points into a Xml File
            public static void SaveXmlFile1(HashSet<Point> liste )
            {
                // We initialize the name and the path of the Xml File
                String filename = Environment.CurrentDirectory + @"\Points.xml";
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    // We serialize the list to the Xml format and into the file
                    xs.Serialize(sw, liste);
                }
            }
            //Function  to save the list of critial points into a Xml File 
               public static void SaveXmlFile2(HashSet<Point> liste )
            {
                // We initialize the name and the path of the Xml File
                String filename = Environment.CurrentDirectory + @"\critPoints.xml";
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    // We serialize the list to the Xml format and into the file
                    xs.Serialize(sw, liste);
                }
            }
        }


    }


    }


