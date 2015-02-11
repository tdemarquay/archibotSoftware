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

        public int set_array()

        {
            System.IO.StreamReader file =new System.IO.StreamReader(path);
            string chaine = "";
            
            int i = 0;
            while((chaine = file.ReadLine())!=null )

            {
                if (chaine.Length!=0)
                {
                    Console.WriteLine(chaine + "\n");
                    double d = Convert.ToDouble(chaine.Split(' ')[1].Replace(".", ",")) / 10;
                    double angle = Convert.ToDouble(chaine.Split(' ')[0].Replace(".", ",")) * Math.PI / 180;
                    int x = Convert.ToInt32(Math.Round(d * Math.Cos(angle), 0));

                    int y = Convert.ToInt32(Math.Round(d * Math.Sin(angle), 0));
                    Console.WriteLine("Valeur X :" + x);
                    Console.WriteLine("Valeur Y :" + y);


                    Console.WriteLine(" Index :" + size * y + x);
                    setXY(x, y);

                }
                i++;
            }
            return size;
        }

        void updateArray()
        {
            BitArray tab_data2 = new BitArray(size * size);  

            for(int i=0; i<tab_data.Length;i++)
            {
                tab_data2[i] = tab_data[i];
            }
            tab_data = null;
            tab_data = tab_data2;
        }

        public void setXY(int x, int y, Boolean boo = true)
        {
            int div = Convert.ToInt32(size / 2);
            Boolean change = false;
            while((x+div)>=size || (y+div) >= size)
            {
                size = size + 100;
                change = true;
            }
            if(change)updateArray();

            tab_data[size * (y+div) + (x+div)] = boo;
        }

        public Boolean getXY(int x, int y)
        {
            int div = Convert.ToInt32(size / 2);
            return tab_data[size * (y) + (x)];
        }

        public void print_to_txt()
        {
            string[] chaine= new string[size];
            int counter=0;
                
                for (int i = 0;i<tab_data.Length;i=i+size )
                {
                    
                    for(int j=0;j<size;j++)
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
        System.IO.File.WriteAllLines(@"test2.txt", chaine);
            }
            
           
        }

        
    }

