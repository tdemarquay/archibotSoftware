using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archibot_1st_version
{
    class Droite
    {
        private int x1, y1, x2, y2;
        private double a, b;
        private double distance;
        private bool a0;
        public List<Point> groupe;

        public Droite(int x1, int y1, int x2, int y2)
        {
            groupe = new List<Point>();
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
            a0 = false;
            distance = Math.Sqrt(Math.Pow(x2-x1,2)+Math.Pow(y2-y1,2));
            if ((Convert.ToDouble(x1) - Convert.ToDouble(x2)) == 0.0)
                a0 = true;
            else a = (Convert.ToDouble(y1) - Convert.ToDouble(y2)) / (Convert.ToDouble(x1) - Convert.ToDouble(x2));
            b = Convert.ToDouble(y1) - a * Convert.ToDouble(x1);
            int btemp = Convert.ToInt32(b);
            int btemp2 = Convert.ToInt32((Convert.ToDouble(y2) - a * Convert.ToDouble(x2)));



            if ((btemp2 != btemp && btemp2 != btemp-1 && btemp2 != btemp+1)  && a0==false)
                Console.WriteLine("Erreur dans le calcul du b de la droite");
        }

        public void setX1(int x)
        {
            x1 = x;
        }

        public void setX2(int x)
        {
            x2 = x;
        }

        public void setY1(int y)
        {
            y1 = y;
        }

        public void setY2(int y)
        {
            y2 = y;
        }

        public void setA(double a)
        {
            this.a = a;
        }

        public void setB(double b)
        {
            this.b = b;
        }

        /*public void setDistance(double dist)
        {
            distance = dist;
        }*/

        public int getX1()
        {
            return x1;
        }

        public int getX2()
        {
            return x2;
        }

        public int getY1()
        {
            return y1;
        }

        public int getY2()
        {
            return y2;
        }

        public double getA()
        {
            return a;
        }

        public double getB()
        {
            return b;
        }

        public double getDistance()
        {
            return distance;
        }

        public bool hasA0()
        {
            return a0;
        }

        public bool isEqual(Droite droit)
        {
            if (droit.getX1() == x1 && droit.getX2() == x2 && droit.getY1() == y1 && droit.getY2() == y2)
                return true;
            else return false;
        }

        public void moindreCarre()
        {
            double xbarre = 0.0, ybarre = 0.0;

            if(groupe.Count==0) return;

            foreach (Point point in groupe)
            {
                xbarre = xbarre + point.getX();
                ybarre = ybarre + point.getY();
            }

            double n = Convert.ToDouble(groupe.Count);

            xbarre = (1 / n)* xbarre;
            ybarre = (1 / n) * ybarre;

            double varX = 0.0, varY = 0.0;
            foreach (Point point in groupe)
            {
                varX = varX + Math.Pow((point.getX()-xbarre),2);
                varY = varY + Math.Pow((point.getY() - ybarre), 2);
            }

            varX = (1 / n) * varX;
            varY = (1 / n) * varY;


            double cov = 0.0;
            foreach (Point point in groupe)
            {
                cov = cov + ((point.getX() - xbarre)*(point.getY() - ybarre));
            }

            cov = (1 / n) * cov;

            a= cov / varX;
            b = ybarre - a * xbarre;
        }

        public void updateDistance()
        {
            distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }



}
