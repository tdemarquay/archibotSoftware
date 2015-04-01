using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace ArchibotNewVersion
{
  public  class Mapping
    {

          
        public string path;
        public double CLEANING_DISTANCE = 7.0;
        public int maxX;
        public int maxY;
        private HashSet<Point> liste;
        private HashSet<Point> Points_Crit;
        

        public Mapping( string path) {
            this.path=path;
             maxX = 0;
             maxY = 0;
            liste = Point.XmlPoint.LoadXmlFile1();
            Points_Crit = Point.XmlPoint.LoadXmlFile2();
        }

        public HashSet<Point> getListe()
        {
            return liste; 
        }
        public HashSet<Point> getCritListe()
        { return Points_Crit; }
        public int getMaxX()
        {
            return maxX;
        }
        public int getMaxY()
        {
            return maxY;
        }

        public void readInformation()

        {  
            System.IO.StreamReader file =new System.IO.StreamReader(path);
            string chaine = "";
          
            int i = 0;
          
            //double[] buffer = new Point[2];
            while((chaine = file.ReadLine())!=null )

            {   

                if (chaine.Length!=0)
                {
                    Console.WriteLine(chaine + "\n");
                     
                       // test_angles_distance(d,tmp1,angle,tmp2);
                    double d = Convert.ToDouble(chaine.Split(' ')[1].Replace(".", ",")) / 10;
                    
                    double angle = ConvertToRadian( Convert.ToDouble(chaine.Split(' ')[0].Replace(".", ","))) ;
           
                  if ( d > 0 && d <200)
                    {
                        
                        

                        int x = Convert.ToInt32(Math.Round(d * Math.Cos(angle), 0));
                        int y = Convert.ToInt32(Math.Round(d * Math.Sin(angle), 0));
                        Console.WriteLine("d :" + d);
                        Console.WriteLine("angle :" + angle);
                        Console.WriteLine("Valeur X :" + x);
                        Console.WriteLine("Valeur Y :" + y);
                         //Console.WriteLine(" Index :" + size * y + x);
                       
                        liste.Add(new Point(x, y));
                        if (Math.Abs(x) > maxX) maxX = Math.Abs(x);
                        if (Math.Abs(y) > maxY) maxY = Math.Abs(y);
                    }
                }
                
                i++;
            }
            //return size;
        }
        public void show()
        {

            for (int i = 0; i < liste.Count; i++)
            {
                Console.WriteLine(" Point ---->Valeur de X :"+ liste.ElementAt(i).getX() +"Valeur de Y :"+ liste.ElementAt(i).getY());

            }
        }

        public  void Test_crit()

        {
            for (int i = 0; i < liste.Count; i++)
            {
                Check(liste.ElementAt(i));
            
            }
        
        
        }
        private double ConvertToRadian(double angle)
        {
            return (Math.PI / 180) * angle;

        }
        public Boolean Check( Point point)
        {
            int x1; 
            int y1;
            int counter=0;

            Console.WriteLine("TEST-------->\n");
           Point  point1= new Point(0,0);
            Point point2= new Point(0,0);
            for (int i = 0; i < liste.Count; i++)
            {   x1=liste.ElementAt(i).getX();

                y1=liste.ElementAt(i).getY();

                
                //Console.WriteLine("test avec le  point de coordonnées X ="+x1 +" Y = "+y1+"\n");
                if (x1 != point.getX() || y1!= point.getY())
                {
                    //Console.WriteLine("Je viens de rentrer \n");
                    if ((x1 <= point.getX() + 1.5 && x1 >= point.getX() - 1.5) || (y1 <= point.getY() + 1.5 && y1 >= point.getY() - 1.5))
                    {
                        //Console.WriteLine("Je viens de rentrer2 \n");
                        double valeur1 = Math.Sqrt(Math.Pow(point.getX() - x1, 2) + Math.Pow(point.getY() - y1, 2));
                      //  double valeur 
                            if (Math.Sqrt(Math.Pow(point.getX() - x1,2)+Math.Pow(point.getY() - y1,2))  < Math.Sqrt(Math.Pow(point.getX() - point1.getX(),2)+Math.Pow(point.getY() - point1.getY(),2)))
                            {
                              //  Console.WriteLine("point numero1 trouvé  \n");
                                Point temp = new Point(point1.getX(), point1.getY());
                                point1.setX(x1);
                                point1.setY(y1);
                                if (Math.Sqrt(Math.Pow(point.getX() - temp.getX(),2)+Math.Pow(point.getY() - temp.getY(),2))  < Math.Sqrt(Math.Pow(point.getX() - point2.getX(),2)+Math.Pow(point.getY() - point2.getY(),2)))
                                {
                                  //  Console.WriteLine("point numero 2 trouvé \n");
                                    point2.setX(temp.getX());
                                    point2.setY(temp.getY());
                                }
                            }
                            else if (Math.Sqrt(Math.Pow(point.getX() - x1,2)+Math.Pow(point.getY() - y1,2))  < Math.Sqrt(Math.Pow(point.getX() - point2.getX(),2)+Math.Pow(point.getY() - point2.getY(),2)))
                            {
                                //Console.WriteLine("point numero 2 trouvé \n");
                                point2.setX(x1);
                                point2.setY(y1);
                            
                            }
                        
                        } 
                    
                    }          
                }            
            

            // Verification des points ( on teste si les deux point trouvés ne sont pas arronés )

            if( (point1.getX()==0 &&  point1.getY()==0) || (point2.getX()==0 && point2.getY()==0))
            {
                // point= point critique
                Console.WriteLine("Point critique1  x :"+point.getX()+"y :"+point.getY());
                Points_Crit.Add(point);
                return true;

            }
            else if((point.getX()==point1.getX() && (point.getY()!=point1.getY() &&  point.getX()!=point2.getX() && point.getY()==point2.getY())))
            {
            //Ceci est un coint  de 60°
                point.setmarque(true);
                return false;
            
            }
            else if((point.getX()==point2.getX() && (point.getY()!=point2.getY() &&  point.getX()!=point1.getX() && point.getY()==point1.getY())))
            {
            
            //Ceci est un autre coin de 60°
                point.setmarque(true);
                return false;
            }

            else
            {


                if (Math.Sqrt(Math.Pow(point1.getX() - point2.getX(), 2) + Math.Pow(point1.getY() - point2.getY(), 2)) < Math.Sqrt(Math.Pow(point.getX() - point2.getX(), 2) + Math.Pow(point.getY() - point2.getY(), 2)))
                {
                   // les deux points trouvés au tour du point testé sont du même coté  donc point critque 
                    // point= point critique
                    Console.WriteLine("Point critique2  x :" + point.getX() + "y :" + point.getY());
                    Points_Crit.Add(point);
                    return true;

                }
                else if (Math.Sqrt(Math.Pow(point1.getX() - point2.getX(), 2) + Math.Pow(point1.getY() - point2.getY(), 2)) < Math.Sqrt(Math.Pow(point.getX() - point1.getX(), 2) + Math.Pow(point.getY() - point1.getY(), 2)))
                {
                    // les deux points trouvés au tour du point testé sont du même coté  donc point critque 
                    // point= point critique
                    Console.WriteLine("Point critique3  x :" + point.getX() + "y :" + point.getY());
                    Points_Crit.Add(point);               
                    return true;
                
                }
                else{

                    // Point non critique car entouré de deux points adjacents
                    // On marque le point pour la catégoriser dans la catégorie des points non critique 
                    point.setmarque(true);
                    return false;

                }

           } 

            // Il faut que la distance entre le point testé et chacun des deux points trouvés  soit inférieur a la distance entre  les deux points trouvés

        }


        public Point choose_crit_point()
        {
            int x = Archibot_manager.value_tx;
            int y = Archibot_manager.value_ty;
            Point closer_point = Points_Crit.First();
            for (int i = 1; i < Points_Crit.Count; i++)
            { 
            if(Math.Sqrt(Math.Pow(x - Points_Crit.ElementAt(i).getX(), 2) + Math.Pow(y - Points_Crit.ElementAt(i).getY(), 2))<Math.Sqrt(Math.Pow(x - closer_point.getX(), 2) + Math.Pow(y - closer_point.getY(), 2)))
            {
                closer_point = Points_Crit.ElementAt(i);

            }
            }
            return closer_point;
        }
            ///Donne l'ordre au robot d'avancer d'uu certains angle et jusqu'a une certaine distance du point critique 

        public void give_order( Point point_to_go)
            {
                //Point point_to_go = choose_crit_point();
               int x= Archibot_manager.value_tx;
               int y = Archibot_manager.value_ty;
                
             // Position relative par rapport a la position actuelle 
               x = x - point_to_go.getX();
               y = y - point_to_go.getY();

               double angle = Math.Atan(y / x);
               angle = ((angle / Math.PI) * 180)-Archibot_manager.angle_orientation;
               if (angle < 0)
               {
                   angle = 360 + angle;
               }
            //On ecrit l'ordre
               Archibot_manager.file_orders.WriteLine( angle.ToString()+" 30");
               Archibot_manager.file_orders.Flush();
               Archibot_manager.file_orders.Close();
               //Console.WriteLine("Job done ");
            }
        



          
            
            
            
            }
    
        }



        
        
        /*public Boolean Check_2(Point point)
        {



        }*/

       

        /*void updateArray(int newSize)
        {
            BitArray tab_data2 = new BitArray(newSize * newSize);  

            for(int i=0; i<tab_data.Length;i++)
            {
                tab_data2[i] = tab_data[i];
            }
            tab_data = null;
            tab_data = tab_data2;
        }*/

       /* public void setXY(int x, int y, Boolean boo = true)
        {
            
            Boolean change = false;
            while((x+div)>=(size-10) || (y+div) >= (size-10))
            {
                size = size + 100;
                div = Convert.ToInt32(size / 2);
                change = true;
            }
            if(change)updateArray(size);

            tab_data[size * (y+div) + (x+div)] = boo;
        }*/

       /* public Boolean getXY(int x, int y)
        {
            int div = Convert.ToInt32(size / 2);
            return tab_data[size * (y) + (x)];
        }

        public int getY(int index)
        {
            int test = index % size;
            int test2 = index / size;
            return test2;
        }

        public int getX(int index)
        {
            return index - getY(index) * size; ;
        }

        public void print_to_txt()
        {
            string[] chaine= new string[size];
            int counter=0;
                
                for (int i = 0;i<tab_data.Length;i=i+size )
                {
                    
                    for(int j=0;j<size;j++)
                    {
                    if (tab_data[j+i] == true)
                    {

                        chaine[counter] =chaine[counter]+"X";
                    }
                    else
                    {
                        chaine[counter] =chaine[counter]+" ";
                    }
                  }
                    chaine[counter]=chaine[counter];
                    counter++;
                }
        System.IO.File.WriteAllLines(@"test2.txt", chaine);
            }


            public void clean()
            {

                for(int i=0;i<tab_data.Length;i++)
                {
                    int delete =0 ;
                    if(tab_data[i])
                    {
                        for (int j = 0; j < tab_data.Length; j++)
                        {
                            if(i!=j && tab_data[j])
                            {
                                int x1 = getX(i);
                                int y1 = getY(i);
                                int x2 = getX(j);
                                int y2 = getY(j);

                                int sousX = Math.Abs(x2 - x1);
                                int sousY = Math.Abs(y2 - y1);
                                double sousXCarre = Math.Pow(sousX, 2);
                                double sousYCarre = Math.Pow(sousY, 2);

                                double dist = Math.Sqrt(sousXCarre + sousYCarre);

                                if (dist < CLEANING_DISTANCE) delete ++;
                            }
                        }

                        
                    }
                    if (delete<3) tab_data[i] = false;
                }
            }*/
       /* public bool test_angles_distance(double distance1, double distance2, double angle1, double angle2)
        {
            Console.WriteLine(" rajout ?");
            if(Math.Abs(distance1-distance2)>50 && Math.Abs(angle1-angle2)>5 )
            {
                Angles_Crit.Add(distance1);
                Angles_Crit.Add(distance2);
                Console.WriteLine("Angle rajouté");
            return true;
            }
            return false;
        }*/
        /*public void print_crit()
        {
            for (int i = 0; i < Angles_Crit.Count; i++)
            {
                Console.WriteLine(" Point Critique " + i);
                Console.WriteLine(Angles_Crit.ElementAt(i));
            }

        }*/
  
        
        
    

