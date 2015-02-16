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
    }
}
