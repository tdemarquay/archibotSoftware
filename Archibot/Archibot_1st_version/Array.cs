using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Archibot_1st_version
{
    class Array
    {

        public BitArray [] tab_data;    
        public int size;

        public Array(int size) {
            this.size = size;
            tab_data = new BitArray[size];
            
            for(int i=0;i<size;i++)
            {
                tab_data[i] = new BitArray(13);
                    
            }
            set_array();
            print_to_txt();
         
        }   

        public void set_array()

        {
            System.IO.StreamReader file =new System.IO.StreamReader(@"C:\Users\CX640DX\Desktop\test.txt");
            string chaine="";
            while((chaine = file.ReadLine())!=null )

            {
                if (chaine.Split('/').Length != 1)
                {
                    Console.WriteLine(chaine + "\n");
                    double d = Convert.ToDouble(chaine.Split('/')[1]);
                    double angle = Convert.ToDouble(chaine.Split('/')[0]) * Math.PI / 180;
                    int  x =(int)Math.Round ((d * Math.Cos(angle)));

                    int y =(int) Math.Round((d * Math.Sin(angle))); 
                    Console.WriteLine("Valeur X :" + x);
                    Console.WriteLine("Valeur Y :" + y);
                    tab_data[x+6].Set(y+6,true);
                    
                   
                }
            }
           

            
        
                
        
        }

        public void print_to_txt()
        {
            string[] chaine= new string[13];
            for(int j=0;j<tab_data.Length;j++)
            {
                chaine[j]=null;
                for (int i = 0;i<tab_data[j].Length;i++ )
                {
                    if (tab_data[j].Get(i) == true)
                    {
                        chaine[j] =chaine[j]+"X";
                    }
                    else
                    {
                        chaine[j] =chaine[j]+" ";
                    }

                }

            }
            
           System.IO.File.WriteAllLines(@"C:\Users\CX640DX\Desktop\test2.txt", chaine);
        }

        
    }
}
