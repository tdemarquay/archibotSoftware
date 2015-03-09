using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections;
//using System.Drawing;


namespace Archibot_1st_version
{


    class Generated_image
    {

        // public  Bitmap image;
        public int size;
        public Array array;
        private List<Droite> droites;

        public Generated_image(Array array, int size)
        {
            this.array = array;
            this.size = size;
            droites = new List<Droite>();

        }

        public void generate()
        {
            long memory = GC.GetTotalMemory(true);

            var b = new Bitmap(array.getMaxX() * 2, array.getMaxY() * 2);
            for (int i = 0; i < array.getMaxX() * 2; i++)
            {
                for (int j = 0; j < array.getMaxY() * 2; j++)
                {
                    b.SetPixel(i, j, Color.White);
                }
            }
            generateDroites();
            Graphics g = Graphics.FromImage(b);
            RectangleF rectf = new RectangleF(120, 10, 200, 50);
            g.DrawString("2D Map", new Font("Tahoma", 14), Brushes.Black, rectf);
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            memory = GC.GetTotalMemory(true);
            Console.WriteLine(memory);
            drawLines(g, myPen);
            memory = GC.GetTotalMemory(true);
            b.Save("test.bmp");
            memory = GC.GetTotalMemory(true);

        }

        public void generateDroites()
        {
            Droite tmp;
            foreach (Point lis in array.getListe())
            {
                foreach (Point lis2 in array.getListe())
                {
                    tmp = new Droite(lis.getX(), lis.getY(), lis2.getX(), lis2.getY());
                    double proportion = calculProportion(tmp);
                    if (proportion < 0.00000000001 && proportion != 0.0)
                        droites.Add(tmp);
                }
            }
        }

        public double calculProportion(Droite droite)
        {
            int nbre = 1;
            double dist;
            double num, den;

            if (droite.hasA0())
                return 0;
            foreach (Point lis in array.getListe())
            {
                num = Math.Abs(droite.getA() * Convert.ToDouble(lis.getX()) - Convert.ToDouble(lis.getY()) + droite.getB());
                den = Math.Sqrt(Math.Pow(droite.getA(), 2) + 1);
                dist = num / den;
                dist = FindDistanceToSegment(lis, droite);

                if (dist == 0.0)
                {
                    if (lis.getX() != droite.getX1() && lis.getY() != droite.getY1())
                    {
                        if (lis.getX() != droite.getX2() && lis.getY() != droite.getY2())
                        {
                            nbre++;
                        }
                    }
                }

            }
            dist = droite.getDistance();
            return dist / Convert.ToDouble(nbre);
        }

        public void drawPoints(Graphics g, System.Drawing.Pen myPen)
        {
            foreach (Point lis in array.getListe())
            {
                int maxX = Convert.ToInt32(array.getMaxX());
                int maxY = Convert.ToInt32(array.getMaxY());
                g.DrawLine(myPen, lis.getX() + maxX, lis.getY() + maxY, lis.getX() + maxX, lis.getY() + maxY + 1);
                //Console.WriteLine("test" + j + "x2=" + x2 + "y2=" + y2);




            }
        }

        public void drawLines(Graphics g, System.Drawing.Pen myPen)
        {
            foreach (Droite droit in droites)
            {
                int maxX = Convert.ToInt32(array.getMaxX());
                int maxY = Convert.ToInt32(array.getMaxY());
                g.DrawLine(myPen, droit.getX1() + maxX, droit.getY1() + maxY, droit.getX2() + maxX, droit.getY2() + maxY + 1);
                //Console.WriteLine("test" + j + "x2=" + x2 + "y2=" + y2);




            }
        }

        // Calculate the distance between
        // point pt and the segment p1 --> p2.
        private double FindDistanceToSegment(Point pt2, Droite droite)
        {
            Point closest;
            PointF pt = new PointF((float)(pt2.getX()), (float)(pt2.getY()));
            PointF p1 = new PointF((float)(droite.getX1()), (float)(droite.getY1()));
            PointF p2 = new PointF((float)(droite.getX2()), (float)(droite.getY2()));

            double dx = Convert.ToDouble(droite.getX2()) - Convert.ToDouble(droite.getX1());
            double dy = Convert.ToDouble(droite.getY2()) - Convert.ToDouble(droite.getY1());
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                //closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            double t = ((Convert.ToDouble(pt2.getX()) - Convert.ToDouble(droite.getX1())) * dx + (Convert.ToDouble(pt2.getY()) - Convert.ToDouble(droite.getY1())) * dy) /
                (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new Point(droite.getX1(), droite.getY1());
                dx = Convert.ToDouble(pt2.getX()) - Convert.ToDouble(droite.getX1());
                dy = Convert.ToDouble(pt2.getY()) - Convert.ToDouble(droite.getY1());
            }
            else if (t > 1)
            {
                closest = new Point(droite.getX2(), droite.getY2());
                dx = Convert.ToDouble(pt2.getX()) - Convert.ToDouble(droite.getX2());
                dy = Convert.ToDouble(pt2.getY()) - Convert.ToDouble(droite.getY2());
            }
            else
            {
                closest = new Point(droite.getX1() + Convert.ToInt32(t * dx), droite.getY1() + Convert.ToInt32(t * dy));
                dx = Convert.ToDouble(pt2.getX()) - Convert.ToDouble(closest.getX());
                dy = Convert.ToDouble(pt2.getY()) - Convert.ToDouble(closest.getY());
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
