using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Archibot_1st_version
{
    class Array
    {

        public BitArray tab_data;    
        public int size;
        public string path;

        public Array(int size, string path) {
            this.size = size;
            tab_data = new BitArray(size * size);        
            this.path=path;
         
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


                    Console.WriteLine(" Index :" + (13 * (6-y) + 6+x));
                    tab_data[(13 * (6 - y) + 6 + x)] = true;

                }
            }

        }

        public void print_to_txt()
        {
            string[] chaine= new string[13];
            int counter=0;
                
                for (int i = 0;i<tab_data.Length;i+=13 )
                {
                    
                    for(int j=0;j<13;j++)
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
        System.IO.File.WriteAllLines(@"C:\Users\CX640DX\Desktop\test2.txt", chaine);
            }
            
           
        }

        
    }

