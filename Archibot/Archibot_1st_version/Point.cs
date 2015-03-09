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

        public static double calculateDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(2, x2 - x1) + Math.Pow(2, y2 - y1));
        }
    }
}
