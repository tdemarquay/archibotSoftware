using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
using System.IO;


namespace Archibot_1st_version
{


    class Generated_image
    {
        /*public int PROP = 15;//nombre dep oints sur le segment pour qua le segment soit ajouté
        public bool DEBUG = true;
        public double DISTMAX = 0; //Distance max du point au segment pour le calcul des proportions
        public double DISTDROITEMIN = 20;//Ignoré les segments plus courts
        public double COEFEQUATION = 0.1; //Coef max pour la tolérance dans l'équation de la droite*/
        public int MULTIPIXEL = 1;

        public int size;
        public Array array;
        //private List<Droite> droites;
        //private StreamWriter file;
        private List<Droite> droites;
        private List<Point> tmpPoint;

        public Generated_image(Array array, int size)
        {
            droites = new List<Droite>();
            this.array = array;
            this.size = size;
            //droites = new List<Droite>();
            tmpPoint = new List<Point>();
            //file = new StreamWriter(@"C:\Users\Thibault\WriteLines2.txt");

            foreach(Point point in array.getListe())
            {
                tmpPoint.Add(point);
            }
            

        }

        public void triDuPlusGrandAuPlusPetit()
        {
            bool end=false;
            while(end==false)
            {

            }
        }


        public void generate()
        {

            Bitmap bitmap = new Bitmap(array.getMaxX() * 2 * MULTIPIXEL, array.getMaxY() * 2 * MULTIPIXEL);
            Console.WriteLine(array.getMaxX() * 2  + "," + array.getMaxY() * 2 );
            for (int i = 0; i < array.getMaxX() * 2 * MULTIPIXEL; i++)
            {
                for (int j = 0; j < array.getMaxY() * 2 * MULTIPIXEL; j++)
                {
                    //Set8bppIndexedPixel(b, i, j, Color.Black);
                    bitmap.SetPixel(i, j, Color.White);
                }
            }


            bool end = false;
            int k = 0;
            Point point1;
            Point point2;
            Droite droitetmp;
            double a, b;
            int xtmp, ytmp;
            List<Point> remove = new List<Point>();
            while(end==false && tmpPoint.Count != 1)//Boucle, tant que pas fini on parcourt droitetmp
            {
                point1 = tmpPoint[k];//On récupère un permier point et un deuxième
                point2 = tmpPoint[k+1];
                droitetmp = new Droite(point1.getX(), point1.getY(), point2.getX(), point2.getY());//On créé une droite avec ces 2 points
                remove.Clear();//On vide la lsite remove
                
                foreach(Point point in tmpPoint)//On parcourt tou sles points
                {
                    if (point.isEqual(point1) || point.isEqual(point2))
                    {
                       // break;
                    }
                   

                    a = droitetmp.getA();//On récupère le a et b de l'qéquation y=ax+b
                    b = droitetmp.getB();

                    xtmp = point.getX();//On récupère le x et y 
                    ytmp = point.getY();
                    //file.WriteLine("A(" + droitetmp.getX1() + "," + droitetmp.getY1() + ") " + "B(" + droitetmp.getX2() + "," + droitetmp.getY2() + ") a = " + droitetmp.getA() + ", b = " + droitetmp.getB() + " P(" + xtmp + "," + ytmp + ")");
                    
                    //On vérifie si ce points vérifie l'quation de la droite en cours. Si oui on l'ajoute à la list remove pour qu'il soit supprimé de droitetmp. Et on l'ajoute au groupe de la droite
                    if((droitetmp.hasA0() && Math.Abs(xtmp - droitetmp.getX1()) < 5)
                        || (!droitetmp.hasA0() && Math.Abs(a * xtmp + b - ytmp) < 5))
                    {
                        //file.WriteLine("Yes");
                        remove.Add(point);
                        droitetmp.groupe.Add(point);
                    }
                }


                
                //On ajoute la droite seulement si le groupe a plus de 3 pointss
                if (droitetmp.groupe.Count > 9)
                {
                    droites.Add(droitetmp);
                    
                }

                //On supprimer
                remove.Add(point1);
                remove.Add(point2);
                if (remove.Count != 0)
                {
                    foreach (Point point in remove)
                    {
                        tmpPoint.Remove(point);
                    }
                }
                k++;
                if ( tmpPoint.Count == 0) end = true;
                else if (k >= (tmpPoint.Count - 1)) k = 0;
            }

            
            foreach(Droite droite in droites)
            {
                droite.moindreCarre();
            }
            cleanDroites2();

            foreach (Droite droite in droites)
            {
                droite.updateDistance();
            }
            
            removeSmallDroites();
            //droiteInFile();
           // file.Flush();
            //file.Close();
            long memory = GC.GetTotalMemory(true);
            Graphics g = Graphics.FromImage(bitmap);
            RectangleF rectf = new RectangleF(120, 10, 200, 50);
            //g.DrawString("2D Map", new Font("Tahoma", 14), Brushes.Black, rectf);
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            myPen.Width = MULTIPIXEL;
            memory = GC.GetTotalMemory(true);
            Console.WriteLine(memory);
            drawLines(g, myPen);
            drawDistance(g, myPen);
            bitmap.Save("test2.bmp");
            memory = GC.GetTotalMemory(true);
        }

