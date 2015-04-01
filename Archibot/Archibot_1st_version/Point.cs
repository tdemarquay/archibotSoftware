using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archibot_1st_version
{
    class Point
    {
        private int x;
        private int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }


        public bool isEqual(Point point)
        {
            if (x == point.getX() && y == point.getY()) return true;
            else return false;
        }

        public static double calculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(2, x2 - x1) + Math.Pow(2, y2 - y1));
        }

        public double distanceTo(Point point)
        {
            return Math.Sqrt(Math.Pow(point.getX() - x, 2) + Math.Pow(point.getY() - y, 2));
        }
    }
}
