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
        public Array final_table;
        public int size;

        public Generated_image(ref Array table, int size)
        {
            final_table = table;
            this.size = size;

        }

        public void generate()
        {
            long memory = GC.GetTotalMemory(true);

            var b = new Bitmap(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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
            for (int j = 0; j < size; j++)//ligne
            {
                for (int i = 0; i < size; i++)//Colonnes
                {
                    if (final_table.getXY(i, j))
                    {
                        g.DrawLine(myPen, j, i, j , i+1);
                        //Console.WriteLine("test" + j + "x2=" + x2 + "y2=" + y2);
                    }
                }


            }
            memory = GC.GetTotalMemory(true);
            b.Save("test.bmp");
            memory = GC.GetTotalMemory(true);

        }
        



    }
}