        public void drawDistance(Graphics g, System.Drawing.Pen myPen)
        {
            int midX, midY;
            Font drawFont = new Font("Arial", 5*MULTIPIXEL);
            int maxX = Convert.ToInt32(array.getMaxX());
            int maxY = Convert.ToInt32(array.getMaxY());
            int distance;
            foreach (Droite droit in droites)
            {
                midX = Convert.ToInt32((droit.getX1() + droit.getX2()) / 2);
                midY = Convert.ToInt32((droit.getY1() + droit.getY2()) / 2);

                distance = Convert.ToInt32(Math.Round(droit.getDistance()));
                RectangleF rectf = new RectangleF((midX + maxX - 15) * MULTIPIXEL, (midY  + maxY) * MULTIPIXEL + 10, 30 * MULTIPIXEL, 8 * MULTIPIXEL);
                g.DrawString(distance+ " cm", drawFont, Brushes.Black, rectf);
            }
        }

        public void cleanDroites()
        {
            double disMax = 0;
            Point A, B;
            foreach(Droite droite in droites)
            {
                A = null;
                B = null;
                disMax = 0;

                foreach(Point point1 in droite.groupe)
                {
                    foreach (Point point2 in droite.groupe)
                    {
                        if(point1.distanceTo(point2) > disMax)
                        {
                            disMax = point1.distanceTo(point2);
                            A = point1;
                            B = point2;
                        }
                    }
                }
                droite.setX1(A.getX());
                droite.setY1(A.getY());
                droite.setX2(B.getX());
                droite.setY2(B.getY());
            }
        }


        public void cleanDroites2()
        {
            foreach (Droite droite in droites)
            {
                int minX = int.MaxValue, maxX = int.MinValue;

                foreach (Point point1 in droite.groupe)
                {
                    if(point1.getX()<minX)
                    {
                        minX = point1.getX();
                    }
                    if (point1.getX() > maxX)
                    {
                        maxX = point1.getX();
                    }
                }
                droite.setX1(minX);
                droite.setY1(Convert.ToInt32(droite.getA() * minX + droite.getB()));
                droite.setX2(maxX);
                droite.setY2(Convert.ToInt32(droite.getA() * maxX + droite.getB()));
            }
        }

        public void removeSmallDroites()
        {
            int k = 0;
            while (k != droites.Count)
            {
                if(droites[k].getDistance() < 40)
                {
                    droites.Remove(droites[k]);
                }
                k = k + 1;
            }
        }

        public void droiteInFile()
        {

            foreach (Droite droite in droites)
            {
                //file.WriteLine("A(" + droite.getX1() + "," + droite.getY1() + ") " + "B(" + droite.getX2() + "," + droite.getY2() + ") a = " + droite.getA() + ", b = " + droite.getB());

                foreach(Point point in droite.groupe)
                {
                    //file.WriteLine("A(" + point.getX() + "," + point.getY() + ") ");
                }
            }
                
        }

        public void drawLines(Graphics g, System.Drawing.Pen myPen)
        {
            foreach (Droite droit in droites)
            {
                int maxX = Convert.ToInt32(array.getMaxX());
                int maxY = Convert.ToInt32(array.getMaxY());
                g.DrawLine(myPen, (droit.getX1() + maxX) * MULTIPIXEL, (droit.getY1() + maxY) * MULTIPIXEL , (droit.getX2() + maxX) * MULTIPIXEL , (droit.getY2() + maxY + 1) * MULTIPIXEL );
                //Console.WriteLine("test" + j + "x2=" + x2 + "y2=" + y2);




            }
        }

    }
}
