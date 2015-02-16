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
        public int div;
        public double CLEANING_DISTANCE = 7.0;

        public Array(int size, string path) {
            this.size = size;
            div = Convert.ToInt32(size / 2);     
            this.path=path;
         
        }   

        public int set_array()

        {
            tab_data = new BitArray(size * size);      
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

        void updateArray(int newSize)
        {
            BitArray tab_data2 = new BitArray(newSize * newSize);  

            for(int i=0; i<tab_data.Length;i++)
            {
                tab_data2[i] = tab_data[i];
            }
            tab_data = null;
            tab_data = tab_data2;
        }

        public void setXY(int x, int y, Boolean boo = true)
        {
            
            Boolean change = false;
            while((x+div)>=(size-10) || (y+div) >= (size-10))
            {
                size = size + 100;
                div = Convert.ToInt32(size / 2);
                change = true;
            }
            if(change)updateArray(size);

            tab_data[size * (y+div) + (x+div)] = boo;
        }

        public Boolean getXY(int x, int y)
        {
            int div = Convert.ToInt32(size / 2);
            return tab_data[size * (y) + (x)];
        }

        public int getY(int index)
        {
            int test = index % size;
            int test2 = index / size;
            return test2;
        }

        public int getX(int index)
        {
            return index - getY(index) * size; ;
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


            public void clean()
            {

                for(int i=0;i<tab_data.Length;i++)
                {
                    int delete =0 ;
                    if(tab_data[i])
                    {
                        for (int j = 0; j < tab_data.Length; j++)
                        {
                            if(i!=j && tab_data[j])
                            {
                                int x1 = getX(i);
                                int y1 = getY(i);
                                int x2 = getX(j);
                                int y2 = getY(j);

                                int sousX = Math.Abs(x2 - x1);
                                int sousY = Math.Abs(y2 - y1);
                                double sousXCarre = Math.Pow(sousX, 2);
                                double sousYCarre = Math.Pow(sousY, 2);

                                double dist = Math.Sqrt(sousXCarre + sousYCarre);

                                if (dist < CLEANING_DISTANCE) delete ++;
                            }
                        }

                        
                    }
                    if (delete<3) tab_data[i] = false;
                }
            }

        }
        
    }

