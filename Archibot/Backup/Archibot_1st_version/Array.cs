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

        public Array(int size) {
            this.size = size;
            byte[] myBytes = new byte[size];
            for(int i=0;i<size;i++)
            {
            myBytes.SetValue(0,i);
            }

            tab_data = new BitArray(myBytes);
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(tab_data[i]);
            }
          //  PrintValues(tab_data, 8);
        }
    }
}
