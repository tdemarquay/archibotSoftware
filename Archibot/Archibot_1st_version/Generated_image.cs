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

        public Generated_image(Array array, int size)
        {
            this.array = array;
            this.size = size;

        }

        public void generate()
        {
            long memory = GC.GetTotalMemory(true);

            var b = new Bitmap(array.getMaxX()*2,array.getMaxY()*2);
            for (int i = 0; i < array.getMaxX()*2; i++)
            {
                for (int j = 0; j < array.getMaxY()*2; j++)
                {
                    b.SetPixel(i, j, Color.White);
                }
            }

            Graphics g = Graphics.FromImage(b);
            RectangleF rectf = new RectangleF(120, 10, 200, 50);
            g.DrawString("2D Map", new Font("Tahoma", 14), Brushes.Black, rectf);
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            memory = GC.GetTotalMemory(true);
            Console.WriteLine(memory);
            foreach (Point lis in array.getListe())
            {
                int maxX = Convert.ToInt32(array.getMaxX() );
                int maxY = Convert.ToInt32(array.getMaxY() );
                g.DrawLine(myPen, lis.getX() + maxX, lis.getY() + maxY, lis.getX() + maxX , lis.getY() + maxY + 1);
                        //Console.WriteLine("test" + j + "x2=" + x2 + "y2=" + y2);
                    
                


            }
            memory = GC.GetTotalMemory(true);
            b.Save("test.bmp");
            memory = GC.GetTotalMemory(true);

        }
        



    }
}
